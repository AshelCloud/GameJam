using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : HackableObject
{
    [SerializeField]
    Sprite[] sprites;

    public GameObject zoomPanel;

    [SerializeField] float zommOutSize = 12f;
    [SerializeField] float zoomOutStayTime = 5f;
    [SerializeField] float zoomOutTime = 1f;
    [SerializeField] float zoomInTime = 0.5f;

    private float originalSize;
    public bool isHacked;

    private void Awake()
    {
        isHacked = false;
    }

    protected override void Hacking()
    {
        base.Hacking();

        originalSize = Camera.main.orthographicSize;

        StartCoroutine(ZoomCamera());

        isHacked = true;
    }

    IEnumerator ZoomCamera()
    {
        float time = 0f;
        while (time < zoomOutTime) 
        {
            Camera.main.orthographicSize = Mathf.Lerp(originalSize, zommOutSize, time / zoomOutTime);
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.orthographicSize = zommOutSize;

        zoomPanel.SetActive(true);
        yield return new WaitForSeconds(zoomOutStayTime);
        zoomPanel.SetActive(false);

        time = 0f;
        while(time < zoomInTime)
        {
            Camera.main.orthographicSize = Mathf.Lerp(zommOutSize, originalSize, time / zoomInTime);
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.orthographicSize = originalSize;
    }
}
