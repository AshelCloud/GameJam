using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : MonoBehaviour
{
    public static StageScene Instance;
    public bool stageStart = false;

    [SerializeField] private float zoomOutTime = 2.0f;
    [SerializeField] private float zoomOutStayTime = 3.0f;
    [SerializeField] private float zoomInTime = 0.5f;
    [SerializeField] private float zoomOutSize = 25f;
    [SerializeField] private Vector3 zoomOutPos;
    [SerializeField] private int stageNum;

    private Vector3 startPos;
    private static bool hasOnce = false;

    [SerializeField]
    private AudioClip bgm;
    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        if (hasOnce == false && stageNum == 1)
        {
            Camera.main.GetComponent<FollowCamera>().enabled = false;
            float startSize = Camera.main.orthographicSize;
            startPos = GameObject.Find("Player").transform.position;
            startPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = startPos;
            zoomOutPos.z = Camera.main.transform.position.z;

            float time = 0f;
            while (time < zoomOutTime)
            {
                Camera.main.orthographicSize = Mathf.Lerp(startSize, zoomOutSize, time / zoomOutTime);
                Camera.main.transform.position = Vector3.Lerp(startPos, zoomOutPos, time / zoomOutTime);

                time += Time.deltaTime;

                yield return null;
            }
            Camera.main.orthographicSize = zoomOutSize;
            Camera.main.transform.position = zoomOutPos;

            yield return new WaitForSeconds(zoomOutStayTime);

            time = 0f;
            Camera.main.GetComponent<FollowCamera>().enabled = true;
            while (time < zoomInTime)
            {
                Camera.main.orthographicSize = Mathf.Lerp(zoomOutSize, startSize, time / zoomInTime);

                time += Time.deltaTime;

                yield return null;
            }
            Camera.main.orthographicSize = startSize;

            hasOnce = true;
            SoundManager.Instance.PlayBGM(bgm);
        }
        else if (stageNum == 2)
        {
            SoundManager.Instance.PlayBGM(bgm);
        }
    }
}
