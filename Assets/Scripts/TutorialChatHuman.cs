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
        text = "아무리 게놈지도에서 쥐와 사람이 같다하지만… 나같은 천재해커와 쥐를 합칠 생각을 하다니";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = false; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        DisablePlayerScript();
        text = "여기있다간  뉴럴 링크로도 실험 당하겠어";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = false; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        DisablePlayerScript();
        text = "내 머리에 칩이 박힐 수는 없지. 얼른 도망치자";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = true; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);
    }

    private void DisablePlayerScript()
    {
        playerScript.playScript = false;
        playerScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
