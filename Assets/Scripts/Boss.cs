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

    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private IEnumerator Attack01()
    {
        yield return new WaitForSeconds(1f);

        m_Animator.SetBool("Attack1", true);
        yield return new WaitForSeconds(2.5f);
        m_Animator.SetBool("Attack1", false);
    }

    public void Explosion()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            GameObject go  = Instantiate(Resources.Load<GameObject>("Particles/Explosion"), m_ExplosionPoint_L.position, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.05f, 0.05f);

            Destroy(go.gameObject, 1.5f);
        }
        else
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Particles/Explosion"), m_ExplosionPoint_R.position, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.05f, 0.05f);
            Destroy(go.gameObject, 1.5f);
        }
    }

    private IEnumerator Rush()
    {
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

        while(transform.position.x - target.position.x >= 0.1f)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, m_RushSpeed);
            Vector3 origin = transform.position;

            origin.x = pos.x;
            transform.position = origin;
            yield return null;
        }

        Camera.main.GetComponent<CameraShake>().Run(0.5f, 0.2f);
        m_Animator.SetBool("Attack2", false);

        for(int i = 0; i < 3; i ++)
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
}
