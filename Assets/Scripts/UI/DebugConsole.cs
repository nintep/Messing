using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugConsole : MonoBehaviour
{

    public GameObject MessageContainer;

    public GameObject LogMessagePrefab;
    public GameObject WarningMessagePrefab;

    private DebugMessage LastMessage;

    public bool TestInputsActive = false;

    private void Awake()
    {
        #if UNITY_EDITOR
        #else
            TestInputsActive = false;
        #endif

    }

    private void Start()
    {
        AddMessage("InvalidOperationException: Could not complete function HideDebugger(true)", true);
        AddMessage("game started", false);
    }

    private void Update()
    {
        if (TestInputsActive)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddMessage("NullReferenceException: Object reference not set to an instance of an object", true);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                AddMessage("Trying the debuger - add message", false);
            }
        }
    }

    public void AddMessage(string message, bool isWarning)
    {
        GameObject prefab = isWarning ? WarningMessagePrefab : LogMessagePrefab;
        GameObject newMessage = GameObject.Instantiate(prefab, transform.position, transform.rotation);
        newMessage.transform.SetParent(MessageContainer.transform, false);

        if (LastMessage != null) LastMessage.isLatestMessage = false;
        if (LastMessage != null) LastMessage.timerRunning = true;

        LastMessage = newMessage.GetComponent<DebugMessage>();
        LastMessage.isLatestMessage = true;
        LastMessage.timerRunning = false;

        string timeStamp = System.DateTime.Now.ToString("HH:mm");
        string messageWithTimeStamp = String.Format("[{0}] {1}", timeStamp, message);

        LastMessage.SetText(messageWithTimeStamp);
    }

}
