using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private string m_SceneName = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0f;
        Instantiate(Resources.Load<GameObject>("Objects/FadeOut_Order"), pos, Quaternion.identity);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(m_SceneName);
    }
}
