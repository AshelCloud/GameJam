using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperBomb : MonsterObject
{
    SpriteRenderer myRenderer;

    [SerializeField]
    Transform[] destinations;

    [SerializeField]
    float blinkTransparency = 0.3f;

    [SerializeField]
    float blinkTime = 0.3f;

    [SerializeField]
    int blinkCount = 3;

    [SerializeField]
    float patrolSpeed = 2f;

    List<Vector3> destList = new List<Vector3>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myRenderer = GetComponent<SpriteRenderer>();

        foreach(Transform tr in destinations) 
        {
            destList.Add(transform.TransformDirection(tr.position));
        }

        StartCoroutine(Patrol());
    }


    protected override void Found()
    {
        base.Found();
        CheckRotate();
        StartCoroutine(Explode());
    }

    void CheckRotate()
    {
        if (targetObj.transform.position.x - transform.position.x > 0f)
            myRenderer.flipX = false;
        else if(targetObj.transform.position.x - transform.position.x < 0f)
            myRenderer.flipX = true;
    }

    IEnumerator Explode()
    {
        int count = 0;
        while (count < blinkCount)
        {
            StartCoroutine(Blink());
            yield return new WaitForSeconds(blinkTime);
            count++;
        }

        //폭발 로직 여기에 들어감
        Debug.Log("폭발!");
    }

    IEnumerator Blink()
    {
        myRenderer.color = new Color(1, 1, 1, 1 - blinkTransparency);
        yield return new WaitForSeconds(0.1f);
        myRenderer.color = Color.white;
    }

    IEnumerator Patrol()
    {
        int i = 0;
        while (!perceptRange.GetPerception())
        {
            Vector3 dir = destList[i] - transform.position;
            dir.y = 0f;
            transform.Translate(dir.normalized * patrolSpeed * Time.deltaTime);
            float distance = (destList[i].x - transform.position.x) >= 0 ? destList[i].x - transform.position.x : -(destList[i].x - transform.position.x);
            if (distance < 0.3f)
            {
                
                i = ++i % destList.Count;
                if (destList[i].x - transform.position.x > 0f)
                    myRenderer.flipX = false;
                else if(destList[i].x - transform.position.x < 0f)
                    myRenderer.flipX = true;
            }
            yield return null;
        }
    }
}
