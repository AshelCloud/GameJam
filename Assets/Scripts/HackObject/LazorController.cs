using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if(SceneManager.GetActiveScene().name == "Game_Boss")
        {
            StartCoroutine(RunHack());
        }
        else
        {
            Disable();
        }
    }

    private void Disable()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprites[1];
        controlledLazor.SetActive(false);
        this.enabled = false;
    }

    private IEnumerator RunHack()
    {
        controlledLazor.SetActive(false);
        AttackBoss();

        yield return new WaitForSeconds(3f);

        Disable();

    }

    private void AttackBoss()
    {
        Instantiate(Resources.Load("Objects/BossAttack_Lazer"), transform.position, Quaternion.identity);
    }
}
