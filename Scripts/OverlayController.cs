using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayController : MonoBehaviour
{
    [SerializeField] Image overlay;
    [SerializeField] bool initialFadeIn = true;

    [SerializeField] float timeFade = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (overlay == null)
        {
            overlay = GetComponent<Image>();
        }

        if (initialFadeIn)
        {
            SceneManager.sceneLoaded += FadeInScene;
            FadeIn();
        }

        if (gameObject.tag == "Fossil")
        {
            FadeOutTime(timeFade);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        //Make the alpha 1
        Color fixedColor = overlay.color;
        fixedColor.a = 1;
        overlay.color = fixedColor;

        overlay.CrossFadeAlpha(1, 0f, true);

        overlay.CrossFadeAlpha(0, 1.0f, false);
    }

    public void FadeOut()
    {
        //Make the alpha 1
        Color fixedColor = overlay.color;
        fixedColor.a = 1;
        overlay.color = fixedColor;

        //Set the 0 to zero then duration to 0
        overlay.CrossFadeAlpha(0f, 0f, true);

        //Finally perform CrossFadeAlpha
        overlay.CrossFadeAlpha(1, 1.0f, false);
    }

    public void FadeInTime(float time)
    {
        //Make the alpha 1
        Color fixedColor = overlay.color;
        fixedColor.a = 1;
        overlay.color = fixedColor;

        overlay.CrossFadeAlpha(1, 0f, true);

        overlay.CrossFadeAlpha(0, time, false);
    }

    public void FadeOutTime(float time)
    {
        //Make the alpha 1
        Color fixedColor = overlay.color;
        fixedColor.a = 1;
        overlay.color = fixedColor;

        //Set the 0 to zero then duration to 0
        overlay.CrossFadeAlpha(0f, 0f, true);

        //Finally perform CrossFadeAlpha
        overlay.CrossFadeAlpha(1, time, false);
    }

    public void FadeInScene(Scene scene, LoadSceneMode mode)
    {
        FadeIn();
    }
}
