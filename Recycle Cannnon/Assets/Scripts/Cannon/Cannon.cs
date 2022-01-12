using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public List<TrashType> trashAmmo;
    public List<TrashProjectile> PFB_projectiles;

    public float maxDegrees = 57f;

    public float fireRate = 0.8f;

    public float tapDuration = 0.2f;
    public float rotationSpeedMultiplier = 0.1f;
    public Transform instantiateTransform;

    private int _touchIndex = -1;
    private float _pos0;
    private float _touchDuration = 0f;
    private float _fireCooldown = 0f;
    private bool touch = false;

    void Awake()
    {
        _pos0 = Screen.width/4;
    }

    void Update()
    {
        TouchInputControl();
        Rotate();
    }

    public void AddTrash(TrashType trash, int amount=1){
        for (int i = 0; i < amount; i++)
        {            
            trashAmmo.Add(trash);
        }
    }

    private void TouchInputControl(){
        if(Input.touchCount > 0){
            verifyTouchPosition();
            if(_touchIndex >= 0){
                _touchDuration += Input.GetTouch(_touchIndex).deltaTime;
                if(_touchDuration > tapDuration){
                    Rotate();
                } else if(Input.GetTouch(_touchIndex).phase == TouchPhase.Ended){
                    Shoot();
                }
            } else {
                _touchDuration = 0f;
            }
        }
    }

    private void verifyTouchPosition(){
        if(_touchIndex < 0){
            _touchIndex = Input.touchCount-1;
            if(Input.GetTouch(_touchIndex).phase == TouchPhase.Ended) {
                _touchIndex = -1;
            }
        }
    }

    private void Rotate(){
        // if(Input.GetTouch(_touchIndex).phase != TouchPhase.Ended){
        //     transform.Rotate(0f, 0f, _touchPosX - Input.GetTouch(_touchIndex).position.x);    
        // } 

        // DEBUG WITH MOUSE
        if(Input.GetMouseButtonDown(1) && Input.mousePosition.x < Screen.width/2){
            Shoot();
        }

        if(Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width/2){
            touch = true;
        }
        if(Input.GetMouseButton(0) && touch){
            transform.Rotate(0f, 0f, transform.rotation.y - (Input.mousePosition.x - _pos0) * Time.deltaTime * rotationSpeedMultiplier);
        }
        if(Input.GetMouseButtonUp(0)){
            touch = false;
        }
    }

    private void Shoot(){
        if(trashAmmo.Count > 0){
            foreach (var projectile in PFB_projectiles)
            {
                if(projectile.type == trashAmmo[0]){
                    trashAmmo.RemoveAt(0);
                    Instantiate(projectile, instantiateTransform.position, instantiateTransform.rotation);
                    return;
                }
            }
        }
    }
}
