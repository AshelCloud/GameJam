using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "LAZOR")
        {
            Debug.Log("레이저 맞음");
            Destroy(coll.gameObject);

            StartCoroutine(DieLogic());
        }
    }

    IEnumerator DieLogic()
    {
        yield return null;

        Debug.Log("게임오버");

        gameObject.SetActive(false);
    }
}
