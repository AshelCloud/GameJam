using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HackingButtonPrefab;

    private CursorManager m_CursorManager;

    private GameObject m_HackingButton;

    private bool m_IsOver = false;

    private void Start()
    {
        m_CursorManager = Camera.main.GetComponent<CursorManager>();
    }

    private void Update()
    {
        if(m_IsOver && Input.GetMouseButtonDown(1))
        {
            m_HackingButton = Instantiate(m_HackingButtonPrefab, GameObject.Find("Canvas").transform);
            m_HackingButton.transform.position = transform.position + new Vector3(0f, 1f, 0f);
            StartCoroutine("WaitHacking");
        }

        if(m_IsOver && Input.GetMouseButtonUp(1))
        {
            if(m_HackingButton != null)
            {
                Destroy(m_HackingButton.gameObject);
            }
            StopCoroutine("WaitHacking");
        }
    }

    private IEnumerator WaitHacking()
    {
        yield return new WaitUntil(() => m_HackingButton.GetComponent<HackingButton>().m_IsDone);

        if(m_HackingButton != null)
        {
            Destroy(m_HackingButton.gameObject);
        }

        Hacking();
    }

    protected virtual void Hacking()
    {
        
    }

    private void OnMouseOver()
    {
        if (gameObject.tag == "HackObject")
            m_CursorManager.SetCursorManager("Cursor_Hack");
        else if ((gameObject.tag == "BombObject"))
            m_CursorManager.SetCursorManager("Cursor_Bomb");

        m_IsOver = true;
    }

    private void OnMouseExit()
    {
        m_CursorManager.SetCursorManager("Cursor_Default");

        m_IsOver = false;
    }
}
