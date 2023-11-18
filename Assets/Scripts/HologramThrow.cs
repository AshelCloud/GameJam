using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HologramThrow : MonoBehaviour
{
    public GameObject grenadePrefab; // 홀로그램 프리팹
    public Transform throwtf;
    public Image hologramused; //보이는 홀로그램수UI
    [SerializeField]
    private Sprite[] holoImage;

    private int currentusedHG = 0;
    [SerializeField]
    private int maxusedHg = 3; //최대 던지는 횟수


    private void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면 홀로그램을 던집니다.
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("버튼눌림요");
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
