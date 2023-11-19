using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class MainButten : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime = 2.0f;
    [SerializeField]
    private Image fadeimg;


    public void GameExit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeimg.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeimg.color = color;

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game_tutorial");
    }

}
