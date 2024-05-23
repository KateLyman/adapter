using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SkinSelector : MonoBehaviour
{
    private int index = 0;
    [SerializeField] PlayerSO playerSO;
    [SerializeField] GameEventSO startEvent;

    [SerializeField] InputActionAsset inputActions;
    private InputAction flipAction;
    private InputAction startAction;

    private AudioSource thisAudio;

    private bool freezeSelection = false;

    // Start is called before the first frame update
    void Start()
    {
        index = playerSO.GetSkin();
        thisAudio = GetComponent<AudioSource>();
        UpdateIndex();

        flipAction = inputActions.FindActionMap("character").FindAction("flip");
        startAction = inputActions.FindActionMap("character").FindAction("start");

        flipAction.performed += OnCharacterFlip;
        startAction.performed += OnStartAction;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCharacterFlip(InputAction.CallbackContext context)
    {
        if (flipAction.ReadValue<float>() > 0)
        {
            NextRight();
        }
        else
        {
            NextLeft();
        }
    }

    void OnStartAction(InputAction.CallbackContext context)
    {
        startEvent.Raise();
    }

    public void FreezeSelection()
    {
        freezeSelection = true;
    }

    public void NextLeft()
    {
        if (!freezeSelection)
        {
            Debug.Log("left");
            index -= 1;
            thisAudio.Play();
            UpdateIndex();
        }
    }

    public void NextRight()
    {
        if (!freezeSelection)
        {
            Debug.Log("right");
            index += 1;
            thisAudio.Play();
            UpdateIndex();
        }
    }

    public void UpdateIndex()
    {
        if (index >= transform.childCount)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = transform.childCount - 1;
        }

        for (int i = 0; i < transform.childCount; i += 1)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
            }
            else
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().enabled = false;
            }
        }

        playerSO.SetSkin(index);
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("character").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("character").Disable();
    }
}
