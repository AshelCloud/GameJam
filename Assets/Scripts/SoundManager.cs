using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    public static SoundManager Instance;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new GameObject("SoundManager").AddComponent<SoundManager>();

            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        if (m_AudioSource == null)
        {
            m_AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayClip(AudioClip clip)
    {
        m_AudioSource.PlayOneShot(clip);
    }
}
