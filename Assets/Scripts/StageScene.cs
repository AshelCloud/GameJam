using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : MonoBehaviour
{
    public static StageScene Instance;
    [SerializeField]
    private float cameraSpeed = 2.0f;
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    public Transform player;

    [SerializeField]
    private Vector3 targetPosition;

    private bool fadein = false;
    public bool stageStart = false;

    private static bool hasOnce = false;

    private void Awake()
    {
        Instance= this;
    }

    private IEnumerator Start()
    {
        if (hasOnce == false)
        {
            Camera.main.GetComponent<FollowCamera>().enabled = false;
            while (true)
            {
                Camera.main.orthographicSize += 2f * cameraSpeed * Time.deltaTime;

                if (Camera.main.orthographicSize >= 29f)
                {
                    break;
                }
                yield return null;
            }

            Camera.main.GetComponent<FollowCamera>().enabled = true;
            while (true)
            {
                Camera.main.orthographicSize -= 8f * cameraSpeed * Time.deltaTime;
                if (Camera.main.orthographicSize <= 7f)
                {
                    Camera.main.orthographicSize = 7f;
                    this.transform.position = targetPosition;
                    fadein = true;

                    hasOnce = true;
                    break;
                }

                yield return null;
            }
        }
    }

    private void Update()
    {
        if(fadein == true)
        {
            if (player != null)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                float distanceToPlayer = directionToPlayer.magnitude;

                if (distanceToPlayer > 0.1f)
                {
                    // 플레이어 쪽으로 일정 속도로 이동
                    Vector3 moveVector = directionToPlayer.normalized * moveSpeed * Time.deltaTime;

                    // 이동할 거리가 남은 거리보다 크다면 플레이어 위치로 이동
                    if (moveVector.magnitude > distanceToPlayer)
                    {

                        transform.position = player.position;
                    }
                    else
                    {
                        transform.position += moveVector;
                        stageStart = true;
                    }
                }
            }
        }
    }
}
