using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public TextMeshProUGUI uiTextLife;
    public Image uiImageAmmoType;
    public TextMeshProUGUI uiTextAmmoAmmount;
    public TextMeshProUGUI uiPanelWallLife;

    public HealthBase playerHealth;
    public Cannon cannon;
    public HealthBase wallHealth;

    public GameObject gameOverScreen;
    public GameObject winGameScreen;

    public int monstersCount = 0;

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
        UpdateWallLifeUI();
    }

    private void UpdateLifeUI(){
        uiTextLife.text = "Lifes: "+ playerHealth.curHealth;
    }

    public void UpdateAmmoTypeUI(){
        if(cannon.trashAmmo.Count > 0){
            switch (cannon.trashAmmo[0])
            {
                case TrashType.ORGANIC:
                    uiImageAmmoType.color = new Color(0.45f, 0.25f, 0.15f);
                    break;
                case TrashType.METAL:
                    uiImageAmmoType.color = Color.yellow;
                    break;
                case TrashType.PLASTIC:
                    uiImageAmmoType.color = Color.red;
                    break;
            }
        } else {
            uiImageAmmoType.color = Color.white;
        }
    }

    private void UpdateAmmoAmmountUI(){
        uiTextAmmoAmmount.text = "Ammo: " + cannon.trashAmmo.Count;
    }

    private void UpdateWallLifeUI(){
        uiPanelWallLife.text = "Wall life: " + wallHealth.curHealth;
        if(wallHealth == null || wallHealth.curHealth == 0){
            gameOverScreen.SetActive(true);
        }
    }

    public void EndGame(){
        Time.fixedDeltaTime = 0;
    }

    public void MonsterKilled(){
        monstersCount--;
        if(monstersCount == 0){
            winGameScreen.SetActive(true);
        }
    }
}
