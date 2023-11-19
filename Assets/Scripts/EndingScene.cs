using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndingScene : MonoBehaviour
{
    [SerializeField]
    private Chat chat;

    [SerializeField]
    private GameObject m_BG;

    private IEnumerator Start()
    {
        for(int i = 0; i < 30; i  ++)
        {
            Color color = m_BG.GetComponent<SpriteRenderer>().color;

            float amount = 1f * Time.deltaTime;
            color.r -= amount;
            color.g -= amount;
            color.b -= amount;

            m_BG.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(ZoomIn());

        GameObject.Find("Boss").GetComponent<Animator>().SetBool("Die", true);
        for(int i = 0; i < 10; i ++)
        {
            Vector3 pos = new Vector3(Random.Range(-3.5f, 11f), Random.Range(-1.5f, 7.5f));
            Instantiate(Resources.Load<GameObject>("Particles/Strong_Explosion"), pos, Quaternion.identity);
            Camera.main.GetComponent<CameraShake>().Run(0.3f, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }

        Instantiate(Resources.Load<GameObject>("Objects/FadeOut"));

        yield return new WaitForSeconds(2f);

        chat.Open("난 이제 생체실험에서 벗어난 자유의 몸이다!!!!!!!!!", false);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Credit");
    }

    private IEnumerator ZoomIn()
    {
        while (true)
        {
            Camera.main.orthographicSize -= 2f * Time.deltaTime;
            if(Camera.main.orthographicSize <= 5f)
            {
                Camera.main.orthographicSize = 5f;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
