using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextUpdater : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    public string pre_text = "";
    public string post_test = "";
    public SO_Int so_int;

    void Update()
    {
        if(textMesh != null){
            textMesh.text = pre_text + so_int.value + post_test;
        }
    }

}
