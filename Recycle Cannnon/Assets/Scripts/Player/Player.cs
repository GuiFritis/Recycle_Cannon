using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("References")]
    public Rigidbody playerRigidbody;

    [Space]
    public GameObject trash;

    [Space]
    public float speedMultiplier = 1f;

    private Vector2 _touchPos = Vector2.zero;
    private Touch _moveTouch;
    private Vector3 _mousePos = Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        _moveTouch = new Touch();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        verifyTouchPosition();
        if(_moveTouch.phase != TouchPhase.Ended){
            playerRigidbody.velocity = new Vector3(_touchPos.x - _moveTouch.position.x, 0, _touchPos.y - _moveTouch.position.y) * speedMultiplier;
        } else {
            playerRigidbody.velocity = Vector3.zero;
        }
        // DEBUG WITH MOUSE
        if(Input.GetMouseButtonDown(0)){
            _mousePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0)){
            playerRigidbody.velocity = new Vector3(_mousePos.x - Input.mousePosition.x, 0, _mousePos.y - Input.mousePosition.y).normalized * -speedMultiplier;
        }
    }

    private void verifyTouchPosition(){
        if(_moveTouch.phase == TouchPhase.Ended){
            _moveTouch = Input.GetTouch(Input.touchCount);
            if(_moveTouch.phase == TouchPhase.Began){
                if(_moveTouch.position.x > Screen.width/2){
                    _touchPos = _moveTouch.position;
                }
            } else {
                _moveTouch.phase = TouchPhase.Ended;
            }
        }
    }

}
