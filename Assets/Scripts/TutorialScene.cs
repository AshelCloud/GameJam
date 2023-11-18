using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : MonoBehaviour
{
    public static TutorialScene Instance;

    [SerializeField]
    private Chat chat;

    public bool m_IsStart = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartChat());
    }

    private IEnumerator StartChat()
    {
        m_IsStart = false;

        chat.Open("�� ���� �� �̷��� ����?");

        yield return new WaitForSeconds(3f);

        chat.Open("�� ���� �� �̷��� ����");

        yield return new WaitForSeconds(3f);

        m_IsStart = true;
    }
}
