using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosionSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip explosionSound;

    void Start()
    {
        SoundManager.Instance.PlayEffectOneShot(explosionSound);
    }
}
