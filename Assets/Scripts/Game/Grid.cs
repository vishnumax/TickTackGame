using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] List<Tile> tiles = new List<Tile>();

    private void OnEnable()
    {
       
    }
}
