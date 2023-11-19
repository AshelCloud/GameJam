using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class TutorialChatHuman : MonoBehaviour
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
        text = "�ƹ��� �Գ��������� ��� ����� ������������ ������ õ����Ŀ�� �㸦 ��ĥ ������ �ϴٴ�";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = false; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        DisablePlayerScript();
        text = "�����ִٰ�  ���� ��ũ�ε� ���� ���ϰھ�";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = false; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        DisablePlayerScript();
        text = "�� �Ӹ��� Ĩ�� ���� ���� ����. �� ����ġ��";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = true; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);
    }

    private void DisablePlayerScript()
    {
        playerScript.playScript = false;
        playerScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
