using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public TransformData ballTrajectory;
    private GameObject ball;
    private int currentPositionNumber = 0;
    private float velocity = 60f;
    private float passed = 0;
    private bool isStarted = false;
    
    void Awake ()
    {
        ball = this.gameObject;
        ball.transform.position = GetBallPosition(0);      
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(isStarted == false) 
                    StartCoroutine(BallMovement());
            }
        }

    }
    private Vector3 GetBallPosition(int currentPositionNumber)
    {
        return new Vector3(ballTrajectory.item.x[currentPositionNumber],
            ballTrajectory.item.y[currentPositionNumber],
            ballTrajectory.item.z[currentPositionNumber]);
    }  

     IEnumerator BallMovement()
     {
        isStarted = true;
        
        while (currentPositionNumber < ballTrajectory.item.x.Length - 1)
        {
            passed += velocity * Time.deltaTime;

            if (passed >= 1)
            {
                currentPositionNumber++;
                passed = 0;
                ball.transform.Translate(GetBallPosition(currentPositionNumber) - GetBallPosition(currentPositionNumber - 1));
                //Debug.Log(ball.transform.position);
            }
            
            yield return null;
        }

        isStarted = false;
        currentPositionNumber = 0;

     }

     public void SetVelocity(float v)
     {
         this.velocity = v;
     }

     public float GetVelocity()
     {
         return velocity;
     }
    
}
