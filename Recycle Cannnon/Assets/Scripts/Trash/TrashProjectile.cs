using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashProjectile : MonoBehaviour
{
    
    public string monsterTag = "Monster";
    public float velocity = 5f;
    public TrashType type = TrashType.ORGANIC;
    public float timeToDie = 3f;

    void Awake()
    {
        Destroy(gameObject, timeToDie);
    }

    void Update()
    {
        MoveForward();
    }

    private void MoveForward(){
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag(monsterTag)){
            MonsterBase monster = collider.gameObject.GetComponent<MonsterBase>();
            if(monster != null){
                if(monster.type == TrashType.ORGANIC && type != TrashType.ORGANIC){
                    monster.health.TakeDamage(1);
                } else if(monster.type != TrashType.ORGANIC && type == TrashType.ORGANIC){
                    monster.health.TakeDamage(1);
                }
                Destroy(gameObject);
            }
        }
    }

}
