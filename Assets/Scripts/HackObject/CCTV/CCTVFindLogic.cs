using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CCTVFindLogic : MonoBehaviour
{
    [SerializeField]
    CCTV cctv;

    [SerializeField]
    LayerMask findLayer;

    [SerializeField]
    private float findRange = 5f;

    private bool isPlayerIn = false;

    private void Start()
    {
        StartCoroutine(Operate());
    }

    IEnumerator Operate()
    {
        while (true)
        {
            if (FindPlayer())
                cctv.enabled = true;
            else
                cctv.enabled = false;

            yield return null;

        }
    }

    bool FindPlayer()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, findRange, findLayer);

        if (coll == null)
            return false;
        else
            return true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
