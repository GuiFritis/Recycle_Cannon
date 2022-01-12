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

    public void Die(){
        Destroy(this.gameObject, timeToDie);
    }

}
