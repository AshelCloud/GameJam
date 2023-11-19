using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageHide : MonoBehaviour
{
    public Player player;
    private bool isPlayerActive = true;
    [SerializeField]
    private bool hasCollided = false;

    void Start()
    {

        if (player == null)
        {
            Debug.LogError("Player reference not assigned!");
        }
            player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌함");
            hasCollided = true;
        }
    }

    void OnTriggerExit2D(Collider2D Collider)
    {
        if(player.gameObject.activeSelf == true)
        {
            if (Collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("충돌종료");
                hasCollided = false;
            }
        }
    }

    void Update()
    {
        if (!player.m_IsDie &&  hasCollided && Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerActive)
            {
                MakePlayerDisappear();
            }
            else
            {
                MakePlayerAppear();
            }
            Vector3 currentScale = player.transform.localScale;
            currentScale.x = 1f;
            player.transform.localScale = currentScale;
        }
    }

    void MakePlayerDisappear()
    {
        // 플레이어를 비활성화하거나 숨긴다
        player.gameObject.SetActive(false);
        isPlayerActive = false;
    }

    void MakePlayerAppear()
    {
        // 플레이어를 활성화하거나 다시 보이게 한다
        player.gameObject.SetActive(true);
        isPlayerActive = true;
    }
}
