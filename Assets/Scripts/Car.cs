using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    List<Transform> m_positions = new List<Transform>();

    [SerializeField]
    private float m_MoveSpeed = 1f;

    private bool m_IsRun = false;

    private Player m_Player = null;
    private bool m_CarryPlayer = false;
    private bool m_Done = false;

    [SerializeField]
    private AudioClip clip;

    private void Update()
    {
        if(m_CarryPlayer)
        {
            m_Player.transform.position = transform.position;
        }

    }

    public void Run()
    {
        if(m_IsRun)
        {
            return;
        }

        m_CarryPlayer = true;
        m_IsRun = true;
        SoundManager.Instance.PlayEffectOneShot(clip);
        StartCoroutine(StartRun());
    }

    private IEnumerator StartRun()
    {
        foreach(Transform pos in m_positions) 
        {
            float delta = 0f;
            Vector3 originPos = transform.position;
            while(1f >= delta)
            {
                transform.position = Vector3.Lerp(originPos, pos.position, delta);
                delta += m_MoveSpeed * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }

        m_Done = true;
        m_CarryPlayer = false;

        m_Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_Player == null)
        {
            m_Player = collision.GetComponent<Player>();
        }
        if (m_Player)
        {
            Run();
        }
    }
}
