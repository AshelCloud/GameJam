using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Chat chat;
    void Start()
    {
        chat.Open("�� ��ŷ �Ƿ��� �����ָ�");
    }
}
