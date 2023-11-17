using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum PatrolSide
{
    Left,
    Right
}

public class Drone : MonsterObject
{
    SpriteRenderer perceptionRangeRender;

    public bool recognizeComplete;

    Color originalColor;

    [SerializeField]
    Color recognizeColor;

    [SerializeField]
    float recognizeDuration = 1f;

    [SerializeField]
    PatrolSide side = PatrolSide.Right;

    [SerializeField]
    float patrolSpeed = 2f;

    Vector3 rightsidePos;
    Vector3 leftsidePos;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        perceptionRangeRender = transform.Find("PerceptionRange").gameObject.GetComponent<SpriteRenderer>();
        originalColor = perceptionRangeRender.color;

        rightsidePos = transform.TransformDirection(transform.Find("RightPoint").transform.position);
        leftsidePos = transform.TransformDirection(transform.Find("LeftPoint").transform.position);

        StartCoroutine(Patrol());
    }

    protected override void Found()
    {
        base.Found();

        StartCoroutine(RecognizePlayer());
    }

    IEnumerator RecognizePlayer()
    {
        float time = 0f;
        while (perceptRange.GetPerception() && time < recognizeDuration)
        {
            Color lerpedColor = Color.Lerp(originalColor, recognizeColor, time/recognizeDuration);
            perceptionRangeRender.color = lerpedColor;
            yield return null;

            time += Time.deltaTime;
        }

        if (time >= recognizeDuration)
        {
            targetObj.GetComponent<PlayerComponentCtrl>().StopAllComponent();
            targetObj.GetComponent<SpriteRenderer>().GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            perceptionRangeRender.color = originalColor;
            StartCoroutine(WaitRecognize());
            StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol()
    {
        while (!perceptRange.GetPerception())
        {
            switch (side)
            {
                case PatrolSide.Right:
                    MoveToRightPoint();
                    break;
                case PatrolSide.Left:
                    MoveToLeftPoint();
                    break;
            }
            yield return null;
        }
    }

    void MoveToRightPoint()
    {
        if(Vector3.Distance(transform.position, rightsidePos) > 1.0f)
        {
            transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
        }
        else
        {
            transform.Rotate(0, 180, 0);
            side = PatrolSide.Left;
        }
    }

    void MoveToLeftPoint()
    {
        if (Vector3.Distance(transform.position, leftsidePos) > 1.0f)
        {
            transform.position -= Vector3.right * patrolSpeed * Time.deltaTime;
        }
        else
        {
            transform.Rotate(0, 180, 0);
            side = PatrolSide.Right;
        }
    }
}
