using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    
    public TrashType type = TrashType.ORGANIC;
    public int trashSize = 1;

    void Start()
    {
        transform.localScale = Vector3.one * 0.1f * (2 + trashSize);
    }

}
public enum TrashType{
    ORGANIC, METAL, PLASTIC
}
