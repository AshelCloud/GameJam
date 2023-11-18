using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : HackableObject
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    List<GameObject> m_ControlledDrones;

    [SerializeField] Color hackedColor;

    protected override void Hacking()
    {
        base.Hacking();

        transform.GetComponent<SpriteRenderer>().sprite = sprites[1];
        transform.GetComponent<SpriteRenderer>().color = hackedColor;

        StopControlledDrones();

        this.enabled = false;
    }

    void StopControlledDrones()
    {
        foreach (var controlledDrone in m_ControlledDrones)
        {
            if (controlledDrone != null)
                controlledDrone.GetComponent<Drone>().StopDrone();
        }
    }

    private void OnDestroy()
    {
        StopControlledDrones();
    }
}
