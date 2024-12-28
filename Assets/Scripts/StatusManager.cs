using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;

public class StatusManager : MonoBehaviour
{
    static StatusManager instance;  
    public static StatusManager Instance { get { return instance; } }

    [SerializeField] TMP_Text statusMessage;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Message(string message)
    {
        statusMessage.text = message;
    }
}
