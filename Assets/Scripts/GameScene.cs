using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    private static bool hasOnce = false;

    private IEnumerator  Start()
    {
        if(hasOnce == false)
        {
            yield return new WaitForSeconds(7f);
            chat.Open("�� ��ŷ �Ƿ��� �����ָ�");

            hasOnce = true;
        }
    }
}
