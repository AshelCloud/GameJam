using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour
{
    public static BossScene Instance;

    [SerializeField]
    private float m_CameraSpeed = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsStart = false;

    private IEnumerator Start()
    {
        while(true)
        {
            Camera.main.orthographicSize += m_CameraSpeed * Time.deltaTime;

            if(Camera.main.orthographicSize >= 16f)
            {
                Camera.main.orthographicSize = 16f;
                break;
            }

            yield return null;
        }

        IsStart = true;
    }
}
