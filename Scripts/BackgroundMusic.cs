using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource sunlight;
    [SerializeField] AudioSource twilight;
    [SerializeField] AudioSource midnight;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        MusicUpdate(SceneManager.GetActiveScene());
    }

    // Update is called once per frame
    void Update()
    {
        MusicUpdate(SceneManager.GetActiveScene());
    }

    public void MusicUpdate(Scene scene)
    {
        Debug.Log(scene.name);

        if (scene.name == "2.1A" && !twilight.isPlaying)
        {
            sunlight.Stop();
            midnight.Stop();
            twilight.Play();
        }

        else if (scene.name == "3.1A" && !midnight.isPlaying)
        {
            twilight.Stop();
            midnight.Play();
            sunlight.Stop();
        }

        else if (scene.name == "Menu")
        {
            midnight.Stop();
            twilight.Stop();
            sunlight.Stop();
        }

        else if ((scene.name == "1.2B" || scene.name == "1.1B") && !sunlight.isPlaying)
        {
            twilight.Stop();
            midnight.Stop();
            sunlight.Play();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MusicUpdate(scene);
    }
}
