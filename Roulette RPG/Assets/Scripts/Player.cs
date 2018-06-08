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

    public int currentBullets = 0;                  //How many bullets are currently loaded into the active pistol's chamber.
    public int maxBullets = 6;                      //How many bullets can the active pistol hold at full capacity.
    
    public int damageBase = 55;                     //The base damage that the player does to himself.
    public int damagePerLevelModifier = 5;          //How much more damage the player does to himself per level.
    public int damageReduction = 0;                 //Flat damage reduction.

    public int HitChance()
    {
        int temp;
        float div = (float)currentBullets / (float)maxBullets;
        temp = (int)Mathf.Ceil(100 * div);
        return temp;
    }

    public int Damage()
    {
        return damageBase + (level * damagePerLevelModifier) - damageReduction;
    }

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
}