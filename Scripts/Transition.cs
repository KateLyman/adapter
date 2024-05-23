using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] string toSceneName;

    [SerializeField] bool useCurrentX;
    [SerializeField] bool useCurrentY;
    [SerializeField] Vector2 newPosition;

    private Transform player;
    [SerializeField] PlayerSO playerSO;
    [SerializeField] InputManager inputManager;

    [SerializeField] float loadWaitTime = 1.3f;

    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        inputManager = player.gameObject.GetComponent<InputManager>();

        box = GetComponent<BoxCollider2D>();

        if (gameObject.tag == "Fossil")
        {
            TransitionScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.IsAiming())
        {
            box.isTrigger = false;
        }
        else
        {
            box.isTrigger = true;
        }
    }

    public void TransitionScene()
    {
        Debug.Log("transitioning");
        if (toSceneName != null)
        {
            Debug.Log("trying");
            playerSO.SetPreviousScene(SceneManager.GetActiveScene().name);

            StartCoroutine(WaitForTransition());
        }
    }

    IEnumerator WaitForTransition()
    {
        Debug.Log("going");
        yield return new WaitForSeconds(loadWaitTime);
        playerSO.SetStartPos(newPosition);
        if (useCurrentX)
        {
            playerSO.SetStartPosX(player.position.x);
        }
        if (useCurrentY)
        {
            playerSO.SetStartPosY(player.position.y);
        }
        SceneManager.LoadScene(toSceneName);
        yield break;
    }

}
