using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BallsSwitcher : MonoBehaviour
{
    private SpeedController speedController;

    private TouchHandler touchHandler;

    private CameraFollow mainCamera;

    public GameObject[] balls;

    private MoveBall currentBall;

    private int _currentNumber = 0;
    // Start is called before the first frame update
    void Awake()
    {
        speedController = FindObjectOfType<SpeedController>();
        touchHandler = FindObjectOfType<TouchHandler>();
        mainCamera = FindObjectOfType<CameraFollow>();
        
    }

    void Start()
    {
        SetCurrentBall(_currentNumber);   
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextButtonPressed()
    {
        if (_currentNumber < balls.Length - 1)
        {
            currentBall.SetVelocity(0f);
            _currentNumber++;
            SetCurrentBall(_currentNumber);
        }
    }

    public void OnPrevButtonPressed()
    {
        if (_currentNumber > 0)
        {
            currentBall.SetVelocity(0f);
            _currentNumber--;
            SetCurrentBall(_currentNumber);
        } 
    }
    
    private void SetCurrentBall(int number)
    {
        currentBall = balls[number].GetComponent<MoveBall>();
        
        touchHandler.ballMovement = balls[number].GetComponent<MoveBall>();

        mainCamera.targetTransform = balls[number].transform;
        
        speedController.ballMovement = balls[number].GetComponent<MoveBall>();

        speedController.slider.value = currentBall.GetVelocity() / 100f;
    }

}
