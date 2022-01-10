using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    public TrashType type = TrashType.ORGANIC;
    public HealthBase health;
    public float timeToDie = 0.3f;
    public LayerMask trashProjectiles;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer.Equals(trashProjectiles)){
            health.TakeDamage(1);
        }
    }

    public void Die(){
        Destroy(this.gameObject, timeToDie);
    }

}
