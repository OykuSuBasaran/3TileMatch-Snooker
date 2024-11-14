using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEditorInternal;

public class Buttons : MonoBehaviour
{

    private float timeElapsed = 0f; // Geçen süreyi tutacak
    public float interval = 0.25f;


    // Update is called once per frame
    void Update()
    {
        if (SpinButton.Instance.isSpinning)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= interval) // Eðer belirlediðimiz intervali geçtiyse
            {
                SpinButton.Instance.Slot();
                Debug.Log("spin tusu calisiyor");
                timeElapsed = 0f;
            }
        }
    }

   
}

