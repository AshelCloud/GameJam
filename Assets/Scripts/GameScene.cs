using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    private Player playerScript;
    private static bool hasOnce = false;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    private IEnumerator  Start()
    {
        if(hasOnce == false)
        {
            yield return new WaitForSeconds(7f);
            playerScript.playScript = false;
            chat.OpenWithWait("�� ��ŷ �Ƿ��� �����ָ�", (finished) => { if(finished) playerScript.playScript = true; });

            hasOnce = true;
        }
    }
}
