using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class TutorialChatHuman : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    Player playerScript;
    private HologramThrow hologram;

    private static bool hasOnce = false;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        hologram = GameObject.Find("Player").GetComponent<HologramThrow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasOnce == false)
        {
            if(collision.GetComponent<Player>())
            {
                hasOnce = true;
                StartCoroutine(StartChat());
            }
        }
    }

    private  IEnumerator StartChat()
    {
        string text = "";

        DisablePlayerScript();
        text = "�ƹ��� �Գ� �������� ��� ����� �����ص��� �� ���� ��� ���Ĺ����ٴ�!";
        chat.OpenWithWait(text, (finished) => 
        {
            if (finished)
            {
                playerScript.playScript = true;
                hologram.enabled = true;
            }
        });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        DisablePlayerScript();
        text = "�켱 ���⸦ Ż���ؾ߰ھ�.";
        chat.OpenWithWait(text, (finished) => 
        {
            if (finished)
            {
                playerScript.playScript = true;
                hologram.enabled = true;
            }
        });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        DisablePlayerScript();
        text = "���� ��е��� ��ŷ�ϰ� ������� � Ż���غ���";
        chat.OpenWithWait(text, (finished) => 
        {
            if (finished)
            {
                playerScript.playScript = true;
                hologram.enabled = true;
            }
        });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);
    }

    private void DisablePlayerScript()
    {
        playerScript.playScript = false;
        hologram.enabled = false;
        playerScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
