using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WarningPrefab;

    [SerializeField]
    private float m_PatrolSpeed = 3f;

    [SerializeField]
    private float m_PatrolTime = 1f;

    [SerializeField]
    private Transform m_Min;

    [SerializeField]
    private Transform m_Max;

    [SerializeField]
    private float m_RushSpeed = 0.5f;

    [SerializeField]
    private Transform m_ExplosionPoint_L;

    [SerializeField]
    private Transform m_ExplosionPoint_R;

    [SerializeField]
    private List<Transform> m_ArmPoses = new List<Transform>();

    [SerializeField]
    private Transform m_Center;

    [SerializeField]
    private BoxCollider2D m_LeftHand;

    [SerializeField]
    private BoxCollider2D m_RightHand;

    [SerializeField]
    private AudioClip pattern2;

    private Animator m_Animator;

    private bool m_IsAttacking = false;

    private Vector3 dir = Vector3.zero;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        m_LeftHand.enabled = false;
        m_RightHand.enabled = false;

        yield return new WaitUntil(() => BossScene.Instance.IsStart);
        StartCoroutine(Attack());
    }

    private void Update()
    {
        if(m_IsAttacking == false)
        {
            dir = ((m_Center.position + Vector3.right * Random.Range(-5f, 5f) ) - transform.position).normalized;
            dir.y = 0f;
            dir.z = 0f;

            transform.position += dir * m_PatrolSpeed * Time.deltaTime;
        }
    }

    private IEnumerator Attack()
    {
        while(true)
        {
            StartCoroutine(Attack01());
            yield return new WaitUntil(() => m_IsAttacking == false);

            yield return new WaitForSeconds(3f);

            StartCoroutine(Rush());
            yield return new WaitUntil(() => m_IsAttacking == false);

            yield return new WaitForSeconds(3f);

            StartCoroutine(Attack03());
            yield return new WaitUntil(() => m_IsAttacking == false);

            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator Attack01()
    {
        m_IsAttacking = true;
        yield return new WaitForSeconds(1f);

        m_Animator.SetBool("Attack1", true);
        yield return new WaitForSeconds(2.5f);
        m_Animator.SetBool("Attack1", false);
        m_IsAttacking = false;
    }

    public void Explosion()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            m_LeftHand.enabled = true;
            GameObject go  = Instantiate(Resources.Load<GameObject>("Particles/Explosion"), m_ExplosionPoint_L.position, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.1f, 0.1f);
            Destroy(go.gameObject, 1.5f);

            StartCoroutine(DisableHand());
        }
        else
        {
            m_RightHand.enabled = true;
            GameObject go = Instantiate(Resources.Load<GameObject>("Particles/Explosion"), m_ExplosionPoint_R.position, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.1f, 0.1f);
            Destroy(go.gameObject, 1.5f);

            StartCoroutine(DisableHand());
        }
    }

    private IEnumerator DisableHand()
    {
        yield return new WaitForSeconds(0.1f);
        m_LeftHand.enabled = false;
        m_RightHand.enabled = false;
    }

    public bool m_IsRushing;

    private IEnumerator Rush()
    {
        m_IsAttacking = true;
        m_IsRushing = true;

        GameObject player = GameObject.Find("Player");
        Transform target = null;

        if (Vector3.Distance(player.transform.position, m_Min.position) > Vector3.Distance(player.transform.position, m_Max.position))
        {
            target = m_Max;
        }
        else
        {
            target = m_Min;
        }

        GameObject go = Instantiate(Resources.Load<GameObject>("Objects/Warning_Rush"), target);
        if(target == m_Max)
        {
            foreach(Transform child in go.transform)
            {
                child.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
        yield return new WaitForSeconds(1f);

        Destroy(go.gameObject);

        m_Animator.SetBool("Attack2", true);

        while (Mathf.Abs(transform.position.x - target.position.x) >= 0.1f)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, m_RushSpeed);
            Vector3 origin = transform.position;

            origin.x = pos.x;
            transform.position = origin;
            yield return null;
        }

        SoundManager.Instance.PlayEffectOneShot(pattern2);
        Camera.main.GetComponent<CameraShake>().Run(0.5f, 0.5f);
        m_Animator.SetBool("Attack2", false);
        m_IsAttacking = false;
        m_IsRushing = false;

        for (int i = 0; i < 3; i ++)
        {
            TNTSpawner.Instance.Spawn();
            yield return new WaitForSeconds(0.3f);
        }

        for (int i = 0; i < 2; i++)
        {
            CreeperSpawner.Instance.Spawn();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator Attack03()
    {
        m_IsAttacking = true;
        yield return new WaitForSeconds(1f);

        m_Animator.SetBool("Attack3", true);
        List<GameObject> warnings = new List<GameObject>();
        foreach (var t in m_ArmPoses)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Objects/Warning"), t.position + Vector3.up * 5f, Quaternion.identity);
            warnings.Add(go);
        }
        yield return new WaitForSeconds(1f);
        foreach(var g in warnings)
        {
            Destroy(g.gameObject);
        }

        foreach(var t in m_ArmPoses)
        {
            Instantiate(Resources.Load<GameObject>("Objects/Boss_Arm"), t.position, Quaternion.identity);
            yield return null;
        }

        m_Animator.SetBool("Attack3", false);
        m_IsAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            if(m_IsRushing)
            {
                collision.GetComponent<Player>().Die();
            }
        }
    }

    public void GetDamage(float damage)
    {
        StartCoroutine(RedBlink());
    }

    private IEnumerator RedBlink()
    {
        for(int i = 0; i < 5; i ++)
        {
            foreach(Transform child in transform)
            {
                if(child.GetComponent<SpriteRenderer>())
                {
                    child.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

            yield return new WaitForSeconds(0.1f);

            foreach (Transform child in transform)
            {
                if (child.GetComponent<SpriteRenderer>())
                {
                    child.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
