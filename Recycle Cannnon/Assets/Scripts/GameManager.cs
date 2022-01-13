using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public TextMeshProUGUI ui_text_life;
    public Image ui_image_ammo_type;
    public TextMeshProUGUI ui_text_ammo_ammount;

    public Player player;
    public Cannon cannon;

    void Awake()
    {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifeUI();
        UpdateAmmoTypeUI();
        UpdateAmmoAmmountUI();
    }

    private void UpdateLifeUI(){
        ui_text_life.text = "Lifes: "+player.health.curHealth;
    }

    public void UpdateAmmoTypeUI(){
        if(cannon.trashAmmo.Count > 0){
            switch (cannon.trashAmmo[0])
            {
                case TrashType.ORGANIC:
                    ui_image_ammo_type.color = new Color(0.45f, 0.25f, 0.15f);
                    break;
                case TrashType.METAL:
                    ui_image_ammo_type.color = Color.yellow;
                    break;
                case TrashType.PLASTIC:
                    ui_image_ammo_type.color = Color.red;
                    break;
            }
        } else {
            ui_image_ammo_type.color = Color.white;
        }
    }

    private void UpdateAmmoAmmountUI(){
        ui_text_ammo_ammount.text = "Ammo: " + cannon.trashAmmo.Count;
    }

    public void EndGame(){
        Time.fixedDeltaTime = 0;
    }
}
