using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    
    public int maxHealth = 3;

    public int curHealth {get; private set;}

    public delegate void DieCallback();
    public event DieCallback OnDie;

    void Awake()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(int damage = 1){
        curHealth -= damage;
        if(curHealth == 0){
            Die();
        }
    }
    
    public void Die(){
        if(OnDie != null){
            OnDie();
        }
    }
}
