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
            chat.Open("내 해킹 실력을 보여주마");

            hasOnce = true;
        }
    }
}
