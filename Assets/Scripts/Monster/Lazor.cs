using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum LazorState
{
    ON, OFF
}
public class Lazor : MonsterObject
{
    SpriteRenderer perceptionRangeRender;
    Color originalColor;
    Color recognizeColor = Color.black;

    [SerializeField]
    float recognizeDuration = 1f;

    [SerializeField]
    float onTime = 2f;

    [SerializeField]
    float offTime = 1f;

    LazorState state = LazorState.ON;

    protected override void Start()
    {
        base.Start();
        perceptionRangeRender = transform.GetComponent<SpriteRenderer>();
        originalColor = perceptionRangeRender.color;

        StartCoroutine("LazorOnOff");
    }

    protected override void Found()
    {
        base.Found();

        if (targetObj == null)
            return;

        StartCoroutine(KillPlayer());
    }

    IEnumerator KillPlayer()
    {
        float time = 0f;
        while (perceptRange.GetPerception() && time < recognizeDuration && targetObj.gameObject.tag == "Player")
        {
            Color lerpedColor = Color.Lerp(originalColor, recognizeColor, time / recognizeDuration);
            perceptionRangeRender.color = lerpedColor;
            yield return null;

            time += Time.deltaTime;
        }

        if (time >= recognizeDuration)
        {
            GameObject.Find("Player").GetComponent<Player>().Die();
            StopAllCoroutines();
        }
        else
        {
            perceptionRangeRender.color = originalColor;
            StartCoroutine(WaitRecognize());
        }
    }

    IEnumerator LazorOnOff()
    {
        while(true)
        {
            yield return new WaitUntil(() => !perceptRange.GetPerception());

            switch (state)
            {
                case LazorState.ON:
                    transform.GetComponent<SpriteRenderer>().enabled = false;
                    transform.Find("PerceptionRange").gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                    state = LazorState.OFF;
                    yield return new WaitForSeconds(offTime);
                    break;
                case LazorState.OFF:
                    transform.GetComponent<SpriteRenderer>().enabled = true;
                    transform.Find("PerceptionRange").gameObject.transform.GetComponent<BoxCollider2D>().enabled = true;
                    state = LazorState.ON;
                    yield return new WaitForSeconds(onTime);
                    break;
            }
        }    
    }
}
