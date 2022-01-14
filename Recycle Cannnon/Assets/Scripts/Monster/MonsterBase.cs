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
    public HealthBase wall;
    public float distanceToFollow = 3f;
    public string playerTag = "Player";
    public float speed = 5f;
    public float timeToSpawnTrash = 3f;
    public GameObject PFB_trash;
    public Transform positionToSpawn;

    private bool _walk = true;
    private Vector3 _wallPos = Vector3.zero;

    void OnValidate()
    {
        health = GetComponent<HealthBase>();
    }

    void Awake()
    {
        if(player == null){
            player = GameObject.FindWithTag(playerTag);
        }
    }

    void Start()
    {
        health.OnDie += Die;
        InvokeRepeating(nameof(SpawnTrash), timeToSpawnTrash+1, timeToSpawnTrash);
        if(wall != null){
            _wallPos = new Vector3(transform.position.x, transform.position.y, wall.transform.position.z);
        } 
    }

    void Update()
    {
        Move();
    }

    private void Move(){
        if(_walk){
            if(Vector3.Distance(transform.position, player.transform.position) <= distanceToFollow){
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
            } else {
                transform.position = Vector3.MoveTowards(transform.position, _wallPos, speed*Time.deltaTime);
            }
        }
    }

    private void SpawnTrash(){
        if(_walk){
            Instantiate(PFB_trash, positionToSpawn.position, positionToSpawn.rotation);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == wall.gameObject){
            _walk = false;
            InvokeRepeating(nameof(DamageWall), 1f, 1f);
        }
    }

    private void DamageWall(){
        wall.TakeDamage(1);
    }

    public void Die(){
        CancelInvoke();
        speed = 0f;
        Destroy(this.gameObject, timeToDie);
    }

}
