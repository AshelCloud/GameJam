using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Run(float shakeAmount, float shakeTime)
    {
        StartCoroutine(Shake(shakeAmount, shakeTime));
    }

    IEnumerator Shake(float ShakeAmount, float ShakeTime)
    {
        GetComponent<FollowCamera>().enabled = false;

        float timer = 0;
        while (timer <= ShakeTime)
        {
            Vector3 pos = (Vector3)UnityEngine.Random.insideUnitCircle * ShakeAmount;
            pos.z = -10;
            Camera.main.transform.position = pos;
            timer += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        GetComponent<FollowCamera>().enabled = true;
    }
}
