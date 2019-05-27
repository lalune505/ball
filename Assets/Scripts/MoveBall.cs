using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public TransformData ballTrajectory;
    private GameObject ball;
    private int currentPositionNumber = 0;
    private float velocity = 0f;
    private float passed = 0;
    private bool isStarted = false;
    public LineRenderer lineRenderer;
    private int segmentCount = 1;
    private Vector3 startPoint;
   
    
    
    void Awake ()
    {
        ball = this.gameObject;
        ball.transform.position = GetPathPosition(0);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(isStarted == false && velocity != 0f) 
                    StartCoroutine(BallMovement());
            }
        }

    }
    private Vector3 GetPathPosition(int currentPositionNumber)
    {
        return new Vector3(ballTrajectory.item.x[currentPositionNumber],
            ballTrajectory.item.y[currentPositionNumber],
            ballTrajectory.item.z[currentPositionNumber]);
    }  

    private Vector3 GetBallPosition()
    {
        return ball.transform.position;
    }  
    
     IEnumerator BallMovement()
     {
        isStarted = true;
        SetCurrentStartPoint(ball.transform.position);
        
        while (currentPositionNumber < ballTrajectory.item.x.Length - 1)
        {
            passed += velocity * Time.deltaTime;

            if (passed >= 1)
            {
                currentPositionNumber++;
                passed = 0;
                ball.transform.Translate(GetPathPosition(currentPositionNumber) - GetPathPosition(currentPositionNumber - 1));
                segmentCount++;
                DrawLine(segmentCount);
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

     private void DrawLine(int n)
     {    
         lineRenderer.SetVertexCount(segmentCount);
         lineRenderer.SetPosition(segmentCount - 1, GetBallPosition());
     }

     private void SetCurrentStartPoint(Vector3 point)
     {
         this.startPoint = point;
     }
}
