using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTSpawner : MonoBehaviour
{
    public static TNTSpawner Instance;

    [SerializeField]
    private GameObject m_TNTPrefab;

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

        GameObject go = Instantiate(m_TNTPrefab, new Vector3(X, transform.position.y), Quaternion.identity);
        Destroy(go.gameObject, 5f);
    }
}
