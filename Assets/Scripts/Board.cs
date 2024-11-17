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
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Baþka bir instance varsa yok et
        }
    }

    public int columns = 5;
    public int rows = 5;
    public GameObject backG; //initial konum için
    public float spaceBetweenTiles = 0.1f; //tie arasý
    public List<GameObject> spawnedObjects = new List<GameObject>();
    //public GameObject[,] spawnedObjects = new GameObject[5,5];
    public int[,] tilesOnBoard;


    public float CalculateBoardPlacements(out float initialPointX, out float initialPointY)
    {
        Transform firstChildTransform = backG.transform.GetChild(0);
        float cellSize = (firstChildTransform.localScale.x - (columns - 1) * spaceBetweenTiles) / columns;
        initialPointX = backG.transform.position.x + (cellSize / 2);
        initialPointY = backG.transform.position.y - (cellSize / 2);
        return cellSize;
    }

    /*public void ClearBoard()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }*/


    public GameObject GetTileByPosition(Vector2 mousePosition)
    {
        /*float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);

        int tileIndexY = Convert.ToInt32((yPosition - mousePosition.y) / (cellSize + spaceBetweenTiles));
        int tileIndexX = Convert.ToInt32((mousePosition.x - xPosition) / (cellSize + spaceBetweenTiles));

        Debug.Log("indexler: " + tileIndexX + tileIndexY);
        return spawnedObjects[tileIndexY, tileIndexY];*/


        RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit2D.collider != null)
        {
            GameObject clickedObject = hit2D.collider.gameObject;
            if (clickedObject.GetComponent<Tile>() != null)
            {
                Debug.Log("Týklanan obje: " + clickedObject.name);
                return clickedObject;
            }
        }
        return null;
        
    }
}