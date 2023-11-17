using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorController : HackableObject
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    GameObject controlledLazor;

    private void Awake()
    {
        controlledLazor.SetActive(true);
    }

    protected override void Hacking()
    {
        base.Hacking();

        transform.GetComponent<SpriteRenderer>().sprite = sprites[1];
        controlledLazor.SetActive(false);
    }
}
