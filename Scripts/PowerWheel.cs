using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerWheel : MonoBehaviour
{
    private Animator animator;

    [SerializeField] float flipDirection = 1.0f;
    private PlayerSO playerSO;
    [SerializeField] GameObject activeAdaptationIcon;
    [SerializeField] GameObject[] activeAdaptationWheelOrder = new GameObject[5];
    [SerializeField] Image[] activeAdaptationIcons = new Image[5];

    [SerializeField] int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerSO = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().playerSO;

        for (int i = 2; i < activeAdaptationWheelOrder.Length + 2; i += 1)
        {
            activeAdaptationWheelOrder[i-2] = transform.GetChild(i).gameObject;
            activeAdaptationIcons[i - 2] = activeAdaptationIcon.transform.GetChild(i-2).gameObject.GetComponent<Image>();
        }

        UpdateIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExitBool(bool exit)
    {
        animator.SetBool("exit", exit);
    }

    public void SetFlipDirection(float dir)
    {
        flipDirection = dir;
    }

    public void UpdateIndex()
    {
        for (int i = 0; i < activeAdaptationWheelOrder.Length; i += 1)
        {
            if (activeAdaptationWheelOrder[i].GetComponent<Adaptation>().thisAdaptation == playerSO.GetCurrentAdaptation())
            {
                currentIndex = i;
                //this is where icon would first appear
                activeAdaptationIcons[currentIndex].enabled = true;
                break;
            }
        }


    }

    public void NextAdaptation()
    {
        activeAdaptationIcons[currentIndex].enabled = false;

        if (flipDirection > 0)
        {
            currentIndex += 1;

            if (currentIndex > activeAdaptationWheelOrder.Length-1)
            {
                currentIndex = 0;
            }
            while (!playerSO.GetActiveAdaptations().Contains(activeAdaptationWheelOrder[currentIndex].GetComponent<Adaptation>().thisAdaptation))
            {
                currentIndex += 1;
                if (currentIndex > activeAdaptationWheelOrder.Length - 1)
                {
                    currentIndex = 0;
                }
            }
            playerSO.SetActiveADP(activeAdaptationWheelOrder[currentIndex].GetComponent<Adaptation>().thisAdaptation);
        }
        else
        {
            currentIndex -= 1;

            if (currentIndex < 0)
            {
                currentIndex = activeAdaptationWheelOrder.Length - 1;
            }
            while (!playerSO.GetActiveAdaptations().Contains(activeAdaptationWheelOrder[currentIndex].GetComponent<Adaptation>().thisAdaptation))
            {
                currentIndex -= 1;
                if (currentIndex < 0)
                {
                    currentIndex = activeAdaptationWheelOrder.Length - 1;
                }
            }
        }

        playerSO.SetActiveADP(activeAdaptationWheelOrder[currentIndex].GetComponent<Adaptation>().thisAdaptation);
        activeAdaptationIcons[currentIndex].enabled = true;

    }
}
