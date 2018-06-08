using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject holder;

    private GameManager gm;
    
    private GameObject bulletsInChamberDisplay;
    private GameObject hitChanceDisplay;
    private GameObject damageDisplay;
    private GameObject healthDisplay;
    private GameObject manaDisplay;
    private GameObject experienceDisplay;

    Text bulletsInChamberDisplayText;
    Text hitChanceDisplayText;
    Text damageDisplayText;
    Text healthDisplayText;
    Text manaDisplayText;
    Text experienceDisplayText;

    private GameObject itemNameDisplay;
    private GameObject itemQualityTypeDisplay;
    private GameObject requiredLevelDisplay;

    Text itemNameDisplayText;
    Text itemQualityTypeDisplayText;
    Text requiredLevelDisplayText;

    void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();

        holder = GameObject.Find("In-game objects holder");

        //Gets control of the UI GameObjects.
        bulletsInChamberDisplay = GameObject.Find("Bullets in chamber display");
        hitChanceDisplay = GameObject.Find("Hit chance display");
        damageDisplay = GameObject.Find("Damage display");
        healthDisplay = GameObject.Find("Health display");
        manaDisplay = GameObject.Find("Mana display");
        experienceDisplay = GameObject.Find("Experience display");

        //Gets control of the Text component of the same GameObjects.
        bulletsInChamberDisplayText = bulletsInChamberDisplay.GetComponent<Text>();
        hitChanceDisplayText = hitChanceDisplay.GetComponent<Text>();
        damageDisplayText = damageDisplay.GetComponent<Text>();
        healthDisplayText = healthDisplay.GetComponent<Text>();
        manaDisplayText = manaDisplay.GetComponent<Text>();
        experienceDisplayText = experienceDisplay.GetComponent<Text>();

        //Gets control of the Item Display GameObjects.
        itemNameDisplay = GameObject.Find("Item Name");
        itemQualityTypeDisplay = GameObject.Find("Item Quality + Type");
        requiredLevelDisplay = GameObject.Find("Required Level");

        //Gets control of the Text component of the Item Display GameObjects.
        itemNameDisplayText = itemNameDisplay.GetComponent<Text>();
        itemQualityTypeDisplayText = itemQualityTypeDisplay.GetComponent<Text>();
        requiredLevelDisplayText = requiredLevelDisplay.GetComponent<Text>();
    }

    //Updates all the text elements of the GUI. Should be called whenever any of them change.
    public void UpdateUI()
    {        
        bulletsInChamberDisplayText.text = "Bullets in chamber: " + gm.currentPlayer.currentBullets.ToString() + "/" + gm.currentPlayer.maxBullets.ToString();
        hitChanceDisplayText.text = "Chance to hit: " + gm.currentPlayer.HitChance().ToString() + "%";
        damageDisplayText.text = "Damage: " + gm.currentPlayer.Damage().ToString();
        healthDisplayText.text = "Health: " + gm.currentPlayer.currentHealth.ToString() + "/" + gm.currentPlayer.maxHealth;
        manaDisplayText.text = "Mana: " + gm.currentPlayer.maxMana.ToString() + "/" + gm.currentPlayer.maxMana;
        experienceDisplayText.text = "Level: " + gm.currentPlayer.level.ToString() + " " + "Exp: " + gm.currentPlayer.currentExperience.ToString() + "/" + gm.currentPlayer.maxExperience.ToString();
    }

    public void DisplayItem(Item i)
    {
        itemNameDisplayText.text = i.itemName;
        itemQualityTypeDisplayText.text = i.itemQuality + " " + i.itemType;
        requiredLevelDisplayText.text = "Item Level: " + i.requiredLevel.ToString();
    }
}