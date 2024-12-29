using System.Collections;
using System.Collections.Generic;
using TictTackGame.Act;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSel : MonoBehaviour
{
    Button button;

    [SerializeField] GameType gameType;

    /// <summary>
    /// Actions implemented on enable
    /// </summary>
    private void OnEnable()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(SelectGame);
    }

    /// <summary>
    /// Actions implemented on disable
    /// </summary>
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Select Game action
    /// </summary>
    void SelectGame()
    {
        GameActions.levelSelect(gameType);
    }
}

public enum GameType {nineTack,sixtheenTack}
