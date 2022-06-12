using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public bool swipeLeft, swipeRight, swipeUp, swipeDown, tap, doubleTap;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;


    [SerializeField] private float swipeRange;
    [SerializeField] private float tapRange;

    private float firstClickTime;
    private const float DOUBLE_CLICK_TIME = 0.18f;
    private bool coroutineAllowed;
    private int clickCounter;

    private void Awake() {
        swipeLeft = swipeRight = swipeUp = swipeDown = tap = false;

        firstClickTime = 0f;
        clickCounter = 0;
        coroutineAllowed = true;
    }

    void Update() {
        Swipe();
    }

    public void Swipe() {
        if (Input.touchCount > 0) {
            int pointerID = Input.GetTouch(0).fingerId;
            if (EventSystem.current.IsPointerOverGameObject(pointerID)) {
                return;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch) {
                if (Distance.x < -swipeRange) {
                    swipeLeft = true;
                    swipeRight = false;
                    swipeUp = false;
                    swipeDown = false;
                    stopTouch = true;                   
                }
                else if (Distance.x > swipeRange) {
                    swipeLeft = false;
                    swipeRight = true;
                    swipeUp = false;
                    swipeDown = false;
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange) {
                    swipeLeft = false;
                    swipeRight = false;
                    swipeUp = true;
                    swipeDown = false;
                    stopTouch = true;                   
                }
                else if (Distance.y < -swipeRange) {
                    swipeLeft = false;
                    swipeRight = false;
                    swipeUp = false;
                    swipeDown = true;
                    stopTouch = true;                   
                }
                tap = false;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            stopTouch = false;

            bool clicked = false; 

            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange) {
                clickCounter += 1;
      
                if(clickCounter == 1 && coroutineAllowed) {
                    firstClickTime = Time.time;
                    StartCoroutine(DoubleClickDetection());
                }
                
                swipeLeft = false;
                swipeRight = false;
                swipeUp = false;
                swipeDown = false;
                // stopTouch = true;


            }
        }
    }

    private IEnumerator DoubleClickDetection() {
        coroutineAllowed = false;
        bool normalClickAllowed = true;
        while (Time.time < firstClickTime + DOUBLE_CLICK_TIME) {
            if (clickCounter == 2) {
                Debug.Log("DoubleClick");
                tap = false;
                doubleTap = true;
                normalClickAllowed = false;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        if (normalClickAllowed) {
            Debug.Log("NormalClick");
            doubleTap = false;
            tap = true;            
        }
      
        clickCounter = 0;
        firstClickTime = 0f;
        coroutineAllowed = true;
    }


}
