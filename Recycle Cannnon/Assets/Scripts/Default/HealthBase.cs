using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    
    public int curHealth;
    public int maxHealth;

    public delegate void DieCallback();
    public event DieCallback OnDie;

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
