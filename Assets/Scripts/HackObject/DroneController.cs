using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : HackableObject
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    GameObject controlledDrone;

    protected override void Hacking()
    {
        base.Hacking();

        transform.GetComponent<SpriteRenderer>().sprite = sprites[1];
        controlledDrone.GetComponent<Drone>().enabled = false;
        controlledDrone.transform.Find("PerceptionRange").gameObject.SetActive(false);
    }
}
