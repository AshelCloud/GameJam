using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
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

    private Animator m_Animator;

    private bool m_IsAttacking = false;

    private Vector3 dir = Vector3.zero;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => BossScene.Instance.IsStart);
        StartCoroutine(Patrol());
        StartCoroutine(Attack());
    }

    private void Update()
    {
        if(m_IsAttacking == false)
        {
            transform.position += dir * m_PatrolSpeed * Time.deltaTime;
        }
    }

    private IEnumerator Patrol()
    {
        while(true)
        {
            dir = Vector3.right;

            yield return new WaitForSeconds(m_PatrolTime);

            dir = Vector3.left;

            yield return new WaitForSeconds(m_PatrolTime);

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
            GameObject go  = Instantiate(Resources.Load<GameObject>("Particles/Explosion"), m_ExplosionPoint_L.position, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.1f, 0.1f);
            Destroy(go.gameObject, 1.5f);
        }
        else
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Particles/Explosion"), m_ExplosionPoint_R.position, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.1f, 0.1f);
            Destroy(go.gameObject, 1.5f);
        }
    }

    private IEnumerator Rush()
    {
        m_IsAttacking = true;
        yield return new WaitForSeconds(1f);

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

        m_Animator.SetBool("Attack2", true);

        while (transform.position.x - target.position.x >= 0.1f)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, m_RushSpeed);
            Vector3 origin = transform.position;

            origin.x = pos.x;
            transform.position = origin;
            yield return null;
        }

        Camera.main.GetComponent<CameraShake>().Run(0.5f, 0.5f);
        m_Animator.SetBool("Attack2", false);
        m_IsAttacking = false;

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
        yield return new WaitForSeconds(1f);
        foreach(var t in m_ArmPoses)
        {
            Instantiate(Resources.Load<GameObject>("Objects/Boss_Arm"), t.position, Quaternion.identity);
            yield return null;
        }

        m_Animator.SetBool("Attack3", false);
        m_IsAttacking = false;
    }
}
