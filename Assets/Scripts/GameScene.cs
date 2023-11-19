using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    private Player playerScript;
    private HologramThrow hologram;
    private static bool hasOnce = false;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        hologram = GameObject.Find("Player").GetComponent<HologramThrow>();
    }

    private IEnumerator  Start()
    {
        if(hasOnce == false)
        {
            playerScript.playScript = false;
            hologram.enabled = false;

            yield return new WaitForSeconds(7f);
            chat.OpenWithWait("내 해킹 실력을 보여주마", (finished) => 
            {
                if (finished)
                {
                    playerScript.playScript = true;
                    hologram.enabled = true;
                }
            });

            hasOnce = true;
        }
    }
}
