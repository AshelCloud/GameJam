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

    private void Start()
    {
        StartCoroutine(Operate());
    }

    IEnumerator Operate()
    {
        while (!cctv.isHacked)
        {
            if (FindPlayer())
            {
                cctv.Operate();
            }
            else
            {
                cctv.Deoperate();
            }

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
}
