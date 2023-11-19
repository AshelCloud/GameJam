using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHackingChat : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    Player playerScript;

    private static bool hasOnce = false;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
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
        text = "������ ����̳� ���� �ɷ��̸� �� ��ư�� ��ŷ�� �� �ְڴµ�?";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = true; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);
    }

    private void DisablePlayerScript()
    {
        playerScript.playScript = false;
        playerScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
