using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_Chat;
    private string m_OriginText;
    private string m_CurrentText;
    private bool m_EndIsClose = true;

    public void Open(string text, bool endIsClose = true)
    {
        txt_Chat.text = "";

        m_EndIsClose = endIsClose;
        m_OriginText = text;
        m_CurrentText = "";
        gameObject.SetActive(true);

        StartCoroutine(TextOpen());
    }

    private IEnumerator TextOpen()
    {
        foreach(var ch in m_OriginText)
        {
            m_CurrentText += ch;

            txt_Chat.text = m_CurrentText;

            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(0.5f);

        if(m_EndIsClose)
        {
            gameObject.SetActive(false);
            txt_Chat.text = "";
        }
    }
}
