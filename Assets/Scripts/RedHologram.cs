using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHologram : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider;

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Objects/Raiser"));
            go.transform.position = new Vector2(-100f, 100f);

            go.GetComponent<Raiser>().Run();
        }
    }
}

