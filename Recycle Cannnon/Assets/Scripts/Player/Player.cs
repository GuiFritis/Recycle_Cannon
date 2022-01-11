using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class Player : MonoBehaviour
{

    [Header("References")]
    public Rigidbody playerRigidbody;

    [Space]
    public Trash trash;

    [Space]
    public float speedMultiplier = 1f;

    [Space]
    public HealthBase health;

    [Header("Tags")]
    public string monsterTag = "monster";
    public string trashTag = "trash";
    public string trashCanTag = "trashCan";

    public float tapDuration = 0.2f;

    private Vector2 _touchPos = Vector2.zero;
    private int _moveTouch;
    private Vector3 _mousePos = Vector2.zero;

    private float _touchDuration = 0f;
    private bool _tapped = false;

    // Update is called once per frame
    void Update()
    {
        TouchInputControl();
    }

    private void TouchInputControl(){
        _tapped = false;
        if(Input.touchCount > 0){
            verifyTouchPosition();
            if(_moveTouch >= 0){
                _touchDuration += Input.GetTouch(_moveTouch).deltaTime;
                if(_touchDuration > tapDuration){
                    Move();
                } else if(Input.GetTouch(_moveTouch).phase == TouchPhase.Ended){
                    _tapped = true;
                }
            } else {
                _touchDuration = 0f;
            }
        }
    }

    private void Move(){
        if(Input.GetTouch(_moveTouch).phase != TouchPhase.Ended){
            playerRigidbody.velocity = new Vector3(_touchPos.x - Input.GetTouch(_moveTouch).position.x, 0, _touchPos.y - Input.GetTouch(_moveTouch).position.y) * speedMultiplier;
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
        if(Input.GetTouch(_moveTouch).phase == TouchPhase.Ended){
            _moveTouch = Input.touchCount-1;
            if(Input.GetTouch(_moveTouch).phase == TouchPhase.Began){
                if(Input.GetTouch(_moveTouch).position.x > Screen.width/2){
                    _touchPos = Input.GetTouch(_moveTouch).position;
                }
            } else {
                _moveTouch = -1;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(monsterTag)){
            health.TakeDamage(1);
            playerRigidbody.AddExplosionForce(3f, collision.transform.position, 3f);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(_tapped){
            if(collision.gameObject.CompareTag(trashTag) && trash == null){
                    trash = collision.gameObject.GetComponent<Trash>();
            } else if(collision.gameObject.CompareTag(trashCanTag) && trash != null){
                TrashCan trashCan = collision.gameObject.GetComponent<TrashCan>();
                if(trashCan != null){
                    if(trashCan.type == trash.type){
                        trashCan.PutTrash(trash.trashSize);
                        trash = null;
                    }
                }
            }
        }
    }

}
