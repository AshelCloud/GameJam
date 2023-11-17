using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramThrow : MonoBehaviour
{
    //던지는 거리는 고정
    //일정시간 있다가 제거

    //플레이어가 바라보는 방향으로 일정 거리만큼 던지기.

    //처음위치에서부터 최대거리
    //마우스 위치에따라 오른쪽이면 플러스로 던지고 왼쪽이면 마이너스로 던지고

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
