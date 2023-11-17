using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : HackableObject
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    List<GameObject> m_ControlledDrones;

    protected override void Hacking()
    {
        base.Hacking();

        transform.GetComponent<SpriteRenderer>().sprite = sprites[1];

        foreach(var controlledDrone in m_ControlledDrones)
        {
            controlledDrone.GetComponent<Drone>().enabled = false;
            controlledDrone.transform.Find("PerceptionRange").gameObject.SetActive(false);
        }
    }
}
