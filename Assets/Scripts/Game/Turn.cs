using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITurn
{
    public void PlayerSelect(PlayerSet playerSet);
}

[RequireComponent(typeof(Button))]
public class Turn : MonoBehaviour
{
    [SerializeField] PlayerSet playerSet;

    Button button;

    public ITurn callback;

    /// <summary>
    /// Action implemented on enable
    /// </summary>
    private void OnEnable()
    {
        if(button == null)
         button = GetComponent<Button>();

        button.onClick.AddListener(SelectAction);
    }
    
    /// <summary>
    /// Action implemented on disable
    /// </summary>
    private void OnDisable()
    {
        if(button != null)
        button.onClick.RemoveAllListeners();    
    }

    void SelectAction()
    {
        callback.PlayerSelect(playerSet);
    }

    public void EnableInteract(bool enable)
    {
        if (button != null)
            button.interactable = enable;
    }
}
