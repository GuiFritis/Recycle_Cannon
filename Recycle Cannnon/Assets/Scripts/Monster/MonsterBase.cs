using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class MonsterBase : MonoBehaviour
{
    public TrashType type = TrashType.ORGANIC;
    public HealthBase health;
    public float timeToDie = 0.3f;
    public GameObject player;
    public GameObject wall;
    public float distanceToFollow = 3f;
    public string playerTag = "Player";
    public float speed = 5f;

    void OnValidate()
    {
        health = GetComponent<HealthBase>();
    }

    void Start()
    {
        if(player == null){
            player = GameObject.FindWithTag(playerTag);
        }
        health.OnDie += Die;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= distanceToFollow){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, wall.transform.position, speed*Time.deltaTime);
        }
    }

    public void Die(){
        speed = 0f;
        Destroy(this.gameObject, timeToDie);
    }

}
