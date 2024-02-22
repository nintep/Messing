using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{

    public GameObject MessageContainer;

    public GameObject LogMessagePrefab;
    public GameObject WarningMessagePrefab;

    private DebugMessage LastMessage;

    public bool TestInputsActive = false;

    private void Awake()
    {
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
        LastMessage = newMessage.GetComponent<DebugMessage>();
        LastMessage.isLatestMessage = true;

        LastMessage.SetText(message);
    }

}
