using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    
    public SO_Int trashCount;
    public TrashType type;

    void Awake()
    {
        trashCount.value = 0;
    }

    public void UseTrash(int trashUsage = 1){
        trashCount.value -= trashUsage;
    }

    public void CollectTrash(int trashCollected = 1){
        trashCount.value += trashCollected;
    }

}
