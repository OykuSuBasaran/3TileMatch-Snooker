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
    List<GameObject> spawnedObjects = new List<GameObject>();

    public float CalculateBoardPlacements(out float initialPointX, out float initialPointY)
    {
        Transform firstChildTransform = backG.transform.GetChild(0);
        float cellSize = (firstChildTransform.localScale.x - (columns - 1) * spaceBetweenTiles) / columns;
        initialPointX = backG.transform.position.x + (cellSize / 2);
        initialPointY = backG.transform.position.y - (cellSize / 2);
        return cellSize;
    }

    public void ClearBoard()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

}

