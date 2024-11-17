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
        // Her sütun için ayrý bir coroutine baþlatýyoruz
    }

     private IEnumerator SpawnColumn(int columnIndex)
    //bu bloða yalnýzca coroutine oluþturulurken giriyor o yüzden while lar bir kez true oldu mu bir daha durmuyor
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float startXPosition = xPosition + columnIndex * (cellSize + Board.Instance.spaceBetweenTiles); // Her sütunun x pozisyonunu hesapla

        // Sütunlarýn sýrayla baþlamasý için gecikme
        yield return new WaitForSeconds(columnIndex * columnDelay);

        int randomTile = Random.Range(0, prefabs.Length);
        Vector3 spawnPosition = new Vector3(startXPosition, yPosition, 0);
        GameObject instance = Instantiate(prefabs[randomTile], spawnPosition, Quaternion.identity);

        // Prefab'i aþaðýya doðru hareket ettir ve yok et
        instance.transform.DOMoveY(destroyYPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(instance);
        });

        // Her prefab spawn edilmeden önce kýsa bir bekleme süresi
        yield return new WaitForSeconds(spawnInterval);

    }

    
}
