using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popups : MonoBehaviour
{
    [SerializeField] List<GameObject> displayImages;
    [SerializeField] ProfessorController.PEX expression;

    [SerializeField] ProfessorController professor;
    private Animator profAnimation;

    [SerializeField] GameEventSO dialogueStartEvent;
    [SerializeField] GameEventSO dialogueFlipEvent;
    [SerializeField] GameEventSO dialogueEndEvent;

    private int currentIndex = 0;

    [SerializeField] bool canBeSkipped = true;
    [SerializeField] bool canBeFinished = true;
    [SerializeField] bool usesTrigger = true;
    [SerializeField] bool expires = false;
    [SerializeField] float expirationTime;
    [SerializeField] bool destroyOnFinish = false;
    [SerializeField] GameObject objectToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        // displayImages[currentIndex].SetActive(false);
        profAnimation = professor.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (profAnimation.GetCurrentAnimatorStateInfo(0).IsName("Hide"))
        {
            displayImages[currentIndex].SetActive(false);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && usesTrigger)
        {
            SendPopupStart();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && usesTrigger)
        {
            SendPopupClose();
        }
    }

    public void SetImage(GameObject image)
    {
        displayImages.Clear();
        displayImages.Add(image);
    }

    public void AddImage(GameObject image)
    {
        displayImages.Add(image);
    }

    public void SendPopupStart()
    {
        dialogueStartEvent.Raise();
        professor.SetPopup(this);
        displayImages[currentIndex].SetActive(true);
        professor.StartPopup(expression);

        if (expires)
        {
            StartCoroutine(StartExpiration(expirationTime));
        }
    }

    public void NextPopup()
    {
        if (canBeSkipped) {
            currentIndex += 1;
            if (currentIndex >= displayImages.Count)
            {
                currentIndex -= 1;
                if (canBeFinished)
                {
                    SendPopupClose();
                }
                else
                {
                    displayImages[currentIndex].SetActive(true);
                }
            }
            else
            {
                displayImages[currentIndex-1].SetActive(false);
                displayImages[currentIndex].SetActive(true);
            }
        }
    }

    public void SendPopupClose()
    {
        displayImages[currentIndex].SetActive(false);
        professor.ClosePopup();
        StartCoroutine(WaitForCloseCO());
    }

    IEnumerator WaitForCloseCO()
    {
        displayImages[currentIndex].SetActive(true);
        while (!profAnimation.GetCurrentAnimatorStateInfo(0).IsName("Hide"))
        {
            yield return null;
        }
        displayImages[currentIndex].SetActive(false);
        dialogueEndEvent.Raise();
        currentIndex = 0;
        
        if (destroyOnFinish)
        {
            Destroy(objectToDestroy);
        }

        yield break;
    }

    IEnumerator StartExpiration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SendPopupClose();
        yield break;
    }
}
