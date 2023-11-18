using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramThrow : MonoBehaviour
{
    public GameObject grenadePrefab; // 홀로그램 프리팹
    public Transform throwtf;


    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면 홀로그램을 던집니다.
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(grenadePrefab, throwtf.position, Quaternion.identity);
        }
    }
}
