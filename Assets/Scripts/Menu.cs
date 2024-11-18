using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour //Initial main menu manager
{

    private SpriteRenderer spriteRenderer; //For button animation

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer con not be found!");
        }
    }

    public void OnPlayButtonClicked() //If play button clicked start the gameboard scene
    {
        SceneManager.LoadScene(1); 
    }


    public void OnQuitButtonClicked() //If quit button clicked quit the app
    {
        Debug.Log("Game's closed.");
        Application.Quit();
    }

    private void OnMouseDown()
    {
        //Animation sequence for buttons, play and quit
        Sequence sequence = DOTween.Sequence();
        sequence.Append(gameObject.transform.DOScale(gameObject.transform.localScale * 1.15f, 0.15f).SetLoops(2, LoopType.Yoyo));
        sequence.Join(spriteRenderer.DOFade(0.7f, 0.15f).SetLoops(2, LoopType.Yoyo));
        sequence.OnComplete(() =>
        {
            if (gameObject.tag == "PlayButton")
            {
                OnPlayButtonClicked();
            }
            else if (gameObject.tag == "QuitButton")
            {
                OnQuitButtonClicked();
            }
        });
    }
}