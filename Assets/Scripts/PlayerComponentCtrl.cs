using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentCtrl : MonoBehaviour
{
    [SerializeField]
    PlayerMove playerMoveComp;

    [SerializeField]
    PlayerHit playerHitComp;

    public void StopAllComponent()
    {
        playerMoveComp.enabled = false;
        playerHitComp.enabled = false;
    }
}
