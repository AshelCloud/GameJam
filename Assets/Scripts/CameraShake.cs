using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 m_OriginPos = Vector3.zero;

    public bool m_Shakeable = true;

    private void Start()
    {
        m_OriginPos = transform.position;
    }

    public void Run(float shakeAmount, float shakeTime)
    {
        if(m_Shakeable)
        {
            StartCoroutine(Shake(shakeAmount, shakeTime));
        }
    }

    IEnumerator Shake(float ShakeAmount, float ShakeTime)
    {
        float timer = 0;
        while (timer <= ShakeTime)
        {
            Vector3 pos = m_OriginPos + (Vector3)UnityEngine.Random.insideUnitCircle * ShakeAmount;
            pos.z = -10;
            Camera.main.transform.position = pos;
            timer += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = m_OriginPos;
    }
}
