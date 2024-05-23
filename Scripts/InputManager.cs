using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    // Global scriptable object assigment
    [SerializeField] LogbookSO logbook;
    [SerializeField] PlayerSO player;

    // Pizza
    [SerializeField] findCreatures3 creatureFinder;

    // Actions
    public InputActionAsset inputActions;

    // Wheel

    [SerializeField] PowerWheel powerWheel;

    private InputAction moveAction;
    private InputAction aimAction;
    private InputAction scanAction;

    private InputAction logbookOpenAction;
    private InputAction logbookCloseAction;
    private InputAction logbookFlipAction;

    private InputAction wheelOpenAction;
    private InputAction wheelCloseAction;
    private InputAction wheelFlipAction;
    private InputAction wheelCloseRunAction;

    private InputAction activeAdaptationStartAction;

    private InputAction pauseStartAction;
    private InputAction pauseEndAction;

    private InputAction helpStartAction;
    private InputAction helpEndAction;

    private InputAction dialogueNextAction;

    private InputAction resetAction;

    // Events
    [SerializeField] GameEventSO aimStartEvent;
    [SerializeField] GameEventSO aimEndEvent;
    [SerializeField] GameEventSO scanStartEvent;
    [SerializeField] GameEventSO scanSuccessEvent;
    [SerializeField] GameEventSO scanFailEvent;

    [SerializeField] GameEventSO logbookOpenEvent;
    [SerializeField] GameEventSO logbookCloseEvent;
    [SerializeField] GameEventSO logbookFlipEvent;

    [SerializeField] GameEventSO wheelOpenEvent;
    [SerializeField] GameEventSO wheelCloseEvent;
    [SerializeField] GameEventSO wheelFlipEvent;

    [SerializeField] GameEventSO pauseStartEvent;
    [SerializeField] GameEventSO pauseEndEvent;

    [SerializeField] GameEventSO helpStartEvent;
    [SerializeField] GameEventSO helpEndEvent;

    [SerializeField] GameEventSO activeAdaptationStartEvent;

    [SerializeField] GameEventSO dialogueNextEvent;

    // Local variables
    private bool isAiming = false;
    private bool isScanning = false;
    private bool hasAddedScanStart = false;

    void Awake()
    {
        Debug.Log("waking");
        moveAction = inputActions.FindActionMap("gameplay").FindAction("move");
        aimAction = inputActions.FindActionMap("gameplay").FindAction("aim");
        scanAction = inputActions.FindActionMap("gameplay").FindAction("scan");

        logbookOpenAction = inputActions.FindActionMap("gameplay").FindAction("logbook");
        logbookCloseAction = inputActions.FindActionMap("logbook").FindAction("exit");
        logbookFlipAction = inputActions.FindActionMap("logbook").FindAction("flip");

        wheelOpenAction = inputActions.FindActionMap("gameplay").FindAction("powerwheel");
        wheelCloseAction = inputActions.FindActionMap("powerwheel").FindAction("exit");
        wheelFlipAction = inputActions.FindActionMap("powerwheel").FindAction("flip");
        wheelCloseRunAction = inputActions.FindActionMap("powerwheel").FindAction("run");

        pauseStartAction = inputActions.FindActionMap("gameplay").FindAction("pause");
        pauseEndAction = inputActions.FindActionMap("pause").FindAction("return");

        helpStartAction = inputActions.FindActionMap("gameplay").FindAction("help");
        helpEndAction = inputActions.FindActionMap("help").FindAction("exit");

        activeAdaptationStartAction = inputActions.FindActionMap("gameplay").FindAction("adaptation");

        dialogueNextAction = inputActions.FindActionMap("gameplay").FindAction("dialogueNext");

        //resetAction = inputActions.FindActionMap("debug").FindAction("reset");

        ClearFunctions();
        AddFunctions();

        DisableScan();
        Debug.Log("Disabled scan!");
    }

    public void AddFunctions()
    {
        aimAction.performed += OnAimStart;
        aimAction.canceled += OnAimEnd;

        logbookOpenAction.performed += OnLogbookOpen;
        logbookCloseAction.performed += OnLogbookClose;
        logbookFlipAction.performed += OnLogbookFlip;

        wheelOpenAction.performed += OnWheelOpen;
        wheelCloseAction.performed += OnWheelClose;
        wheelFlipAction.performed += OnWheelFlip;
        wheelCloseRunAction.performed += OnWheelCloseRun;

        pauseStartAction.performed += OnPauseStart;
        pauseEndAction.performed += OnPauseEnd;

        helpStartAction.performed += OnHelpStart;
        helpEndAction.performed += OnHelpEnd;

        activeAdaptationStartAction.performed += OnActiveAdaptationStart;

        dialogueNextAction.performed += OnDialogueNext;

        DisableDialogue();

        //resetAction.performed += OnReset;
    }

    public void ClearFunctions()
    {
        aimAction.performed -= OnAimStart;
        aimAction.canceled -= OnAimEnd;

        logbookOpenAction.performed -= OnLogbookOpen;
        logbookCloseAction.performed -= OnLogbookClose;
        logbookFlipAction.performed -= OnLogbookFlip;

        wheelOpenAction.performed -= OnWheelOpen;
        wheelCloseAction.performed -= OnWheelClose;
        wheelFlipAction.performed -= OnWheelFlip;
        wheelCloseRunAction.performed -= OnWheelCloseRun;

        pauseStartAction.performed -= OnPauseStart;
        pauseEndAction.performed -= OnPauseEnd;

        helpStartAction.performed -= OnHelpStart;
        helpEndAction.performed -= OnHelpEnd;

        activeAdaptationStartAction.performed -= OnActiveAdaptationStart;

        dialogueNextAction.performed -= OnDialogueNext;

        //resetAction.performed -= OnReset;
    }

    public void DisableMovement()
    {
        moveAction.Disable();
    }

    public void EnableMovement()
    {
        moveAction.Enable();
    }

    public void DisableLogbook()
    {
        logbookOpenAction.Disable();
    }

    public void EnableLogbook()
    {
        logbookOpenAction.Enable();
    }

    public void DisableWheel()
    {
        wheelOpenAction.Disable();
    }

    public void EnableWheel()
    {
        wheelOpenAction.Enable();
    }

    public void DisableHelp()
    {

    }

    public void EnableHelp()
    {

    }

    public void DisableDialogue()
    {
        dialogueNextAction.Disable();
    }

    public void EnableDialogue()
    {
        dialogueNextAction.Enable();
    }

    public void DisableActiveAdaptation()
    {
        activeAdaptationStartAction.Disable();
    }

    public void EnableActiveAdaptation()
    {
        activeAdaptationStartAction.Enable();
    }

    public void DialogueFunctionStart()
    {
        EnableDialogue();

        DisableActiveAdaptation();
        DisableLogbook();
        DisableWheel();
        DisableHelp();
    }

    public void DialogueFunctionEnd()
    {
        DisableDialogue();

        EnableActiveAdaptation();
        EnableLogbook();
        EnableWheel();
        EnableHelp();
    }

    public void AddWheelFunctions()
    {
        wheelOpenAction.performed += OnWheelOpen;
        wheelCloseAction.performed += OnWheelClose;

        activeAdaptationStartAction.performed += OnActiveAdaptationStart;
    }

    public void ClearWheelFunctions()
    {
        wheelOpenAction.performed -= OnWheelOpen;
        wheelCloseAction.performed -= OnWheelClose;

        activeAdaptationStartAction.performed -= OnActiveAdaptationStart;
    }

    // Update is called once per frame
    void Update()
    {

        if (creatureFinder.thisCreature != null && !isScanning && !hasAddedScanStart)
        {
            scanAction.started += OnScanStart;
            hasAddedScanStart = true;

            // Hey Kate! If you want to fix this, make sure that you change the way the InputManager is interpreting the scan button! Turn the scan into a Coroutine and not through the Input Manager,
            // because it registers as you having held the button whenever you started pressing the button (since it is on the hold mode)

            /*
            if (scanAction.ReadValue<float>() == 1.0f)
            {
                scanStartEvent.Raise();
                scanAction.started -= OnScanStart;
                hasAddedScanStart = false;
            }
            */
        }
        if (creatureFinder.thisCreature == null && hasAddedScanStart)
        {
            scanAction.started -= OnScanStart;
            hasAddedScanStart = false;
        }
    }

    public Vector2 GetAimAngle()
    {
        return Vector2.zero;
    }

    public Vector3 GetMovement()
    {
        // Implicitly convert value of moveAction to Vector3
        Vector3 currentMove = moveAction.ReadValue<Vector2>();
        return currentMove;
    }

    void OnLogbookOpen(InputAction.CallbackContext context)
    {
        logbookOpenEvent.Raise();
        inputActions.FindActionMap("logbook").Enable();
        inputActions.FindActionMap("gameplay").Disable();
    }

    void OnLogbookClose(InputAction.CallbackContext context)
    {
        logbookCloseEvent.Raise();
        inputActions.FindActionMap("gameplay").Enable();
        inputActions.FindActionMap("logbook").Disable();
        DisableScan();
    }

    void OnLogbookFlip(InputAction.CallbackContext context)
    {
        float flipDirection = logbookFlipAction.ReadValue<float>();
        logbook.SetFlipDirection(flipDirection);
        logbookFlipEvent.Raise();
    }

    public bool IsScanning()
    {
        return isScanning;
    }
    public bool IsAiming()
    {
        return isAiming;
    }

    void OnScanStart(InputAction.CallbackContext context)
    {
        scanStartEvent.Raise();
        isScanning = true;
        scanAction.started -= OnScanStart;
    }

    void OnScanSuccess(InputAction.CallbackContext context)
    {
        scanAction.performed -= OnScanSuccess;
        scanAction.canceled -= OnScanFail;
        scanSuccessEvent.Raise();
        isScanning = false;
        isAiming = false;
    }

    void OnScanFail(InputAction.CallbackContext context)
    {
        scanFailEvent.Raise();
        scanAction.performed -= OnScanSuccess;
        scanAction.canceled -= OnScanFail;
        isScanning = false;
        isAiming = false;
    }

    void OnAimStart(InputAction.CallbackContext context)
    {
        aimStartEvent.Raise();

        isAiming = true;
    }

    void OnAimEnd(InputAction.CallbackContext context)
    {
        aimEndEvent.Raise();
        scanAction.performed -= OnScanSuccess;
        scanAction.canceled -= OnScanFail;
        scanAction.started -= OnScanStart;

        isAiming = false;
    }

    void OnWheelOpen(InputAction.CallbackContext callback)
    {
        wheelOpenEvent.Raise();
        inputActions.FindActionMap("powerwheel").Enable();
        inputActions.FindActionMap("gameplay").Disable();
    }

    void OnWheelClose(InputAction.CallbackContext callback)
    {
        wheelCloseEvent.Raise();
        inputActions.FindActionMap("powerwheel").Disable();
        inputActions.FindActionMap("gameplay").Enable();
        DisableScan();
    }

    void OnWheelFlip(InputAction.CallbackContext context)
    {
        if (player.GetActiveAdaptations().Count > 0)
        {
            float flipDirection = wheelFlipAction.ReadValue<float>();
            powerWheel.SetFlipDirection(flipDirection);
            wheelFlipEvent.Raise();
        }
    }

    void OnWheelCloseRun(InputAction.CallbackContext callback)
    {
        wheelCloseEvent.Raise();
        activeAdaptationStartEvent.Raise();
        inputActions.FindActionMap("powerwheel").Disable();
        inputActions.FindActionMap("gameplay").Enable();
        DisableScan();
    }

    void OnPauseStart(InputAction.CallbackContext context)
    {
        pauseStartEvent.Raise();
    }

    void OnPauseEnd(InputAction.CallbackContext context)
    {
        pauseEndEvent.Raise();
    }

    void OnHelpStart(InputAction.CallbackContext context)
    {
        helpStartEvent.Raise();
        inputActions.FindActionMap("gameplay").Disable();
        inputActions.FindActionMap("help").Enable();
    }

    void OnHelpEnd(InputAction.CallbackContext context)
    {
        helpEndEvent.Raise();
        inputActions.FindActionMap("help").Disable();
        inputActions.FindActionMap("gameplay").Enable();
    }

    void OnActiveAdaptationStart(InputAction.CallbackContext callback)
    {
        if (player.GetActiveAdaptations().Count > 0)
        {
            Debug.Log("ACTIVE TIME");
            activeAdaptationStartEvent.Raise();
        }

    }

    void OnDialogueNext(InputAction.CallbackContext callback)
    {
        dialogueNextEvent.Raise();
    }

    void OnReset(InputAction.CallbackContext callback)
    {
        foreach (CreatureSO creature in Resources.LoadAll<CreatureSO>("Creatures"))
        {
            creature.UnScan();
        }

        player.ResetPlayer();
        
        SceneManager.LoadScene(0);
    }

    public void ClearScanActions()
    {
        scanAction.performed -= OnScanSuccess;
        scanAction.canceled -= OnScanFail;
        scanAction.started -= OnScanStart;
    }

    public void AddScanActions()
    {
        scanAction.performed += OnScanSuccess;
        scanAction.canceled += OnScanFail;
        scanAction.started += OnScanStart;
    }

    public void EnableScan()
    {
        scanAction.Enable();
    }

    public void DisableScan()
    {
        Debug.Log("SCAN DISABLED!!");
        scanAction.Disable();
    }

    public void ScanVariableTrue()
    {
        isScanning = true;
    }

    public void ScanVariableFalse()
    {
        isScanning = false;
    }

    public void PauseAddMap()
    {
        inputActions.FindActionMap("gameplay").Disable();
        inputActions.FindActionMap("pause").Enable();
        Debug.Log("WE'RE PAUSED!");
    }

    public void PauseRemoveMap()
    {
        inputActions.FindActionMap("pause").Disable();
        inputActions.FindActionMap("gameplay").Enable();
        Debug.Log("WE'RE BACK");
    }

    void OnEnable()
    {
        inputActions.FindActionMap("gameplay").Enable();
        //inputActions.FindActionMap("gameplay").FindAction("aimAngle").Disable();

        inputActions.FindActionMap("debug").Enable();
    }

    void OnDisable()
    {
        ClearFunctions();
        inputActions.FindActionMap("gameplay").Disable();

        inputActions.FindActionMap("debug").Disable();
        Debug.Log("INPUT MANAGER DISABLED");
    }

}
