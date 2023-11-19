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
            Debug.Log("�浹��");
            hasCollided = true;
        }
    }

    void OnTriggerExit2D(Collider2D Collider)
    {
        if(player.gameObject.activeSelf == true)
        {
            if (Collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("�浹����");
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
        // �÷��̾ ��Ȱ��ȭ�ϰų� �����
        player.gameObject.SetActive(false);
        isPlayerActive = false;
    }

    void MakePlayerAppear()
    {
        // �÷��̾ Ȱ��ȭ�ϰų� �ٽ� ���̰� �Ѵ�
        player.gameObject.SetActive(true);
        isPlayerActive = true;
    }
}
