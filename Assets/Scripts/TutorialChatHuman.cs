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
        chat.Open("�ƹ��� �Գ��������� ��� ����� ������������ ������ õ����Ŀ�� �㸦 ��ĥ ������ �ϴٴ�");
        yield return new WaitForSeconds(7f);

        chat.Open("�����ִٰ�  ���� ��ũ�ε� ���� ���ϰھ�");
        yield return new WaitForSeconds(5f);

        chat.Open("�� �Ӹ��� Ĩ�� ���� ���� ����. �� ����ġ��");
        yield return new WaitForSeconds(5f);
    }
}
