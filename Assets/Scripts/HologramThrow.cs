using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramThrow : MonoBehaviour
{
    public GameObject grenadePrefab; // Ȧ�α׷� ������
    public Transform throwtf;


    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϸ� Ȧ�α׷��� �����ϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(grenadePrefab, throwtf.position, Quaternion.identity);
        }
    }
}
