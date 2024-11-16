using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
public class StopButton : MonoBehaviour
{
    public static StopButton Instance { get; private set; }
    public float moveDuration = 1f;

    public bool isStopped = false;
    private bool isStopPressed = false;

    public GameObject[] prefabs;
    private int[,] tilesOnBoard = new int[5,5];


    // Awake fonksiyonu, bu sýnýfýn örneði oluþturulmadan önce çaðrýlýr
    private void Awake()
    {
        // Eðer Instance zaten varsa ve bu instance bu sýnýf deðilse, o zaman bu nesneyi yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ayný tipteki baþka bir nesneyi sil
        }
        else
        {
            Instance = this; // Bu nesneyi Singleton olarak ayarla
            DontDestroyOnLoad(gameObject); // Bu nesne sahne deðiþse bile yok olmasýn
        }
    }

    public void DecideTiles()
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

    public bool IsConsecutive(int a, int b, int randomindex)
    {
        if (a >= 2 && tilesOnBoard[a - 1, b] == randomindex && tilesOnBoard[a - 2, b] == randomindex)
            return true;

        if (b >= 2 && tilesOnBoard[a, b - 1] == randomindex && tilesOnBoard[a, b - 2] == randomindex)
            return true;

        return false;
    }


    /*public void InstantiateTiles()
    {

        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float tempStartX = xPosition;
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        int columns = Board.Instance.columns;
        int rows = Board.Instance.rows;
        float dropDuration = 1f;
        float columnDelay = 0.5f;
        float altsatir = yPosition - (4*cellSize + 4*spaceBetweenTiles);

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            { 
                GameObject spawnedObject = Instantiate(prefabs[tilesOnBoard[i, j]], new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                spawnedObjects.Add(spawnedObject);

                // Hedef pozisyon, griddeki gerçek yeridir
                Vector3 targetPosition = new Vector3(xPosition, altsatir, 0);

                // DOTween ile düþme animasyonunu uygula
                spawnedObject.transform.DOMove(targetPosition, dropDuration).SetEase(Ease.Linear).SetDelay((i * columnDelay) + j * 0.1f);

                // Sýradaki tile için x pozisyonunu güncelle
                xPosition += cellSize + spaceBetweenTiles;
            }
            xPosition = tempStartX;
            altsatir += (cellSize + spaceBetweenTiles);
        }*/

    private IEnumerator LastInstantiation(int columnIndex)
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float tempStartX = xPosition;
        float tempStartY = yPosition;
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        int columns = Board.Instance.columns;
        int rows = Board.Instance.rows;
        float dropDuration = 1.25f;
        float columnDelay = 0.5f;
        float altsatir = yPosition - (4 * cellSize + 4 * spaceBetweenTiles);
        float altsatirTemp = altsatir;
        float spawnInterval = 0.25f;

        xPosition += columnIndex * (cellSize + spaceBetweenTiles);
        yield return new WaitForSeconds(columnIndex * columnDelay);
            for (int j = 4; j >= 0; j--) //row
            {
                GameObject spawnedObject = Instantiate(prefabs[tilesOnBoard[j, columnIndex]], new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                Board.Instance.spawnedObjects.Add(spawnedObject);

                // Hedef pozisyon, griddeki gerçek yeridir
                Vector3 targetPosition = new Vector3(xPosition, altsatir, 0);

                // DOTween ile düþme animasyonunu uygula
                spawnedObject.transform.DOMove(targetPosition, dropDuration).SetEase(Ease.Linear);
                // Sýradaki tile için x pozisyonunu güncelle
                altsatir += (cellSize + spaceBetweenTiles);
                yield return new WaitForSeconds(spawnInterval);
            }

    }

    private void StopSlot()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(LastInstantiation(i));
        }
    }
    /*for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            GameObject spawnedObject = Instantiate(prefabs[tilesOnBoard[i, j]], new Vector3(xPosition, yPosition, 0), Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
            xPosition += cellSize + spaceBetweenTiles;
        }
        xPosition = tempStartX;
        yPosition -= (cellSize + spaceBetweenTiles);
    }*/



    private void OnMouseDown()
    {
        if (!isStopPressed)
        {
            isStopped = true;
            DecideTiles();
            SpinButton.Instance.isSpinning = false;
            DOTween.To(() => moveDuration, x => moveDuration = x, moveDuration * 1.2f, 3f);
            StopSlot();
        }
        isStopPressed = true;
        
    }

}
