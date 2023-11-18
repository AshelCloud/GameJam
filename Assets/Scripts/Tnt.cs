using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Tnt : MonoBehaviour
{
    public LayerMask detectionLayer; //오브젝트 탐지 레이어
    public float radius = 50f;

    private Collider2D[] detectedObjects;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    DetectObjects(clickPosition);
                }
            }
        }
    }

    void DetectObjects(Vector2 clickPosition)
    {
        detectedObjects = Physics2D.OverlapCircleAll(clickPosition, radius, detectionLayer);

        // 탐지된 오브젝트들을 배열에 추가
        List<Collider2D> tempList = new List<Collider2D>();
        foreach (Collider2D detectedObject in detectedObjects)
        {
            tempList.Add(detectedObject);
        }

        // 배열로 변환
        detectedObjects = tempList.ToArray();


        for(int i = 0; i < detectedObjects.Length; i++)
        {
            Debug.Log(detectedObjects[i].name);
        }
        // 여기서부터는 탐지된 오브젝트들에 대한 추가 작업 가능
        // 예를 들어, 각 오브젝트에 대한 처리나 반응 등을 할 수 있습니다.
        // detectedObjects 배열을 사용하여 필요한 작업을 수행하세요.
    }
}
