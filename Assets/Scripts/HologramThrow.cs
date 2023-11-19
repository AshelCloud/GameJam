using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HologramThrow : MonoBehaviour
{
    public GameObject grenadePrefab; // 홀로그램 프리팹
    public Transform throwtf;

    [SerializeField]
    private Image[] holoImage;

    private int currentusedHG = 0;
    [SerializeField]
    private int maxusedHg = 3; //최대 던지는 횟수

    public Color notthingColor;

    private void Start()
    {
        string hex = "7C7C7C"; //스프라이트 회색으로 변경
        notthingColor = HexToColor(hex);
    }
    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면 홀로그램을 던집니다.
        if (Input.GetMouseButtonDown(0))
        {
            if(currentusedHG < maxusedHg)
            {
                GameObject grenadeInstance = Instantiate(grenadePrefab, throwtf.position, Quaternion.identity);
                currentusedHG++;
                ChangeSprite();
                //여기에서 UI가시성 추가해야함.
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
