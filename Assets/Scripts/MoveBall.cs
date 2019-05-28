using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Vuforia;

public class MoveBall : MonoBehaviour
{
    public TransformData ballTrajectory;
    public GameObject ball;
    private int currentPositionNumber = 0;
    private float velocity = 0f;
    private float passed = 0;
    private bool isStarted;
    public LineRenderer lineRenderer;
    private int segmentCount = 1;
    private int newSegmentCount = 0;
    private Vector3 startPoint;
    private IEnumerator movement;
    
    void Start()
    {
        ball.transform.position = GetPathPosition(0);
        SetCurrentStartPoint(GetPathPosition(0));
        DrawLine(segmentCount);
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
     public IEnumerator BallMovement()
     {
         isStarted = true;
         SetCurrentStartPoint(ball.transform.position);
         newSegmentCount = 0;

         while (currentPositionNumber < ballTrajectory.item.x.Length - 1)
         {
             passed += velocity * Time.deltaTime;

             if (passed >= 1)
             {
                 currentPositionNumber++;
                 passed = 0;
                 ball.transform.Translate(GetPathPosition(currentPositionNumber) -
                                          GetPathPosition(currentPositionNumber - 1));
                 newSegmentCount++;
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
     public void Reset()
     {
         StopMovement();
         ResetPosition();
     }
     public void StartMovement()
     {
         if (isStarted == false && velocity != 0f)
         {
             movement = BallMovement();
             StartCoroutine(movement);
         }
     }
     private void StopMovement()
     {
         if (isStarted)
         {
             StopCoroutine(movement);
             isStarted = false;
         }
     }

     private void ResetPosition()
     {
         if (GetBallPosition().x <= startPoint.x && GetBallPosition().x > GetPathPosition(0).x)
         {
             ball.transform.position = GetBallPosition() - GetPathPosition(ballTrajectory.item.x.Length - 1);
             segmentCount -= ballTrajectory.item.x.Length - 1;
         }
         else if (GetBallPosition().x > startPoint.x)
         {
             ball.transform.position = startPoint;
             currentPositionNumber = 0;
             segmentCount -= newSegmentCount;
         }
         
         newSegmentCount = 0;
         lineRenderer.SetVertexCount(segmentCount);
     }

}
