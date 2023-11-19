using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossControllHacking : HackableObject
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    List<GameObject> m_ControlledDrones;

    protected override void Hacking()
    {
        base.Hacking();

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0f;
        Instantiate(Resources.Load<GameObject>("Objects/FadeOut_Order"), pos, Quaternion.identity);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Ending");
    }
}
