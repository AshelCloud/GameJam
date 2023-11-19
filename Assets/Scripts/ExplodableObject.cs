using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleObj;

    [SerializeField]
    private AudioClip effectClip;

    public void ExplodeObj()
    {
        Instantiate(particleObj, transform.position, Quaternion.identity);
        SoundManager.Instance.PlayEffectOneShot(effectClip);
        Destroy(gameObject);
    }

    public ParticleSystem GetParticle() { return particleObj;}
}
