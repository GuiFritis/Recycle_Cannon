using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{

    public Cannon cannon;
    public TrashType type;

    public void PutTrash(int trashCollected = 1){
        cannon.AddTrash(type, trashCollected);
    }

}
