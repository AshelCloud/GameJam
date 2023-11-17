using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingButton : MonoBehaviour
{
    [SerializeField]
    private Image m_FillObject;

    public bool m_IsDone { get; private set; } = false;

    private void Start()
    {
        m_FillObject.fillAmount = 0f;
    }

    private void Update()
    {
        m_FillObject.fillAmount += Time.deltaTime;

        if(m_FillObject.fillAmount >= 1f)
        {
            m_IsDone = true;
        }
    }
}
