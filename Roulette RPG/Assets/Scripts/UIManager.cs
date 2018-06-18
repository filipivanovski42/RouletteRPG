using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameManager gm;

    //the button which starts or restarts the game.
    [HideInInspector]
    public GameObject startRestartButton;
    //the button's text component.
    [HideInInspector]
    public Text startRestartButtonText;

    //this game object is the parent of all the UI objects which should be displayed while the user is in game.
    [HideInInspector]
    public GameObject inGameUIObjectsHolder;

    //this panel displays selected items.
    [HideInInspector]
    public GameObject itemDisplayPanel;

    //initialize the UI gameobjects which have to do with player stats.
    private GameObject bulletsInChamberDisplay;
    private GameObject hitChanceDisplay;
    private GameObject damageDisplay;
    private GameObject healthDisplay;
    private GameObject manaDisplay;
    private GameObject experienceDisplay;

    //initialize the UI gameobjects which have to do with the item display.
    private GameObject itemNameDisplay;
    private GameObject itemQualityTypeDisplay;
    private GameObject requiredLevelDisplay;
    private GameObject baseStatDisplay;
    private GameObject firstStatDisplay;
    private GameObject secondStatDisplay;

    private Image itemIconImage;

    //initialize the texts of the player stats UI GameObjects.
    private Text bulletsInChamberDisplayText;
    private Text hitChanceDisplayText;
    private Text damageDisplayText;
    private Text healthDisplayText;
    private Text manaDisplayText;
    private Text experienceDisplayText;

    //initialize the texts of the item display UI GameObjects.
    private Text itemNameDisplayText;
    private Text itemQualityTypeDisplayText;
    private Text requiredLevelDisplayText;
    private Text baseStatDisplayText;
    private Text firstStatDisplayText;
    private Text secondStatDisplayText;

    //initialize sprites
    private Sprite revolverSprite;
    private Sprite pendantSprite;
    private Sprite helmSprite;
    private Sprite tunicSprite;

    void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();

        startRestartButton = GameObject.Find("Canvas/Game Area/Start Restart button");
        startRestartButtonText = GetComponentInChildren<Text>();
        startRestartButtonText.text = "Start";

        inGameUIObjectsHolder = GameObject.Find("In-game objects holder");
        itemDisplayPanel = GameObject.Find("Item Display Panel");

        //Gets control of the player stats UI GameObjects.
        bulletsInChamberDisplay = GameObject.Find("Bullets in chamber display");
        hitChanceDisplay = GameObject.Find("Hit chance display");
        damageDisplay = GameObject.Find("Damage display");
        healthDisplay = GameObject.Find("Health display");
        manaDisplay = GameObject.Find("Mana display");
        experienceDisplay = GameObject.Find("Experience display");

        //Gets control of the Item Display GameObjects.
        itemNameDisplay = GameObject.Find("Item Name");
        itemQualityTypeDisplay = GameObject.Find("Item Quality + Type");
        requiredLevelDisplay = GameObject.Find("Required Level");
        baseStatDisplay = GameObject.Find("Base Stat");
        firstStatDisplay = GameObject.Find("First Stat");
        secondStatDisplay = GameObject.Find("Second Stat");        

        //Gets control of the Text component of the player stats UI GameObjects.
        bulletsInChamberDisplayText = bulletsInChamberDisplay.GetComponent<Text>();
        hitChanceDisplayText = hitChanceDisplay.GetComponent<Text>();
        damageDisplayText = damageDisplay.GetComponent<Text>();
        healthDisplayText = healthDisplay.GetComponent<Text>();
        manaDisplayText = manaDisplay.GetComponent<Text>();
        experienceDisplayText = experienceDisplay.GetComponent<Text>();

        //Gets control of the Text component of the Item Display GameObjects.
        itemNameDisplayText = itemNameDisplay.GetComponent<Text>();
        itemQualityTypeDisplayText = itemQualityTypeDisplay.GetComponent<Text>();
        requiredLevelDisplayText = requiredLevelDisplay.GetComponent<Text>();
        baseStatDisplayText = baseStatDisplay.GetComponent<Text>();
        firstStatDisplayText = firstStatDisplay.GetComponent<Text>();
        secondStatDisplayText = secondStatDisplay.GetComponent<Text>();

        itemIconImage = GameObject.Find("Item Icon").GetComponent<Image>();

        revolverSprite = (Sprite)Resources.Load<Sprite>("icon_revolver") as Sprite;
        pendantSprite = (Sprite)Resources.Load<Sprite>("icon_pendant") as Sprite;
        helmSprite = (Sprite)Resources.Load<Sprite>("icon_helm") as Sprite;
        tunicSprite = (Sprite)Resources.Load<Sprite>("icon_tunic") as Sprite;
    }

    //Updates the player stats on the UI. Should be called whenever any of them change.
    public void DisplayPlayerStats()
    {        
        bulletsInChamberDisplayText.text = "Bullets in chamber: " + gm.currentPlayer.currentBullets.ToString() + "/" + gm.currentPlayer.maxBullets.ToString();
        hitChanceDisplayText.text = "Chance to hit: " + gm.currentPlayer.HitChance().ToString() + "%";
        damageDisplayText.text = "Damage: " + gm.currentPlayer.Damage().ToString();
        healthDisplayText.text = "Health: " + gm.currentPlayer.currentHealth.ToString() + "/" + gm.currentPlayer.maxHealth;
        manaDisplayText.text = "Mana: " + gm.currentPlayer.maxMana.ToString() + "/" + gm.currentPlayer.maxMana;
        experienceDisplayText.text = "Level: " + gm.currentPlayer.level.ToString() + " " + "Exp: " + gm.currentPlayer.currentExperience.ToString() + "/" + gm.currentPlayer.maxExperience.ToString();
    }

    //Displays an item (i) on the UI.
    public void DisplayItem(Item currentItem)
    {
        //check if the item display panel is disabled and if so, enable it.
        if (itemDisplayPanel.activeInHierarchy == false)
        {
            itemDisplayPanel.SetActive(true);
        }

        itemNameDisplayText.text = currentItem.itemName;
        itemQualityTypeDisplayText.text = currentItem.itemQuality + " " + currentItem.itemType;
        requiredLevelDisplayText.text = "Item Level: " + currentItem.itemLevel.ToString();
        DisplayItemStats(currentItem);
        DisplayItemIcon(currentItem);
    }

    //checks how many base and bonus stats the item has, and displays them on the item display panel.
    private void DisplayItemStats(Item currentItem)
    {
        //TODO use the commented code below to fix the abomination that is this function.

        ////handles display of base stats
        //if (currentItem.itemType == Item.Types.Revolver) baseStatDisplayText.text = "Chamber Capacty: " + currentItem.chamberCapacity.ToString();
        //else if (currentItem.itemType != Item.Types.Revolver) baseStatDisplayText.text = "+ " + currentItem.damageReduction.ToString() + " Damage Reduction.";

        ////handles display of secondary stats
        //if ((int)currentItem.itemQuality == 0)          //if common
        //{
        //    firstStatDisplayText.text = "";
        //    secondStatDisplayText.text = "";
        //}
        //else if ((int)currentItem.itemQuality == 1)     //if uncommon
        //{
        //    //TODO find out which is the single stat and display it.
        //    secondStat = "";
        //}
        //else if ((int)currentItem.itemQuality == 2 || (int)currentItem.itemQuality == 3)    //if rare or epic
        //{
        //    //TODO find out which are the two stats and display them.
        //}

        //handles display of base stats
        if (currentItem.itemType == Item.Types.Revolver) baseStatDisplayText.text = "Chamber Capacty: " + currentItem.chamberCapacity.ToString();
        else if (currentItem.itemType != Item.Types.Revolver) baseStatDisplayText.text = "+ " + currentItem.damageReduction.ToString() + " Damage Reduction.";

        int statsGreaterThanZero = 0;
        bool firstStatDisplayed = false;

        if (currentItem.maximumHealth > 0)
        {
            statsGreaterThanZero++;
            if (!firstStatDisplayed)
            {
                firstStatDisplayText.text = "+ " + currentItem.maximumHealth.ToString() + " Maximum Health";
                firstStatDisplayed = true;
            }
            else if (firstStatDisplayed)
            {
                secondStatDisplayText.text = "+ " + currentItem.maximumHealth.ToString() + " Maximum Health";
            }
        }
        if (currentItem.maximumMana > 0)
        {
            statsGreaterThanZero++;
            if (!firstStatDisplayed)
            {
                firstStatDisplayText.text = "+ " + currentItem.maximumMana.ToString() + " Maximum Mana";
                firstStatDisplayed = true;
            }
            else if (firstStatDisplayed)
            {
                secondStatDisplayText.text = "+ " + currentItem.maximumMana.ToString() + " Maximum Mana";
            }
        }
        if (currentItem.healthRegen > 0)
        {
            statsGreaterThanZero++;
            if (!firstStatDisplayed)
            {
                firstStatDisplayText.text = "+ " + currentItem.healthRegen.ToString() + " Health Regeneration";
                firstStatDisplayed = true;
            }
            else if (firstStatDisplayed)
            {
                secondStatDisplayText.text = "+ " + currentItem.healthRegen.ToString() + " Health Regeneration";
            }
        }
        if (currentItem.manaRegen > 0)
        {
            statsGreaterThanZero++;
            if (!firstStatDisplayed)
            {
                firstStatDisplayText.text = "+ " + currentItem.manaRegen.ToString() + " Mana Regeneration";
                firstStatDisplayed = true;
            }
            else if (firstStatDisplayed)
            {
                secondStatDisplayText.text = "+ " + currentItem.manaRegen.ToString() + " Mana Regeneration";
            }
        }
        if (currentItem.dodgeChance > 0)
        {
            statsGreaterThanZero++;
            if (!firstStatDisplayed)
            {
                firstStatDisplayText.text = "+ " + currentItem.dodgeChance.ToString() + "% Chance to Dodge";
                firstStatDisplayed = true;
            }
            else if (firstStatDisplayed)
            {
                secondStatDisplayText.text = "+ " + currentItem.dodgeChance.ToString() + "% Chance to Dodge";
            }
        }

        if (statsGreaterThanZero < 2)
        {
            if (statsGreaterThanZero < 1)
            {
                firstStatDisplayText.text = "";
            }
            secondStatDisplayText.text = "";
        }
    }

    //is responsible for displaying the proper item icon to the user.
    private void DisplayItemIcon(Item currentItem)
    {
        if (currentItem.itemType == Item.Types.Revolver) itemIconImage.sprite = revolverSprite;
        else if (currentItem.itemType == Item.Types.Pendant) itemIconImage.sprite = pendantSprite;
        else if (currentItem.itemType == Item.Types.Helm) itemIconImage.sprite = helmSprite;
        else if (currentItem.itemType == Item.Types.Tunic) itemIconImage.sprite = tunicSprite;
        else Debug.LogWarning("Couldn't find a sprite to display!");
    }
}