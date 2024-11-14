using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEditorInternal;

public class Try : MonoBehaviour
{
    public GameObject[] prefabs;
    private int[,] tilesOnBoard;
    List<GameObject> spawnedObjects = new List<GameObject>();
    private Coroutine[] columnCoroutines = new Coroutine[5];
    public float columnDelay = 0.5f;
    public float spawnInterval = 0.25f;
    public float moveDuration = 1f;
    public float destroyYPosition = -2.15f;
    private bool shouldSpin;
    private bool isSpinning = false;
    private bool stopTusu = false;
    private bool spinTusu = false;
    void InstantiateTiles()
    {

        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float tempStartX = xPosition;
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        int columns = Board.Instance.columns;
        int rows = Board.Instance.rows;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject spawnedObject = Instantiate(prefabs[tilesOnBoard[i, j]], new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                spawnedObjects.Add(spawnedObject);
                xPosition += cellSize + spaceBetweenTiles;
            }
            xPosition = tempStartX;
            yPosition -= (cellSize + spaceBetweenTiles);
        }
    }



    private void DecideTiles()
    {
        int[] eachPrefabCount = new int[prefabs.Length];
        int tileCount = 0;

        int columns = Board.Instance.columns;
        int rows = Board.Instance.rows;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int randomIndex = Random.Range(0, prefabs.Length);

                if (eachPrefabCount[randomIndex] < 3 && tileCount < 21 && !IsConsecutive(i, j, randomIndex))
                {
                    tilesOnBoard[i, j] = randomIndex;
                    eachPrefabCount[randomIndex]++;
                    tileCount++;
                }
                else if (tileCount < 21)
                {
                    j--;
                    continue;
                }
                else if (!IsConsecutive(i, j, randomIndex))
                {
                    tilesOnBoard[i, j] = randomIndex;
                    eachPrefabCount[randomIndex]++;
                }
                else
                {
                    j--;
                    continue;
                }
            }
        }
    }

    private bool IsConsecutive(int a, int b, int randomindex)
    {
        if (a >= 2 && tilesOnBoard[a - 1, b] == randomindex && tilesOnBoard[a - 2, b] == randomindex)
            return true;

        if (b >= 2 && tilesOnBoard[a, b - 1] == randomindex && tilesOnBoard[a, b - 2] == randomindex)
            return true;

        return false;
    }
}

