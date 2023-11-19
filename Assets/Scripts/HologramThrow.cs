using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HologramThrow : MonoBehaviour
{
    public GameObject grenadePrefab; // Ȧ�α׷� ������
    public Transform throwtf;

    [SerializeField]
    private Image[] holoImage;

    private int currentusedHG = 0;
    [SerializeField]
    private int maxusedHg = 3; //�ִ� ������ Ƚ��

    public Color notthingColor;

    private void Start()
    {
        string hex = "7C7C7C"; //��������Ʈ ȸ������ ����
        notthingColor = HexToColor(hex);
    }
    private void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϸ� Ȧ�α׷��� �����ϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            if(currentusedHG < maxusedHg)
            {
                GameObject grenadeInstance = Instantiate(grenadePrefab, throwtf.position, Quaternion.identity);
                currentusedHG++;
                ChangeSprite();
                //���⿡�� UI���ü� �߰��ؾ���.
            }
        }
    }

    private void ChangeSprite()
    {
        Image imageComponent = holoImage[3 - currentusedHG].GetComponent<Image>();
        imageComponent.color = notthingColor;
    }
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color(r / 255f, g / 255f, b / 255f);
    }
}
