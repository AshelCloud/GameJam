using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    protected Perception perceptRange;
    protected GameObject targetObj;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        perceptRange = transform.Find("PerceptionRange").gameObject.GetComponent<Perception>();

        StartCoroutine(WaitRecognize());
    }

    protected IEnumerator WaitRecognize()
    {
        yield return new WaitUntil(() => perceptRange.GetPerception());

        Found();
    }

    protected virtual void Found()
    {
        targetObj = perceptRange.GetPerceptionTarget();
    }
}
