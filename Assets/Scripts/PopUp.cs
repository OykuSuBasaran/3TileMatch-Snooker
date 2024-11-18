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


    //When the match made, this will be called
    public void ShowSuccessPopUp()
    {
        audioSource.Play();
        Instantiate(successPopUp);
        GameObject particles = Instantiate(confettiPrefab);
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        ps.Play();
        //successPopUp.transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutBack);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Not necessary for now, since the gameboard scene will reload automatically
    public void ClosePopUp()
    {
        Destroy(successPopUp);
    }
}
