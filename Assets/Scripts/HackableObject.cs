using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableObject : MonoBehaviour
{
    [SerializeField]
    private HackingButton m_HackingButton;

    protected void Start() => StartCoroutine(WaitHacking());

    private IEnumerator WaitHacking()
    {
        yield return new WaitUntil(() => m_HackingButton.m_IsSucceed);

        Hacking();
    }

    protected virtual void Hacking()
    {
        
    }
}
