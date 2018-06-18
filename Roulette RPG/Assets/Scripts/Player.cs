using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int maxHealth = 80;                      //The player's current health pool.
    public int maxMana = 50;                        //The player's current mana pool.

    public int currentHealth = 80;                  //The player's current health.
    public int currentMana = 50;                    //The player's current mana.

    public int level = 1;                           //The player's current level.
    public int currentExperience = 0;               //The player's progress towards the next level.
    public int maxExperience = 30;                  //Experience cap.
    public int maxExperiencePerLevel = 10;          //How much does the experience cap grow per level.
    public int safeShotExperience = 5;              //Experience gained by shooting a blank.

    public int healthRegenOnSafeShot = 0;            //When the player fires an empty round, he heals for this amount.
    public int manaRegenOnSafeShot = 0;

    public int currentBullets = 0;                  //How many bullets are currently loaded into the active pistol's chamber.
    public int maxBullets = 0;                      //How many bullets can the active pistol hold at full capacity.
    
    public int damageBase = 55;                     //The base damage that the player does to himself.
    public int damagePerLevelModifier = 5;          //How much more damage the player does to himself per level.
    public int damageReduction = 0;                 //Flat damage reduction.

    public int dodgeChance = 0;                     //Chance to avoid being hit if a bullet is fired.

    //the player's equipped items are null at the start of the game.
    public Item equippedRevolver = null;
    public Item equippedPendant = null;
    public Item equippedHelm = null;
    public Item equippedTunic = null;

    //calculates the chance for the player to hit himself when firing.
    public int HitChance()
    {
        int temp;
        float div = (float)currentBullets / (float)maxBullets;
        temp = (int)Mathf.Ceil(100 * div);
        return temp;
    }

    //calculates the damage done to the player if a bullet hits.
    public int Damage()
    {
        return damageBase + (level * damagePerLevelModifier) - damageReduction;
    }

    //adds experience to the player and handles leveling up.
    public void AddExperience()
    {
        currentExperience += safeShotExperience;
        if (currentExperience >= maxExperience)
        {
            currentExperience -= maxExperience;
            maxExperience += maxExperiencePerLevel;
            level++;
        }
    }

    //calculates the player's stat bonusses gained from equipped items.
    public void UpdateInventoryStatBonuses()
    {
        if (equippedRevolver != null) UpdateEquippedItemStats(equippedRevolver);
        if (equippedPendant != null) UpdateEquippedItemStats(equippedPendant);
        if (equippedHelm != null) UpdateEquippedItemStats(equippedHelm);
        if (equippedTunic != null) UpdateEquippedItemStats(equippedTunic);

        Debug.Log("Inventory stat bonusses updated!");
    }

    //calculates the player's stat bonusses gained from equipped items for a given item slot.
    private void UpdateEquippedItemStats(Item equippedItem)
    {
        maxBullets += equippedItem.chamberCapacity;
        maxHealth += equippedItem.maximumHealth;
        maxMana += equippedItem.maximumMana;
        healthRegenOnSafeShot += equippedItem.healthRegen;
        manaRegenOnSafeShot += equippedItem.manaRegen;
        dodgeChance += equippedItem.dodgeChance;
    }

    //decides if the player manages to dodge.
    public bool Dodge()
    {        
        int temp = Random.Range(1, 101);
        if (temp <= dodgeChance)
        {
            return true;
        }

        return false;
    }
}