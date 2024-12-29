using System;
using System.Collections;
using System.Collections.Generic;
using TictTackGame.Act;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        GameActions.levelSelect += GameSelectAction;
    }

    private void GameSelectAction(GameType type)
    {
        PageNavigator.Instance.NavAction(PageSet.Game);
    }
}
