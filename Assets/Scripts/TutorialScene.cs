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
    private HologramThrow hologram;

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
            hologram = GameObject.Find("Player").GetComponent<HologramThrow>();

            StartCoroutine(StartChat());
        }
    }

    private IEnumerator StartChat()
    {
        string text = "";

        m_IsStart = false;

        playerScript.playScript = false;
        hologram.enabled = false;

        text = "³» ¸öÀÌ ¿Ö ÀÌ·¸°Ô µÆÁö?";
        chat.OpenWithWait(text, (finished) => 
        {
            if (finished)
            {
            }
        });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        text = "Èò ÅÐÀÌ ¿Ö ÀÌ·¸°Ô ¸¹¾Æ";
        chat.OpenWithWait(text, (finished) => 
        {
            if (finished)
            {
                playerScript.playScript = true;
                hologram.enabled = true;
            }
        });
        yield return new WaitForSeconds(text.Length * 0.03f + 2f);

        yield return new WaitForSeconds(3f);

        m_IsStart = true;
    }
}
