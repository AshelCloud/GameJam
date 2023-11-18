using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Tnt : MonoBehaviour
{
    public LayerMask detectionLayer; //������Ʈ Ž�� ���̾�
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

        // Ž���� ������Ʈ���� �迭�� �߰�
        List<Collider2D> tempList = new List<Collider2D>();
        foreach (Collider2D detectedObject in detectedObjects)
        {
            tempList.Add(detectedObject);
        }

        // �迭�� ��ȯ
        detectedObjects = tempList.ToArray();


        for(int i = 0; i < detectedObjects.Length; i++)
        {
            Debug.Log(detectedObjects[i].name);
        }
        // ���⼭���ʹ� Ž���� ������Ʈ�鿡 ���� �߰� �۾� ����
        // ���� ���, �� ������Ʈ�� ���� ó���� ���� ���� �� �� �ֽ��ϴ�.
        // detectedObjects �迭�� ����Ͽ� �ʿ��� �۾��� �����ϼ���.
    }
}
