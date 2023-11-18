using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HologramThrow : MonoBehaviour
{
    public GameObject grenadePrefab; // Ȧ�α׷� ������
    public Transform throwtf;
    public Image hologramused; //���̴� Ȧ�α׷���UI
    [SerializeField]
    private Sprite[] holoImage;

    private int currentusedHG = 0;
    [SerializeField]
    private int maxusedHg = 3; //�ִ� ������ Ƚ��


    private void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϸ� Ȧ�α׷��� �����ϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("��ư������");
            if(currentusedHG < maxusedHg)
            {
                Instantiate(grenadePrefab, throwtf.position, Quaternion.identity);
                currentusedHG++;
                ChangeSprite();
            }
        }
    }

    private void ChangeSprite()
    {
        hologramused.sprite = holoImage[currentusedHG];
    }
}
