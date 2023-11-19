using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Raiser : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10f;

    private bool m_Run = false;
    private Player m_Target = null;

    private Vector3 dir = Vector3.zero;

    [SerializeField]
    private AudioClip laserClip;

    private void Start()
    {
        m_Target = GameObject.Find("Player").GetComponent<Player>();

        dir = (m_Target.transform.position - transform.position).normalized;
        LookAt2D(dir);

        StartCoroutine(Damage());
    }

    public void Run()
    {
        m_Run = true;
    }

    private void Update()
    {
        if (m_Run == false)
        {
            return;
        }

        transform.position += dir * Speed * Time.deltaTime;
    }

    protected void LookAt2D(Vector3 dir)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private IEnumerator Damage()
    {
        SoundManager.Instance.PlayEffectOneShot(laserClip);
        yield return new WaitForSeconds(0.5f);

        m_Target.Die();

        Destroy(gameObject);
    }
}
