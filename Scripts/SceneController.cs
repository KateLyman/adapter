using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameEventSO gameStartEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseEventStartGame()
    {
        if (gameStartEvent != null)
        {
            gameStartEvent.Raise();
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void TimeStop()
    {
        Time.timeScale = 0.0f;
    }

    public void TimeStart()
    {
        Time.timeScale = 1.0f;
    }

    public void TimeSet(float time)
    {
        Time.timeScale = time;
    }

    public void DeselectCurrent()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SelectThis(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(obj);
    }
}
