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

    private void Start()
    {
        Run();
    }

    public void Run()
    {
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
    }
}
