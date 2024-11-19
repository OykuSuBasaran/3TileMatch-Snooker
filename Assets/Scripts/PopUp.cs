using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PopUp : MonoBehaviour
{
    public static PopUp Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //singleton
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject successPopUp; //Pop-up panel reference
    public AudioSource audioSource;
    public GameObject confettiPrefab; //Prefab for the confetti particle animation
    public GameObject[] balls; //ball sprites
    public List<float> finalPositions; //ball destinations in x-axis
    public List<int> spinDirection; //spin direction of the balls on the scene

    //When the match made, this will be called
    public void ShowSuccessPopUp()
    {
        successPopUp.transform.DOScale(new Vector3(5, 5, 1), 1f) // rescale the pop-up background 5 times bigger 
            .SetEase(Ease.OutBack).OnComplete(()=> {
            StartCoroutine(HandleSequenceAndReload());
            });
        audioSource.Play();
        GameObject particles = Instantiate(confettiPrefab);
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        ps.Play();
    }


    private IEnumerator HandleSequenceAndReload() //Manages the scene sequence 
    {
        yield return PopupSequence(); //Balls' move animations

        yield return new WaitForSeconds(2f); //Wait for extra 2 seconds before reloading the scene

        //Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private IEnumerator PopupSequence() //Manages the balls' sequence
    {
        for (int i = 0; i < balls.Length; i++)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(balls[i].transform.DOMoveX(finalPositions[i], 1f).SetEase(Ease.Linear));
            sequence.Join(balls[i].transform.DORotate(new Vector3(0, 0, -360 * spinDirection[i]), 1f, RotateMode.FastBeyond360)
                 .SetEase(Ease.InOutQuad));
        }
        yield return new WaitForSeconds(0.5f);
    }

    //Not necessary for now, since the gameboard scene will reload automatically
    public void ClosePopUp()
    {
        Destroy(successPopUp);
    }
}
