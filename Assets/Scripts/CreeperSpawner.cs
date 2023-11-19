using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperSpawner : MonoBehaviour
{
    public static CreeperSpawner Instance;

    [SerializeField]
    private GameObject m_CreeperPrefab;

    [SerializeField]
    private Transform m_Min;

    [SerializeField]
    private Transform m_Max;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn()
    {
        float X = Random.Range(m_Min.position.x, m_Max.position.x);

        GameObject go = Instantiate(m_CreeperPrefab, new Vector3(X, transform.position.y), Quaternion.identity);

        go.GetComponent<CreeperBomb>().GetComponentInChildren<CreeperPatrol>().isRandomPatrol = true;

        Destroy(go.gameObject, 5f);
    }
}
