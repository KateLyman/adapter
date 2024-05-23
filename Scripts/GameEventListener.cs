using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    [SerializeField] GameEventSO eventListen;

    [SerializeField] UnityEvent eventResponse;

    private void OnEnable()
    {
        eventListen.RegisterListener(this);
    }

    private void OnDisable()
    {
        eventListen.DeregisterListener(this);
    }

    public void OnEventRaised()
    {
        eventResponse.Invoke();
    }

}
