using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    
    public TrashType type = TrashType.ORGANIC;

}
public enum TrashType{
    ORGANIC, METAL, PLASTIC
}
