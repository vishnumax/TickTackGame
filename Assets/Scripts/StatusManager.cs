using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public interface IStat
{
    public void PlayAction();
    public void HomeAction();
}

public class StatusManager : MonoBehaviour
{
    static StatusManager instance;  
    public static StatusManager Instance { get { return instance; } }

    [SerializeField] TMP_Text statusMessage;

    [Space]
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text messageText;

    [Header("Buttons:")]
    [SerializeField] Button playBtn;
    [SerializeField] Button homeBtn;

    public IStat callback;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playBtn.onClick.AddListener(Play);
        homeBtn.onClick.AddListener(Home);
    }

    public void Message(string message)
    {
        statusMessage.text = message;
    }

    public void EnableMenu(bool enable,string message)
    {
        panel.SetActive(enable);
        messageText.text = message; 
    }

    void Home()
    {
        callback.HomeAction();
        panel.SetActive(false); 
    }

    void Play()
    {
        callback.PlayAction();
        panel.SetActive(false); 
    }
}
