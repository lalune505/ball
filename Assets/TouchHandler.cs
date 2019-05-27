using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private float doubleClickTimeLimit = 0.25f;
    private MoveBall ballMovement;
    protected void Start()
    {
        ballMovement = FindObjectOfType<MoveBall>();
        StartCoroutine(InputListener());
    }

    private IEnumerator InputListener() 
    {
        while(enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                    yield return ClickEvent();
            }
               
            yield return null;
        }
    }
    private IEnumerator ClickEvent()
    {
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while(count < doubleClickTimeLimit)
        {
            if(Input.GetMouseButtonDown(0))
            {
                DoubleClick();
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }
        SingleClick();
    }

    private void SingleClick()
    {                        
        ballMovement.StartMovement();     
    }

    private void DoubleClick()
    {
        ballMovement.Reset();
    } 
    
}

