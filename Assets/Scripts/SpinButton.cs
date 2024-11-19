using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinButton : MonoBehaviour
{
    public static SpinButton Instance { get; private set; }
    public bool isSpinning = false;
    public bool isSpinPressed = false;
    public AudioSource audioSource;
    private Coroutine[] columnCoroutines = new Coroutine[5];
    private float columnDelay = 0.50f;
    private float spawnInterval = 0.15f;
    private float moveDuration = 1f;
    private float destroyYPosition = -1.76f;
    public GameObject[] prefabs;
    public GameObject cue;

    private void Awake()
    {
        //SpinButton existingSpinButton = FindObjectOfType<SpinButton>();
        //if (existingSpinButton != null && existingSpinButton != this)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("spin instance destroyed");
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
        sequence.Append(cue.transform.DOMoveX(cue.transform.position.x + 0.80f, 0.24f).SetLoops(2, LoopType.Yoyo));
        sequence.Join(transform.DORotate(new Vector3(0, 0, -360), 1.7f, RotateMode.FastBeyond360)
                 .SetEase(Ease.InOutQuad));//starting button sprite animation when clickked 

        audioSource.Play();//play an audio
        if (!isSpinPressed)//is Spin is not pressed it'll work
        {
            Board.Instance.ClearBoard();//clear the previous prefabs in case of respin 
            isSpinning = true;
            StopButton.Instance.isStopped = false;
            StopButton.Instance.isStopPressed = false;
            Board.Instance.tilesOnBoard = new int[5, 5];
        }
        isSpinPressed = true; //spin button can't be clicked again till the stop button clicked
    }




    public void Slot() //start coroutines for each column
    {
        for (int i = 0; i < 5; i++) //we have 5 column
        {
            columnCoroutines[i] = StartCoroutine(SpawnColumn(i));
        }
    }

     private IEnumerator SpawnColumn(int columnIndex) //coroutine to 
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float startXPosition = xPosition + columnIndex * (cellSize + Board.Instance.spaceBetweenTiles); // Each columns x position

        // Latency between columns to slot illusion
        yield return new WaitForSeconds(columnIndex * columnDelay);

        //Instantiate random prefab
        int randomTile = Random.Range(0, prefabs.Length);
        Vector3 spawnPosition = new Vector3(startXPosition, yPosition, 0);
        GameObject instance = Instantiate(prefabs[randomTile], spawnPosition, Quaternion.identity);

        //Move prefab to the buttom and destroy it 
        instance.transform.DOMoveY(destroyYPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(instance);
        });

        //Spawn interval time between prefabs
        yield return new WaitForSeconds(spawnInterval);

    }

    
}
