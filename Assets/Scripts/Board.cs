using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //singleton
        }
        else
        {
            Destroy(gameObject); //if there is another object, destroy it
        }
    }

    public int columns = 5;
    public int rows = 5;
    public GameObject backG; //for initial position of the board
    public float spaceBetweenTiles = 0.1f;
    public List<GameObject> spawnedObjects = new List<GameObject>();
    public int[,] tilesOnBoard;


    public float CalculateBoardPlacements(out float initialPointX, out float initialPointY)
    {
        Transform firstChildTransform = backG.transform.GetChild(0);
        float cellSize = (firstChildTransform.localScale.x - (columns - 1) * spaceBetweenTiles) / columns;
        initialPointX = backG.transform.position.x + (cellSize / 2);
        initialPointY = backG.transform.position.y - (cellSize / 2);
        return cellSize;
    }

    public void ClearBoard() //Clear objects on the board since its singleton
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }


    public GameObject GetTileByPosition(Vector2 mousePosition) //get the gameobject by the mouse position
    {
        RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit2D.collider != null)
        {
            GameObject clickedObject = hit2D.collider.gameObject;
            if (clickedObject.GetComponent<Tile>() != null)
            {
                Debug.Log("Clicked Object: " + clickedObject.name);
                return clickedObject;
            }
        }
        return null;        
    }

}