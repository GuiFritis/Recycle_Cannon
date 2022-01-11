using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class MonsterBase : MonoBehaviour
{
    public TrashType type = TrashType.ORGANIC;
    public HealthBase health;
    public float timeToDie = 0.3f;
    public string projectileTag = "TrashProjectile";

    void OnValidate()
    {
        health = GetComponent<HealthBase>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag(projectileTag)){
            health.TakeDamage(1);
        }
    }

    public void Die(){
        Destroy(this.gameObject, timeToDie);
    }

}
