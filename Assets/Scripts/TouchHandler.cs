using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private float doubleClickTimeLimit = 0.25f;
    public MoveBall ballMovement;
    protected void Start()
    {
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
        //Debug.Log("single");
        ballMovement.StartMovement();     
    }

    private void DoubleClick()
    {
        //Debug.Log("double");
        ballMovement.Reset();
    } 
    
}

