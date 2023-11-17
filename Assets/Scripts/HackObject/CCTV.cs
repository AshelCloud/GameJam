using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : HackableObject
{
    [SerializeField]
    private GameObject m_LightObject;

    protected override void Hacking()
    {
        base.Hacking();

        m_LightObject.SetActive(false);
    }
}
