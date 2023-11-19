using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingButton : MonoBehaviour
{
    [SerializeField]
    private Image m_FillObject;

    [SerializeField]
    private AudioClip hackingClip;

    [SerializeField]
    private AudioClip completeClip;

    public bool m_IsDone { get; private set; } = false;

    private void Start()
    {
        SoundManager.Instance.PlayEffectClip(hackingClip);
        m_FillObject.fillAmount = 0f;
    }

    private void Update()
    {
        m_FillObject.fillAmount += Time.deltaTime;

        if(m_FillObject.fillAmount >= 1f)
        {
            SoundManager.Instance.StopEffectClip();
            SoundManager.Instance.PlayEffectOneShot(completeClip);
            m_IsDone = true;
        }
    }
}
