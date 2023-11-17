using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentCtrl : MonoBehaviour
{
    public Player playerComponent;
    public void StopAllComponent()
    {
        playerComponent.enabled = false;
    }
}
