﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{

    public Slider slider;
    public MoveBall ballMovement;
    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }
    void Update()
    {
        
    }
    
    public void ValueChangeCheck()
    {
        ballMovement.SetVelocity(slider.value * 100f);
    }
    
}
