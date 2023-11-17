using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazor : MonoBehaviour
{
    public void FireToPlayer()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return null;
    }
}
