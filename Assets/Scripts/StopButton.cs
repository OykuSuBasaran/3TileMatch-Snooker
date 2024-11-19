using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
public class StopButton : MonoBehaviour
{
    public static StopButton Instance { get; private set; }
    private float dropDuration = 1.25f;
    public bool isStopped = false;
    public bool isStopPressed = false;

    public GameObject[] prefabs;
    private int[,] tilesOnBoard = new int[5,5];
    public AudioSource audioSource;
    public GameObject cue;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; // This object is Singleton
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cue.transform.DOMoveX(cue.transform.position.x - 0.80f, 0.24f).SetLoops(2, LoopType.Yoyo));
        sequence.Join(transform.DORotate(new Vector3(0, 0, 360), 1.7f, RotateMode.FastBeyond360)
                 .SetEase(Ease.InOutQuad)); //starting button sprite animation when clickked 

        audioSource.Play(); //playing an audio
        if (!isStopPressed) //if the stop button is not pressed it can work
        {
            isStopped = true; //checking for the button manager
            DecideTiles(); //when stop button is pressed, the last state of tiles on the board will be decided
            SpinButton.Instance.isSpinning = false; //updating spin button's state for the button manager
            SpinButton.Instance.isSpinPressed = false; //making spin button reusable
            //DOTween.To(() => dropDuration, x => dropDuration = x, dropDuration * 1.2f, 3f); //when stopping, add tiles a slowing illusion 
            StopSlot();
        }
        isStopPressed = true; //update flag, if stop button pressed it can not be used again till spin button is used again

    }

    public void DecideTiles() //This function ensures that the gameboard has each tile prefab at least three times by adjusting the first 21
    //Also making sure that there are no consecutiveness more than two, both horizontal and vertical
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

    public bool IsConsecutive(int a, int b, int randomindex) //Checking if there are any consecutiveness by checking what's one and two before
    {
        if (a >= 2 && tilesOnBoard[a - 1, b] == randomindex && tilesOnBoard[a - 2, b] == randomindex)
            return true;

        if (b >= 2 && tilesOnBoard[a, b - 1] == randomindex && tilesOnBoard[a, b - 2] == randomindex)
            return true;

        return false;
    }


    private void StopSlot() //Starting coroutine for each column, in this example it's 5
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(LastInstantiation(i));
        }
    }

    private IEnumerator LastInstantiation(int columnIndex) //Coroutine for decided tiles to settle in their cells with slotlike animation
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        int rows = Board.Instance.rows;

        float columnDelay = 0.5f;
        float lastLine = yPosition - (4 * cellSize + 4 * spaceBetweenTiles);
        float spawnInterval = 0.25f;

        xPosition += columnIndex * (cellSize + spaceBetweenTiles); //Prefab's x position when instantiating them, depending on columnIndex parameter
        yield return new WaitForSeconds(columnIndex * columnDelay);
        for (int j = rows - 1; j >= 0; j--) //rows, in this example we have 5
        {
            GameObject spawnedObject = Instantiate(prefabs[tilesOnBoard[j, columnIndex]], new Vector3(xPosition, yPosition + (cellSize + spaceBetweenTiles), 0), Quaternion.identity);
            Board.Instance.spawnedObjects.Add(spawnedObject);

            // Target position
            Vector3 targetPosition = new Vector3(xPosition, lastLine, 0);

            // Fall animation with DoMove 
            spawnedObject.transform.DOMove(targetPosition, dropDuration).SetEase(Ease.Linear);
            //Updating y position by adding offset every iteration since the tiles fall from above and stop at the last row
            lastLine += (cellSize + spaceBetweenTiles);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}