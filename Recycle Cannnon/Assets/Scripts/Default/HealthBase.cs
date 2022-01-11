using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    
    public int maxHealth;

    private int _curHealth;

    public delegate void DieCallback();
    public event DieCallback OnDie;

    void Awake()
    {
        _curHealth = maxHealth;
    }

    public void TakeDamage(int damage = 1){
        _curHealth -= damage;
        if(_curHealth == 0){
            Die();
        }
    }
    
    public void Die(){
        if(OnDie != null){
            OnDie();
        }
    }
}
