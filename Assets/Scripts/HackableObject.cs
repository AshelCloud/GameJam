using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HackingButtonPrefab;

    private GameObject m_HackingButton;

    [SerializeField] Color hackedColor;

    private Color originalColor;
    private bool m_IsOver = false;
    protected bool isOperate = false;

    private void Start()
    {
        originalColor = Color.white;

        Operate();
    }

    private void Update()
    {
        if(isOperate && m_IsOver && Input.GetMouseButtonDown(1))
        {
            m_HackingButton = Instantiate(m_HackingButtonPrefab, GameObject.Find("Canvas").transform);
            m_HackingButton.transform.position = transform.position + new Vector3(0f, 1f, 0f);
            StartCoroutine("WaitHacking");
        }

        if(isOperate && m_IsOver && Input.GetMouseButtonUp(1))
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
            CursorManager.Instance.SetCursorManager("Cursor_Default");
        }

        Hacking();
    }

    protected virtual void Hacking()
    {
        Deoperate();
    }

    public void Operate()
    {
        isOperate = true;
        transform.GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void Deoperate()
    {
        isOperate = false;
        transform.GetComponent<SpriteRenderer>().color = hackedColor;
    }

    private void OnMouseOver()
    {
        if (!isOperate)
        {
            CursorManager.Instance.SetCursorManager("Cursor_Default");
            return;
        }

        if (gameObject.tag == "HackObject")
            CursorManager.Instance.SetCursorManager("Cursor_Hack");
        else if ((gameObject.tag == "BombObject"))
            CursorManager.Instance.SetCursorManager("Cursor_Bomb");

        m_IsOver = true;
    }

    private void OnMouseExit()
    {
        if (!isOperate)
            return;

        CursorManager.Instance.SetCursorManager("Cursor_Default");

        m_IsOver = false;
    }
}
