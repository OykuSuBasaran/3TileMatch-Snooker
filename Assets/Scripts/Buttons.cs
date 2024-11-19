using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEditorInternal;

public class Buttons : MonoBehaviour //ButtonManager for Spin and Stop Buttons
{

    private float timeElapsed = 0f; // Time passed 
    public float interval = 0.25f; //  for limiting processed frames

    // Update is called once per frame
    void Update()
    {
        if (SpinButton.Instance.isSpinning) // if spin button pressed code will create random prefabs for slot illusion, it's an infinite loop 
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= interval) // If the interval reached or exceeded
            {
                SpinButton.Instance.Slot(); //Start slot animation, which means create new prefabs and move them, till the stop button pressed
                Debug.Log("spin tusu calisiyor");
                timeElapsed = 0f;
            }
        }
    }

   
}

