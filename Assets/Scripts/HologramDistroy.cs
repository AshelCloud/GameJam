using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramDistroy : MonoBehaviour
{
    private float destroyDelay = 3f; //���� �����ð�
    
    void Start()
    {
        StartCoroutine(deleteog());
    }
    IEnumerator deleteog()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
