using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap,isDraging;
    private Vector2 startTouch, swipeDelta;
    public GameObject sangria;
    private Rigidbody sangriaRB;

    public Vector2 SwipeDelta{ get { return swipeDelta; } }
    public Vector2 StartTouch { get { return startTouch; } }

    public void Start()
    {
        sangriaRB = sangria.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        tap = false;
        #region MouseInputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region TouchInput
        if(Input.touchCount> 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate Force
        if(isDraging)
        {
            if (Input.touchCount > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
            swipeDelta = swipeDelta * 0.1f;
            sangriaRB.AddForce(swipeDelta);
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
}
