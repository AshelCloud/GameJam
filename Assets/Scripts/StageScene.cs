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
            while (Camera.main.orthographicSize < 29f)
            {
                Camera.main.orthographicSize += 2f * cameraSpeed * Time.deltaTime;

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
}
