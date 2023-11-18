using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
enum PatrolSide
{
    Up,
    Down,
    Left,
    Right
}

public class Drone : MonsterObject
{
    [SerializeField]
    PatrolSystem patrolSystem;

    SpriteRenderer perceptionRangeRender;
    SpriteRenderer myRenderer;

    public bool recognizeComplete;

    [SerializeField] Color recognizeColor;
    [SerializeField] Color StopColor;
    Color originalColor;

    [SerializeField] float recognizeDuration = 1f;
    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float PatrolTime = 1f;

    PatrolSide patrolSide;
    Vector3 m_MoveDir;
    private float originalScaleX;

    List<Vector3> destList = new List<Vector3>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        perceptionRangeRender = transform.Find("PerceptionRange").gameObject.GetComponent<SpriteRenderer>();
        myRenderer = transform.GetComponent<SpriteRenderer>();
        originalColor = perceptionRangeRender.color;

        switch(patrolSystem.direction)
        {
            case PatrolDirection.Horizontal:
                patrolSide = patrolSystem.tr[0].position.x > transform.position.x ? PatrolSide.Right : PatrolSide.Left;
                break;
            case PatrolDirection.Vertical:
                patrolSide = patrolSystem.tr[0].position.y > transform.position.y ? PatrolSide.Up : PatrolSide.Down;
                break;
        }

        foreach (Transform tr in patrolSystem.tr)
        {
            destList.Add(transform.TransformDirection(tr.position));
        }

        originalScaleX = transform.localScale.x;

        StartCoroutine(Patrol());
    }

    void CheckSide()
    {
        switch (patrolSide) 
        {
            case PatrolSide.Up:
                m_MoveDir = Vector3.up;
                break;
            case PatrolSide.Down:
                m_MoveDir = -Vector3.up;
                break;
            case PatrolSide.Right:
                m_MoveDir = Vector3.right;
                transform.localScale = new Vector3(originalScaleX, transform.localScale.y, transform.localScale.z);
                break;
            case PatrolSide.Left:
                m_MoveDir = -Vector3.right;
                transform.localScale = new Vector3(-originalScaleX, transform.localScale.y, transform.localScale.z);
                break;
        }
    }

    protected override void Found()
    {
        base.Found();

        StartCoroutine(RecognizePlayer());
    }

    private void FixedUpdate()
    {
            transform.position += m_MoveDir * patrolSpeed * Time.deltaTime;
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
            if(targetObj.CompareTag("HoloPlayer"))
            {
                transform.GetComponent<ExplodableObject>().ExplodeObj(); //µå·Ð Æø¹ß 
            }
            if(targetObj.CompareTag("Player"))
            {
                targetObj.GetComponent<Player>().Die();
            }
        }
        else
        {
            perceptionRangeRender.color = originalColor;
            StartCoroutine(WaitRecognize());
            StartCoroutine(Patrol());
        }
    }

    private IEnumerator Patrol()
    {
        int i = 0;
        while(!perceptRange.GetPerception())
        {
            CheckSide();

            float distance = 0f;
            if (patrolSystem.direction == PatrolDirection.Horizontal)
                distance = (destList[i].x - transform.position.x) >= 0 ? destList[i].x - transform.position.x : -(destList[i].x - transform.position.x);
            else
                distance = (destList[i].y - transform.position.y) >= 0 ? destList[i].y - transform.position.y : -(destList[i].y - transform.position.y);

            if (distance < 0.3f)
            {
                i = ++i % destList.Count;
                switch(patrolSide)
                {
                    case PatrolSide.Up:
                        patrolSide = PatrolSide.Down;
                        break;
                    case PatrolSide.Down:
                        patrolSide = PatrolSide.Up;
                        break;
                    case PatrolSide.Left:
                        patrolSide = PatrolSide.Right;
                        break;
                    case PatrolSide.Right:
                        patrolSide = PatrolSide.Left;
                        break;
                }
            }

            yield return null;
        }
    }

    public void StopDrone()
    {
        StopAllCoroutines();
        transform.Find("PerceptionRange").gameObject.SetActive(false);
        myRenderer.color = StopColor;
        this.enabled = false;
    }
}
