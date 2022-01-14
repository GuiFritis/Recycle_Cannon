using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    
    public List<MonsterBase> monsters;
    public float minTimeToSpawn = 3f;
    public float maxTimeToSpawn = 8f;

    public GameObject player;
    public HealthBase wallHealth;


    void Awake()
    {
        StartCoroutine(nameof(CallWave));
        GameManager.Instance.monstersCount = monsters.Count;
    }

    private IEnumerator CallWave(){
        monsters[0].player = player;
        monsters[0].wall = wallHealth;
        yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));
        GameObject monster = Instantiate(monsters[0].gameObject, transform.position, transform.rotation);
        monster.transform.position = new Vector3(Random.Range(-transform.localScale.x/2, transform.localScale.x/2), transform.position.y, transform.position.z);
        monsters.RemoveAt(0);
        if(monsters.Count > 0){
            StartCoroutine(nameof(CallWave));
        }
    }

}
