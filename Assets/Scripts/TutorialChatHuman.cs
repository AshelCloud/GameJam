using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class TutorialChatHuman : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            StartCoroutine(StartChat());
        }
    }

    private  IEnumerator StartChat()
    {
        chat.Open("아무리 게놈지도에서 쥐와 사람이 같다하지만… 나같은 천재해커와 쥐를 합칠 생각을 하다니");
        yield return new WaitForSeconds(7f);

        chat.Open("여기있다간  뉴럴 링크로도 실험 당하겠어");
        yield return new WaitForSeconds(5f);

        chat.Open("내 머리에 칩이 박힐 수는 없지. 얼른 도망치자");
        yield return new WaitForSeconds(5f);
    }
}
