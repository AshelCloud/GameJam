using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLazer : MonoBehaviour
{
    private GameObject m_Boss;

    [SerializeField]
    private float m_Speed = 1f;

    private void Start()
    {
        m_Boss = GameObject.Find("Boss");
    }

    void Update()
    {
        Vector3 dir = m_Boss.transform.position - transform.position;
        LookAt2D(dir);

        transform.position += dir * m_Speed * Time.deltaTime;
    }

    protected void LookAt2D(Vector3 dir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Boss>())
        {
            collision.GetComponent<Boss>().GetDamage(15f);
            Destroy(gameObject);
        }
    }
}
