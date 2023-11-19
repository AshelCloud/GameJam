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
        text = "아무리 게놈 지도에서 쥐와 사람이 같다해도… 내 몸을 쥐와 합쳐버리다니!";
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
        text = "우선 여기를 탈출해야겠어.";
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
        text = "정찰 드론들을 해킹하고 실험실을 어서 탈출해보자";
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
