using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfessorController : MonoBehaviour
{
    public enum PEX { talkEyesOpen, talkEyesClosed, happy, surprise, deadpan };

    private Animator popupAnimator;
    [SerializeField] GameObject professor;
    [SerializeField] Popups popup;

    [SerializeField] Sprite happyProf;
    [SerializeField] Sprite surpriseProf;
    [SerializeField] Sprite deadpanProf;

    // Start is called before the first frame update
    void Start()
    {
        popupAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPopup(PEX expression)
    {
        if (popupAnimator == null)
        {
            popupAnimator = GetComponent<Animator>();
        }

        popupAnimator.Play("Enter");
        popupAnimator.SetBool("exit", false);

        SetProfessor(expression);
    }

    private void SetProfessor(PEX expression)
    {
        if (expression == PEX.talkEyesOpen)
        {
            professor.GetComponent<Animator>().Play("TalkEyesOpen");
        }
        else if (expression == PEX.talkEyesClosed)
        {
            professor.GetComponent<Animator>().Play("TalkEyesClose");
        }
        else
        {
            professor.GetComponent<Animator>().StopPlayback();
            
            if (expression == PEX.happy)
            {
                professor.GetComponent<Image>().sprite = happyProf;
            }
            else if (expression == PEX.surprise)
            {
                professor.GetComponent<Image>().sprite = surpriseProf;
            }
            else
            {
                professor.GetComponent<Image>().sprite = deadpanProf;
            }
        }
    }

    public void SetPopup(Popups inputPopup)
    {
        popup = inputPopup;
    }

    public void NextDialogue()
    {
        popup.NextPopup();
    }

    public void ClosePopup()
    {
        popupAnimator.SetBool("exit", true);
    }
}
