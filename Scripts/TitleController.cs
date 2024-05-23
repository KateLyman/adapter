using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] PlayerSO playerSO;
    [SerializeField] bool overrideStart = false;

    // Start is called before the first frame update
    void Start()
    {
        if (overrideStart)
        {
            gameObject.SetActive(true);
        }

        else
        {
            if (playerSO.GetPreviousScene() == "Tutorial")
            {
                gameObject.SetActive(true);
            }
            else if (playerSO.GetPreviousScene() == "2.1A" && SceneManager.GetActiveScene().name == "1.2B")
            {
                gameObject.SetActive(true);
            }
            else if (SceneManager.GetActiveScene().name == "2.1A")
            {
                gameObject.SetActive(true);
            }
            else if (SceneManager.GetActiveScene().name == "3.1A")
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
