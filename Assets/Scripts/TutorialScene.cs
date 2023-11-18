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

        chat.Open("≥ª ∏ˆ¿Ã ø÷ ¿Ã∑∏∞‘ µ∆¡ˆ?");

        yield return new WaitForSeconds(3f);

        chat.Open("»Ú ≈–¿Ã ø÷ ¿Ã∑∏∞‘ ∏πæ∆");

        yield return new WaitForSeconds(3f);

        m_IsStart = true;
    }
}
