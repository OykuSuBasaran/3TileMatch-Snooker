using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;
using System.Runtime.InteropServices.WindowsRuntime;

public class Tile : MonoBehaviour
{

    public enum TileTypes { Blue, Pink, Red, Brown, Yellow, Green, Black } //For comparison
    public TileTypes TileType;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private AudioSource audioSource;

    /*private void OnMouseDown()
    {
        Debug.Log("tiklandi objeye: " +  gameObject.transform.position);
    }*/


    //Vector3 GetTargetPosition(){}

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwipeRight()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);
        Vector2 otherTile = new Vector2(worldStartPosition.x + (cellSize + spaceBetweenTiles), worldStartPosition.y);
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile);
        float offset = cellSize + spaceBetweenTiles;
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.x;
            float tempXPosition = gameObject.transform.position.x;
            // Objeyi saða kaydýr
            Debug.Log("ana obje pozisyonu once: " + gameObject.transform.position);
            gameObject.transform.DOMoveX(tempXPosition + offset, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                Debug.Log("ana obje pozisyonu sonra: " + gameObject.transform.position);
                ControlMatches(gameObject.transform.position, offset);
            });
            Debug.Log("degisen obje pozisyonu once: " + targetTile.transform.position);
            targetTile.transform.DOMoveX(tempTargetPosition - offset, 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                Debug.Log("deðisen obje pozisyonu sonra: " + targetTile.transform.position);
                ControlMatches(targetTile.transform.position, offset);
            });
        }
        else
        {
            Debug.LogWarning("Taþ dýþýna týkladýnýz. Ýþlem yapýlmadý.");
            return;
        }

    }

    public void SwipeLeft()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);
        Vector2 otherTile = new Vector2(worldStartPosition.x - (cellSize + spaceBetweenTiles), worldStartPosition.y);
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.x;
            float tempXPosition = gameObject.transform.position.x;
            // Objeyi sola kaydýr
            gameObject.transform.DOMoveX(tempXPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset);
            });
            targetTile.transform.DOMoveX(tempTargetPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset);
            });
        }
        else
        {
            Debug.LogWarning("Taþ dýþýna týkladýnýz. Ýþlem yapýlmadý.");
            return;
        }
    }

    public void SwipeUp()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);
        Vector2 otherTile = new Vector2(worldStartPosition.x, worldStartPosition.y + (cellSize + spaceBetweenTiles));
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.y;
            float tempXPosition = gameObject.transform.position.y;
            // Objeyi yukarý kaydýr
            gameObject.transform.DOMoveY(tempXPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset);
            });
            targetTile.transform.DOMoveY(tempTargetPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset);
            });
        }
        else
        {
            Debug.LogWarning("Taþ dýþýna týkladýnýz. Ýþlem yapýlmadý.");
            return;
        }
    }

    public void SwipeDown()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);
        Vector2 otherTile = new Vector2(worldStartPosition.x, worldStartPosition.y - (cellSize + spaceBetweenTiles));
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.y;
            float tempXPosition = gameObject.transform.position.y;
            // Objeyi aþaðý kaydýr
            gameObject.transform.DOMoveY(tempXPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset);
            });
            targetTile.transform.DOMoveY(tempTargetPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset);
            });
        }
        else
        {
            Debug.LogWarning("Taþ dýþýna týkladýnýz. Ýþlem yapýlmadý.");
            return;
        }
    }

    void DetectSwipe()
    { 
        Vector2 delta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) // Yatay hareket
        {

            if (delta.x > 0) SwipeRight();
            else SwipeLeft();  
        }
        else // Dikey hareket
        {
            if (delta.y > 0) SwipeUp();
            else SwipeDown();
        }
    }

    void OnMouseDown()
    {
        startTouchPosition = Input.mousePosition; // Týklanan pozisyonu al
        audioSource.Play();
    }

    void OnMouseUp()
    {
        endTouchPosition = Input.mousePosition; // Fare býrakýldýðýnda pozisyonu al
        DetectSwipe();
    }


    void ControlMatches(Vector2 position, float offset)
    {
        //for the horizontal consecutives
        List<GameObject> horizontalMatch = new List<GameObject>();
        horizontalMatch.AddRange(ControlNeighbours(Vector2.right, 2, position, offset));
        horizontalMatch.AddRange(ControlNeighbours(Vector2.left, 2, position, offset));

        //for the vertical consecutives
        List<GameObject> verticalMatch = new List<GameObject>();
        verticalMatch.AddRange(ControlNeighbours(Vector2.up, 2, position, offset));
        verticalMatch.AddRange(ControlNeighbours(Vector2.down, 2, position, offset));

        if (horizontalMatch.Count >= 2) // 2 or more matching tile means 3tilematch
        {
            Debug.Log("There is a horizontal match. Success!");
            PopUp.Instance.ShowSuccessPopUp();
        }
        else if (verticalMatch.Count >= 2)
        {
            Debug.Log("There is a vertical match. Success!");
            PopUp.Instance.ShowSuccessPopUp();
        }
        else
        {
            Debug.Log("There is no match!");
        }
    }

    List<GameObject> ControlNeighbours(Vector2 direction, int distance, Vector2 reference, float offset)
    {
        List<GameObject> matchedTiles = new List<GameObject>();
        Tile tile =Board.Instance.GetTileByPosition(reference).GetComponent<Tile>();
        TileTypes originTileType = tile.TileType;

        for (int i = 1; i <= distance; i++) // Daha geniþ bir aralýk kontrol ediliyor
        {
            Vector2 controlPosition = reference + (direction * i * offset);
            GameObject controlledTile = Board.Instance.GetTileByPosition(controlPosition);
            if(controlledTile == null) //for safety, if clicked outside of a tile
            {
                Debug.LogWarning("Tile is null for position: " + reference);
                return matchedTiles;
            }

            TileTypes controlledTileType = controlledTile.GetComponent<Tile>().TileType;

            if(controlledTileType == originTileType)
            {
                matchedTiles.Add(controlledTile);
            }
            else
            {
                break;
            }

        }
        return matchedTiles;
    }

        /*  void Update()
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        startTouchPosition = touch.position; // Dokunma baþlangýç pozisyonu
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        endTouchPosition = touch.position; // Dokunma bitiþ pozisyonu
                        DetectSwipe();
                    }
                }
            }*/


    
}
