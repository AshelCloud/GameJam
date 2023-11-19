using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    private Player playerScript;
    private HologramThrow hologramScript;
    private static bool hasOnce = false;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        hologramScript = GameObject.Find("Player").GetComponent<HologramThrow>();
    }

    private IEnumerator  Start()
    {
        if(hasOnce == false)
        {
            playerScript.playScript = false;
            hologramScript.playScript = false;
            yield return new WaitForSeconds(7f);
            chat.gameObject.SetActive(true);
            chat.StartCoroutine(chat.TextOpenAndWait("�� ��ŷ �Ƿ��� �����ָ�", (finished) => 
            {
                if (finished)
                {
                    playerScript.playScript = true;
                    hologramScript.playScript = true;
                }
            })) ;
            //chat.Open("�� ��ŷ �Ƿ��� �����ָ�");
            //playerScript.playScript = true;

            hasOnce = true;
        }
    }
}
