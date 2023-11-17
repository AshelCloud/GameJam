using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Gauge;

    [SerializeField]
    private float m_GaugeSpeed = 2f;

    private bool m_IsMouseDown = false;

    public bool m_IsSucceed { get; private set; } = false;

    private void Update()
    {
        if(m_IsSucceed)
        {
            return;
        }

        if (m_IsMouseDown)
        {
            m_Gauge.transform.localScale += new Vector3(0f, m_GaugeSpeed, 0f) * Time.deltaTime;
        }

        if(m_Gauge.transform.localScale.y >= 4.2f)
        {
            m_IsSucceed = true;
        }
    }

    private void OnMouseDown()
    {
        m_IsMouseDown = true;
    }

    private void OnMouseUp()
    {
        m_IsMouseDown = false;
    }
}
