using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{

    public enum TileTypes { Blue, Pink, Red, Brown, Yellow, Green, Black } //For comparison for possible match
    public TileTypes TileType;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private AudioSource audioSource;
   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwipeRight() //Swipe right mechanic
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition); //clicked objets's position
        Vector2 otherTile = new Vector2(worldStartPosition.x + offset, worldStartPosition.y); //other object is one offset right
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile); //get the other tile gameobject by its world point
        if (targetTile != null) //if there is a gameobject (collider) at the clicked position
        {
            float tempTargetPosition = targetTile.transform.position.x;
            float tempXPosition = gameObject.transform.position.x;
            // Swipe clicked object to right
            gameObject.transform.DOMoveX(tempXPosition + offset, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset); //check for a 3tilematch
            });
            //And swipe the other object to left to switch positions
            targetTile.transform.DOMoveX(tempTargetPosition - offset, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset); //check for a 3tilematch
            });
        }
        else
        {
            Debug.LogWarning("Clicked out of tile.");
            return;
        }

    }

    public void SwipeLeft() //Swipe left mechanic
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition); //clicked objets's position
        Vector2 otherTile = new Vector2(worldStartPosition.x - offset, worldStartPosition.y); //other object is one offset left
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile); //get the other tile gameobject by its world point
        if (targetTile != null) //if there is a gameobject (collider) at the clicked position
        {
            float tempTargetPosition = targetTile.transform.position.x;
            float tempXPosition = gameObject.transform.position.x;
            //Swipe clicked object to left
            gameObject.transform.DOMoveX(tempXPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset); //check for a 3tilematch
            });
            //And swipe the other object to right to switch positions
            targetTile.transform.DOMoveX(tempTargetPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset); //check for a 3tilematch
            });
        }
        else
        {
            Debug.LogWarning("Clicked out of tile.");
            return;
        }
    }

    public void SwipeUp() //Swipe up mechanic
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition); //clicked objets's position
        Vector2 otherTile = new Vector2(worldStartPosition.x, worldStartPosition.y + offset); //other object is one offset up
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile); //get the other tile gameobject by its world point
        if (targetTile != null) //if there is a gameobject (collider) at the clicked position
        {
            float tempTargetPosition = targetTile.transform.position.y;
            float tempXPosition = gameObject.transform.position.y;
            //Swipe clicked object up
            gameObject.transform.DOMoveY(tempXPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset); //check for a 3tilematch
            });
            //And swipe the other object down to switch positions
            targetTile.transform.DOMoveY(tempTargetPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset); //check for a 3tilematch
            });
        }
        else
        {
            Debug.LogWarning("Clicked out of tile.");
            return;
        }
    }

    public void SwipeDown() //Swipe down mechanic
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        float offset = cellSize + spaceBetweenTiles;
        Vector2 worldStartPosition = Camera.main.ScreenToWorldPoint(startTouchPosition); //clicked objets's position
        Vector2 otherTile = new Vector2(worldStartPosition.x, worldStartPosition.y - offset); //other object is one offset down
        GameObject targetTile = Board.Instance.GetTileByPosition(otherTile); //get the other tile gameobject by its world point
        if (targetTile != null) //if there is a gameobject (collider) at the clicked position
        {
            float tempTargetPosition = targetTile.transform.position.y;
            float tempXPosition = gameObject.transform.position.y;
            //Swipe clicked object down
            gameObject.transform.DOMoveY(tempXPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(gameObject.transform.position, offset); //check for a 3tilematch
            });
            //And swipe the other object up to switch positions
            targetTile.transform.DOMoveY(tempTargetPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                ControlMatches(targetTile.transform.position, offset); //check for a 3tilematch
            });
        }
        else
        {
            Debug.LogWarning("Clicked out of tile.");
            return;
        }
    }

    void DetectSwipe() //Decide the swipe direction by mouseDown and mouseUp positions
    { 
        Vector2 delta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) //Horizontal move
        {

            if (delta.x > 0) SwipeRight();
            else SwipeLeft();  
        }
        else //Vertical move
        {
            if (delta.y > 0) SwipeUp();
            else SwipeDown();
        }
    }

    void OnMouseDown()
    {
        //When a tile is clicked, the spin and stop mechanic will be unabled
        SpinButton.Instance.isSpinPressed = true; 
        StopButton.Instance.isStopPressed = true;
        startTouchPosition = Input.mousePosition; //Get the clicked position
        audioSource.Play();
    }

    void OnMouseUp()
    {
        endTouchPosition = Input.mousePosition; //Get the position when mouse released
        DetectSwipe();
    }


    void ControlMatches(Vector2 position, float offset) //If there is a match it show the pop up.
    {
        //for the horizontal consecutives check up to two tiles to the side (both right and left)
        List<GameObject> horizontalMatch = new List<GameObject>();
        horizontalMatch.AddRange(ControlNeighbours(Vector2.right, 2, position, offset));
        horizontalMatch.AddRange(ControlNeighbours(Vector2.left, 2, position, offset));

        //for the vertical consecutives check up to two tiles to the side (both up and down)
        List<GameObject> verticalMatch = new List<GameObject>();
        verticalMatch.AddRange(ControlNeighbours(Vector2.up, 2, position, offset));
        verticalMatch.AddRange(ControlNeighbours(Vector2.down, 2, position, offset));

        if (horizontalMatch.Count >= 2) // 2 or more matching tile means 3tilematch
        {
            Debug.Log("There is a horizontal match. Success!");
            PopUp.Instance.ShowSuccessPopUp();
        }
        else if (verticalMatch.Count >= 2) //if there is no horizontal match still check the vertical one
        {
            Debug.Log("There is a vertical match. Success!");
            PopUp.Instance.ShowSuccessPopUp();
        }
        else
        {
            Debug.Log("There is no match!");
        }
    }

    List<GameObject> ControlNeighbours(Vector2 direction, int distance, Vector2 reference, float offset) //check the given number of neighbours (distance) to the sides
    {
        //will return how many matched tiles are there, it's length is important for detecting a match
        List<GameObject> matchedTiles = new List<GameObject>();

        Tile tile =Board.Instance.GetTileByPosition(reference).GetComponent<Tile>();
        TileTypes originTileType = tile.TileType;

        for (int i = 1; i <= distance; i++)
        {
            Vector2 controlPosition = reference + (direction * i * offset);
            GameObject controlledTile = Board.Instance.GetTileByPosition(controlPosition);
            if(controlledTile == null) //for safety, if clicked outside of a tile
            {
                Debug.LogWarning("Tile is null for position: " + reference);
                return matchedTiles;
            }

            TileTypes controlledTileType = controlledTile.GetComponent<Tile>().TileType;

            if(controlledTileType == originTileType) //If the main tile type and the tile on the side is same, add it to matchedTiles list
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
                        startTouchPosition = touch.position; 
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        endTouchPosition = touch.position; 
                        DetectSwipe();
                    }
                }
            }*/


    
}
