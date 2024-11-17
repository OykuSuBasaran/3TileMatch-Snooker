using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PopUp : MonoBehaviour
{
    public static PopUp Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Baþka bir instance varsa yok et
        }
    }
    public GameObject successPopUp; // Pop-up paneli burada referans alacaðýz.
    public AudioSource audioSource;
    // Baþarýyla tamamlandýðýnda çaðrýlacak bir metot.
    public void ShowSuccessPopUp()
    {
        audioSource.Play();
        Instantiate(successPopUp);
        //successPopUp.transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutBack);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Kapatma fonksiyonu (Buton için kullanýlabilir).
    public void ClosePopUp()
    {
        Destroy(successPopUp);
    }
}
