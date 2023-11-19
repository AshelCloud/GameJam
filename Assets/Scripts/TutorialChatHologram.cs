using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChatHologram : MonoBehaviour
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
        if (hasOnce == false)
        {
            if (collision.GetComponent<Player>())
            {
                hasOnce = true;
                StartCoroutine(StartChat());
            }
        }
    }

    private IEnumerator StartChat()
    {
        string text = "";

        DisablePlayerScript();
        text = "ũ���۰� �Ա��տ� ���ֱ�. Ȧ�α׷����� �����ؼ� ���Ľ��Ѿ߰ھ�";
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
