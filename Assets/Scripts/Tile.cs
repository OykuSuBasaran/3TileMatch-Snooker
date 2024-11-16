using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;

public class Tile : MonoBehaviour
{

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    /*private void OnMouseDown()
    {
        Debug.Log("tiklandi objeye: " +  gameObject.transform.position);
    }*/


    //Vector3 GetTargetPosition(){}

    Vector3 GetTilePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return mousePosition;
    }

    public void SwipeRight()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        GameObject targetTile = Board.Instance.GetTileByPosition(endTouchPosition);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.x;
            float tempXPosition = gameObject.transform.position.x;
            // Objeyi saða kaydýr
            gameObject.transform.DOMoveX(tempXPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
            targetTile.transform.DOMoveX(tempTargetPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
        }
        else {
            Debug.LogWarning("Taþ dýþýna týkladýnýz. Ýþlem yapýlmadý.");
            return; 
        }
   
    }

    public void SwipeLeft()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        GameObject targetTile = Board.Instance.GetTileByPosition(endTouchPosition);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.x;
            float tempXPosition = gameObject.transform.position.x;
            // Objeyi sola kaydýr
            gameObject.transform.DOMoveX(tempXPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
            targetTile.transform.DOMoveX(tempTargetPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
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
        GameObject targetTile = Board.Instance.GetTileByPosition(endTouchPosition);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.y;
            float tempXPosition = gameObject.transform.position.y;
            // Objeyi yukarý kaydýr
            gameObject.transform.DOMoveY(tempXPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
            targetTile.transform.DOMoveY(tempTargetPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
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
        GameObject targetTile = Board.Instance.GetTileByPosition(endTouchPosition);
        if (targetTile != null)
        {
            float tempTargetPosition = targetTile.transform.position.y;
            float tempXPosition = gameObject.transform.position.y;
            // Objeyi aþaðý kaydýr
            gameObject.transform.DOMoveY(tempXPosition - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
            targetTile.transform.DOMoveY(tempTargetPosition + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
        }
        else
        {
            Debug.LogWarning("Taþ dýþýna týkladýnýz. Ýþlem yapýlmadý.");
            return;
        }
    }

    void DetectSwipe()
    {
        Vector3 delta = endTouchPosition - startTouchPosition;

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
    }

    void OnMouseUp()
    {
        endTouchPosition = Input.mousePosition; // Fare býrakýldýðýnda pozisyonu al
        DetectSwipe();
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
