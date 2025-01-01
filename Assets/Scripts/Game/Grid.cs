using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.Tilemaps;
using System;
using TictTackGame.Act;
using Random = UnityEngine.Random;
using UnityEngine.WSA;

public class Grid : MonoBehaviour,ITile
{
    [Header("Tiles")]
    [SerializeField] List<Tile> tiles = new List<Tile>();
   
    [SerializeField] PlayerSet currentPlayer;

    [SerializeField] List<Result> results = new List<Result>();

    [Header("Matrix:")]
    [SerializeField] int matrix;


    private void Start()
    {
        results.Clear();

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].callback = this;
        }

        currentPlayer = PlayerSet.first;

        PlayerTurnAction();

        StatusManager.Instance.Message("Player 1 Turn");

        GameActions.RestartAction += RestartAction;
    }

    /// <summary>
    /// Action implemented on turning the tiles 
    /// </summary>
    public void PlayerTurnAction()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetPlayer(currentPlayer);
        }

        //Computer play
        if(currentPlayer == PlayerSet.second)
        {
            Debug.LogWarning("Computer play !!!");

            for(int i=0; i<tiles.Count; i++) { tiles[i].EnableInteract(false); }

            Vector2 pos = Vector2.zero;

            List<Tile> unMarktiles = tiles.Where(x => !x.isMark).ToList();

            int randomIndex = Random.Range(0, unMarktiles.Count);
            
            Tile tile= unMarktiles[randomIndex];
            tile.Mark();
        }
        else
            for (int i = 0; i < tiles.Count; i++) { tiles[i].EnableInteract(true); }

    }

    /// <summary>
    /// Set the tile
    /// </summary>
    /// <param name="playerSet"></param>
    /// <param name="pos"></param>
    public void SetTile(PlayerSet playerSet, Vector2 pos)
    {
        Debug.Log("CurrentPlayer :" + currentPlayer + " Pos: " + pos);

        PlayerSet nextTurn = playerSet == PlayerSet.first ? PlayerSet.second : PlayerSet.first;
        currentPlayer = nextTurn;

        PlayerSelect(nextTurn);


        Result result = new Result
        {
            set = playerSet,
            pos = pos,
        };

        if (results.Contains(result)) return;

        results.Add(result);

        CheckResult(); 
    }

    /// <summary>
    /// Turn the player
    /// </summary>
    /// <param name="playerSet"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void PlayerSelect(PlayerSet playerSet) 
    { 
        currentPlayer = playerSet;

        PlayerTurnAction();

        string message = playerSet switch
        {
            PlayerSet.first => "Player 1 Turn",
            PlayerSet.second => "Player 2 Turn",
            _ => throw new System.NotImplementedException()
        };

        StatusManager.Instance.Message(message);
    }

    #region RESULT
    /// <summary>
    /// Check the result
    /// </summary>
    void CheckResult()
    {
        if (results.Count == 0) return;

        List<Result> results_1 = new List<Result>();
        List<Result> results_2 = new List<Result>();

        foreach (Result result in results)
        {
            if(result.set == PlayerSet.first)
                results_1.Add(result);

            if (result.set == PlayerSet.second)
                results_2.Add(result);
        }

        Debug.LogWarning("First player count: " + results_1.Count);

        //Check Player one Result
        if (results_1.Count >=3)
        {
            results_1 = results_1.TakeLast(3).ToList();

            Vector2 firstVector = results_1[0].pos;

            bool isWin = false;

            isWin = results_1.All(x => x.pos.x == firstVector.x);
            if (isWin)
            {    
                ShowResult(PlayerSet.first, results_1);
                return;
            }

            isWin = results_1.All(x => x.pos.y == firstVector.y);
            if (isWin)
            {
                ShowResult(PlayerSet.first, results_1);
                return;
            }

            int dSum = matrix - 1;
            for (int i = 0; i < results_1.Count; i++)
            {
                isWin = results_1[i].pos.x + results_1[i].pos.y == dSum;

                if(!isWin)
                    isWin = results_1[i].pos.x == results_1[i].pos.y;

                if (!isWin)
                    break;
            }

            for (int i = 0; i < results_1.Count; i++)
            {
                isWin = results_1[i].pos.x == results_1[i].pos.y;

                if (!isWin)
                    break;
            }

            if (isWin)
            {
                ShowResult(PlayerSet.first, results_1);
                return;
            }
        }

        //Check player two result
        if (results_2.Count >= 3)
        {
            results_2 = results_2.TakeLast(3).ToList();

            Vector2 firstVector = results_2[0].pos;

            bool isWin = false;

            isWin = results_2.All(x => x.pos.x == firstVector.x);
            if (isWin)
            {
                ShowResult(PlayerSet.second, results_2);
                return;
            }

            isWin = results_2.All(x => x.pos.y == firstVector.y);
            if (isWin)
            {
                ShowResult(PlayerSet.second, results_2);
                return;
            }

            int dSum = matrix - 1;
            for (int i = 0; i < results_2.Count; i++)
            {
                isWin = results_2[i].pos.x + results_2[i].pos.y == dSum;

                if (!isWin)
                    break;
            }

            for (int i = 0; i < results_2.Count; i++)
            {
                isWin = results_2[i].pos.x == results_2[i].pos.y;

                if (!isWin)
                    break;
            }


            if (isWin)
            {
                ShowResult(PlayerSet.second, results_2);
                return;
            }
        }
    }


    void ShowResult(PlayerSet playerSet , List<Result> results)
    {
        string message = playerSet switch
        {
            PlayerSet.first => "Player 1 Win",
            PlayerSet.second => "Player 2 Win",
            _ => throw new System.NotImplementedException()
        };

        StatusManager.Instance.EnableMenu(true,message);

        foreach(Result result in results)
        {
           Tile tile = tiles.Find(x => x.Position == result.pos);

            if(tile != null)
            {
                tile.WinAction();
            }
        }

        GameActions.StopAction();
    }
    #endregion

    void RestartAction()
    {
        results.Clear();

        currentPlayer = PlayerSet.first;

        PlayerTurnAction();

        StatusManager.Instance.Message("Player 1 Turn");

    }
}

[System.Serializable]
public class Result
{
    public PlayerSet set;
    public Vector2 pos;
}
