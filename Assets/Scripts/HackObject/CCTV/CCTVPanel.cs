using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVPanel : MonoBehaviour
{
    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = main.transform.position;
        pos.z = 0f;
        transform.position = pos;
    }
}
