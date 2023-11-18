using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleObj;

    public void ExplodeObj()
    {
        Instantiate(particleObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public ParticleSystem GetParticle() { return particleObj;}
}
