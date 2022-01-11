using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public TrashType type = TrashType.ORGANIC;

    [Range(3,5)]
    public int trashSize = 3;

    void Start()
    {
        transform.localScale = Vector3.one * 0.1f * trashSize;
    }

}
public enum TrashType{
    ORGANIC, METAL, PLASTIC
}
