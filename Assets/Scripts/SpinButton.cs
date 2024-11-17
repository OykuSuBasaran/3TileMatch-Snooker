using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinButton : MonoBehaviour
{
    public static SpinButton Instance { get; private set; }
    public bool isSpinning = false;
    private bool isSpinPressed = false;
    public AudioSource audioSource;

    private Coroutine[] columnCoroutines = new Coroutine[5];
    //private int[,] tilesOnBoard;
    public float columnDelay = 0.25f;
    public float spawnInterval = 0.15f;
    public float moveDuration = 1f;
    public float destroyYPosition = -1.76f;
    public GameObject[] prefabs;




    // Awake fonksiyonu, bu s�n�f�n �rne�i olu�turulmadan �nce �a�r�l�r
    private void Awake()
    {
        // E�er Instance zaten varsa ve bu instance bu s�n�f de�ilse, o zaman bu nesneyi yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ayn� tipteki ba�ka bir nesneyi sil
        }
        else
        {
            Instance = this; // Bu nesneyi Singleton olarak ayarla
            DontDestroyOnLoad(gameObject); // Bu nesne sahne de�i�se bile yok olmas�n
        }
    }

    private void OnMouseDown()
    {
        transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.InOutQuad);
        audioSource.Play();
        if (!isSpinPressed)
        {
            isSpinning = true;
            StopButton.Instance.isStopped = false;
            Board.Instance.tilesOnBoard = new int[5, 5];
            //Board.Instance.ClearBoard();
        }
        isSpinPressed = true;
    }




    public void Slot()
    {
        for (int i = 0; i < 5; i++)
        {
            columnCoroutines[i] = StartCoroutine(SpawnColumn(i));
        }
        // Her s�tun i�in ayr� bir coroutine ba�lat�yoruz
    }

     private IEnumerator SpawnColumn(int columnIndex)
    //bu blo�a yaln�zca coroutine olu�turulurken giriyor o y�zden while lar bir kez true oldu mu bir daha durmuyor
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float startXPosition = xPosition + columnIndex * (cellSize + Board.Instance.spaceBetweenTiles); // Her s�tunun x pozisyonunu hesapla

        // S�tunlar�n s�rayla ba�lamas� i�in gecikme
        yield return new WaitForSeconds(columnIndex * columnDelay);

        int randomTile = Random.Range(0, prefabs.Length);
        Vector3 spawnPosition = new Vector3(startXPosition, yPosition, 0);
        GameObject instance = Instantiate(prefabs[randomTile], spawnPosition, Quaternion.identity);

        // Prefab'i a�a��ya do�ru hareket ettir ve yok et
        instance.transform.DOMoveY(destroyYPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(instance);
        });

        // Her prefab spawn edilmeden �nce k�sa bir bekleme s�resi
        yield return new WaitForSeconds(spawnInterval);

    }

    
}
