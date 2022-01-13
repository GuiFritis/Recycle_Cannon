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
    public float repulsionPower = 300f;
    public float stunDuration = 0.5f;
    public float recoverDuration = 1.5f;

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
    private bool touch = false;

    private GameObject _auxTrash;
    private GameObject _auxTrashCan;
    private bool _stuned = false;
    private bool _tangible = true;

    void Start()
    {
        health.OnDie += Die;
    }

    // Update is called once per frame
    void Update()
    {
        TouchInputControl();
        Move();
    }

    private void TouchInputControl(){
        if(Input.touchCount > 0 && !_stuned){
            VerifyTouchPosition();
            if(_touchIndex >= 0){
                _touchDuration += Input.GetTouch(_touchIndex).deltaTime;
                if(_touchDuration > tapDuration){
                    Move();
                } else if(Input.GetTouch(_touchIndex).phase == TouchPhase.Ended){
                    Interact();   
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
        if(_stuned){return;}
        if(Input.GetMouseButtonDown(1) && Input.mousePosition.x > Screen.width/2){
            Interact();
        }

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

    private void VerifyTouchPosition(){
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

    private void Interact(){
        if(trash == null){
            if(_auxTrash != null){
                trash = _auxTrash.GetComponent<Trash>();
            }
        } else if(_auxTrashCan != null){
            TrashCan trashCan = _auxTrashCan.GetComponent<TrashCan>();
            if(trashCan != null){
                if(trashCan.type == trash.type){
                    trashCan.PutTrash(trash.trashSize);
                    trash = null;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(monsterTag) && _tangible){
            _stuned = true;
            _tangible = false;
            health.TakeDamage(1);
            playerRigidbody.AddExplosionForce(repulsionPower, collision.gameObject.transform.position, 10f);
            Invoke(nameof(UnStun), stunDuration);
            Invoke(nameof(MakeTangible), recoverDuration);
        } else if(collision.gameObject.CompareTag(trashCanTag) && trash != null){
            _auxTrashCan = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag(trashCanTag)){
            _auxTrashCan = null;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag(trashTag) && trash == null){
            _auxTrash = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag(trashTag)){
            _auxTrash = null;
        }
    }

    private void UnStun(){
        _stuned = false;
    }

    private void MakeTangible(){
        _tangible = true;
    }

    private void Die(){
        Destroy(gameObject);
    }

}
