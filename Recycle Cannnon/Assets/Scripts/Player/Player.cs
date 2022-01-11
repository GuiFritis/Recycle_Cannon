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
    public string monsterTag = "Monster";
    public string trashTag = "Trash";
    public string trashCanTag = "TrashCan";

    public float tapDuration = 0.2f;

    private Vector2 _touchPos = Vector2.zero;
    private int _touchIndex = -1;
    private Vector3 _mousePos = Vector2.zero;

    private float _touchDuration = 0f;
    private bool _tapped = false;
    private bool touch = false;

    // Update is called once per frame
    void Update()
    {
        TouchInputControl();
        Move();
    }

    private void TouchInputControl(){
        _tapped = false;
        if(Input.touchCount > 0){
            verifyTouchPosition();
            if(_touchIndex >= 0){
                _touchDuration += Input.GetTouch(_touchIndex).deltaTime;
                if(_touchDuration > tapDuration){
                    Move();
                } else if(Input.GetTouch(_touchIndex).phase == TouchPhase.Ended){
                    _tapped = true;       
                }
            } else {
                _touchDuration = 0f;
            }
        }
    }

    private void Move(){
        // if(Input.GetTouch(_touchIndex).phase != TouchPhase.Ended){
        //     playerRigidbody.velocity = new Vector3(_touchPos.x - Input.GetTouch(_touchIndex).position.x, 0, _touchPos.y - Input.GetTouch(_touchIndex).position.y) * speedMultiplier;
        // } else {
        //     playerRigidbody.velocity = Vector3.zero;
        // }
        // DEBUG WITH MOUSE
        if(Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width/2){
            _mousePos = Input.mousePosition;
            touch = true;
        }
        if(Input.GetMouseButton(0) && touch){
            playerRigidbody.velocity = new Vector3(_mousePos.x - Input.mousePosition.x, 0, _mousePos.y - Input.mousePosition.y).normalized * -speedMultiplier;
        }
        if(Input.GetMouseButtonUp(0)){
            playerRigidbody.velocity = Vector3.zero;
            touch = false;
        }
    }

    private void verifyTouchPosition(){
        if(_touchIndex < 0){
            _touchIndex = Input.touchCount-1;
            if(Input.GetTouch(_touchIndex).phase == TouchPhase.Began){
                if(Input.GetTouch(_touchIndex).position.x > Screen.width/2){
                    _touchPos = Input.GetTouch(_touchIndex).position;
                }
            } else if(Input.GetTouch(_touchIndex).phase == TouchPhase.Ended){
                _touchIndex = -1;
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
