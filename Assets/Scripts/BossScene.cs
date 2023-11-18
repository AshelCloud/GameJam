using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour
{
    public static BossScene Instance;

    [SerializeField]
    private float m_CameraSpeed = 1f;

    [SerializeField]
    private GameObject m_BossPanel = null;

    private static bool hasOnce = false;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsStart = false;

    private IEnumerator Start()
    {
        if(hasOnce == false)
        {
            m_BossPanel.SetActive(true);

            yield return new WaitForSeconds(3f);

            Destroy(m_BossPanel.gameObject);
            hasOnce = true;
        }

        while(true)
        {
            Camera.main.orthographicSize += m_CameraSpeed * Time.deltaTime;

            if(Camera.main.orthographicSize >= 20f)
            {
                Camera.main.orthographicSize = 20f;
                break;
            }

            yield return null;
        }

        IsStart = true;
    }
}
