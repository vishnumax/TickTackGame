using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITile
{
    public void SetTile(PlayerSet playerSet, Vector2 pos);
}

[RequireComponent(typeof(Button))]
public class Tile : MonoBehaviour
{
    Button button;

    [SerializeField] Image markImage;
    [SerializeField] Sprite playerOne;
    [SerializeField] Sprite playerTwo;

    PlayerSet playerSet;

    [SerializeField] Vector2 pos;

    public ITile callback;


    private void OnEnable()
    {
        if (button == null)
            button = this.GetComponent<Button>();

        button.onClick.AddListener(Mark);

        markImage.sprite = null;
        markImage.enabled = false;
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }


    public void SetPlayer(PlayerSet set)
    {
        if (button == null)
            return;

        playerSet = set;

        markImage.sprite = set switch
        {
            PlayerSet.first => playerOne,
            PlayerSet.second => playerTwo,
            _ => throw new System.NotImplementedException(),
        };
    }

    void Mark()
    {
        markImage.enabled = true;
        button.enabled = false;

        callback.SetTile(playerSet, pos);
    }

}

public enum PlayerSet { first,second}
