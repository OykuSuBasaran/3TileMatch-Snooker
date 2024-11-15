using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;

public class Tile : MonoBehaviour
{

    private Vector3 startTouchPosition;
    private Vector3 endTouchPosition;

    /*private void OnMouseDown()
    {
        Debug.Log("tiklandi objeye: " +  gameObject.transform.position);
    }*/

    Vector3 GetBoardPosition()
    {
        return new (gameObject.transform.position.x, gameObject.transform.position.y);
    }

    //Vector3 GetTargetPosition(){}

    public void SwipeRight()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        // Objeyi saða kaydýr
        transform.DOMoveX(transform.position.x + (cellSize+spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);

    }

    public void SwipeLeft()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        // Objeyi saða kaydýr
        gameObject.transform.DOMoveX(transform.position.x - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);
    }

    public void SwipeUp()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        // Objeyi saða kaydýr
        gameObject.transform.DOMoveY(transform.position.y + (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);

    }

    public void SwipeDown()
    {
        float xPosition, yPosition;
        float cellSize = Board.Instance.CalculateBoardPlacements(out xPosition, out yPosition);
        float spaceBetweenTiles = Board.Instance.spaceBetweenTiles;
        // Objeyi saða kaydýr
        gameObject.transform.DOMoveY(transform.position.y - (cellSize + spaceBetweenTiles), 1f).SetEase(Ease.InOutQuad);

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

    /*    void Update()
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

    void OnMouseDown()
    {
        startTouchPosition = Input.mousePosition; // Týklanan pozisyonu al
    }

    void OnMouseUp()
    {
        endTouchPosition = Input.mousePosition; // Fare býrakýldýðýnda pozisyonu al
        DetectSwipe();
    }
}
