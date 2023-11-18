using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm : MonoBehaviour
{
    [SerializeField]
    private float m_MaxHeight = -1f;

    [SerializeField]
    private float m_MinHeight = -6f;

    private float m_Speed = 20f;

    private void Start()
    {
        StartCoroutine(Up());
    }

    private IEnumerator Up()
    {
        while(true)
        {
            transform.position += Vector3.up * m_Speed  * Time.deltaTime;

            if(transform.position.y > m_MaxHeight)
            {
                break;
            }
            yield return null;
        }

        while(true)
        {
            transform.position -= Vector3.up * m_Speed * Time.deltaTime;

            if (transform.position.y < m_MinHeight)
            {
                break;
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
