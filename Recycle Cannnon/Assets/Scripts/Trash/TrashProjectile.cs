using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashProjectile : MonoBehaviour
{
    
    public string monsterTag = "Monster";

    void Update()
    {
        MoveForward();
    }

    private void MoveForward(){

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag(monsterTag)){
            HealthBase monsterHealth = collider.gameObject.GetComponent<HealthBase>();
            if(monsterHealth != null){
                monsterHealth.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }

}
