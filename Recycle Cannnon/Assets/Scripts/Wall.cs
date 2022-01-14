using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public HealthBase health;

    void Awake()
    {
        health.OnDie += OnDie;
    }

    public void OnDie(){
        Destroy(gameObject);
    }
}
