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

        StopControlledDrones();
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
