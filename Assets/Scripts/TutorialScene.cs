using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;

public class TutorialScene : MonoBehaviour
{
    public static TutorialScene Instance;

    [SerializeField]
    private Chat chat;

    public bool m_IsStart = false;

    private static bool hasOnce = false;

    Player playerScript;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(hasOnce == false)
        {
            hasOnce = true;

            playerScript = GameObject.Find("Player").GetComponent<Player>();

            StartCoroutine(StartChat());
        }
    }

    private IEnumerator StartChat()
    {
        string text = "";

        m_IsStart = false;

        playerScript.playScript = false;
        text = "�� ���� �� �̷��� ����?";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = true; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        playerScript.playScript = false;
        text = "�� ���� �� �̷��� ����";
        chat.OpenWithWait(text, (finished) => { if (finished) playerScript.playScript = true; });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        yield return new WaitForSeconds(3f);

        m_IsStart = true;
    }
}
