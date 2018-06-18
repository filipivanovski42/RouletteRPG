using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public enum Types { Revolver, Pendant, Helm, Tunic }
    public enum Qualities { Common, Uncommon, Rare, Epic };

    public Types itemType;
    public Qualities itemQuality;

    public int itemLevel;
    public string itemName;

    //this is for the purpose of the RandomlyGenerateStats function.
    int[] stats = new int[5] { 1, 2, 3, 4, 5 };

    //items can have the following stats        
    public int maximumHealth = 0;
    public int maximumMana = 0;
    public int healthRegen = 0;
    public int manaRegen = 0;
    public int dodgeChance = 0;

    //base stat for revolvers, can only occur on revolvers.
    public int chamberCapacity = 0;

    //base stat for non-revolvers, can only occur on non-revolvers.
    public int damageReduction = 0;

    //the player starts with one item which is defined here.
    public Item startingRevolver;

    //This function is using a random number generator to decide the type of the item.
    public void RollItemType()
    {
        int enumSize = Enum.GetNames(typeof(Types)).Length;
        int temp = UnityEngine.Random.Range(0, enumSize);
        itemType = (Types)temp;
    }

    //This function is using a random number generator to decide the quality of the item.
    public void RollItemQuality()
    {
        int quality;

        //initializing a temporary variable and settiing it's value to be random between the ranges of 1 and 100.
        int temp = UnityEngine.Random.Range(1, 101);

        //setting the quality of the item based on the temp variable's value.
        if (temp <= 55) quality = 0;            //55% chance for the item to be of common quality.
        else if (temp <= 85) quality = 1;       //30% chance for the item to be of uncommon quality.
        else if (temp <= 95) quality = 2;       //10% chance for the item to be of rare quality.
        else quality = 3;                       //5% chance for the item to be of epic quality.

        itemQuality = (Qualities)quality;   
    }

    //Constructs a name for the item based on it's quality, type, and item level.
    public void GenerateItemName()
    {
        //if the item's quality is epic, the name will be resolved in the following if statement.
        if ((int)itemQuality == 3)   
        {
            if ((int)itemType == 0) itemName = "Handgun of the Visually Impaired Marksman";
            else if ((int)itemType == 1) itemName = "Pendant of the Visually Impaired Marksman";
            else if ((int)itemType == 2) itemName = "Faceguard of the Visually Impaired Marksman";
            else if ((int)itemType == 3) itemName = "Chestguard of the Visually Impaired Marksman";
            else Debug.LogError("Not sure how to name item of type " + (int)itemType + ". Constructing the name for this epic item failed.");

            return;
        }

        //If the item's quality isn't epic, the itemName string is constructed in the rest of this function, starting with an empty string.
        string tempName = "";

        //adding prefix based on quality
        if ((int)itemQuality == 1) tempName += "Superior ";
        else if ((int)itemQuality == 2) tempName += "Exceptional ";

        //adding the middle of the itemName based on type
        if ((int)itemType == 0) tempName += "Pistol";
        else if ((int)itemType == 1) tempName += "Amulet";
        else if ((int)itemType == 2) tempName += "Faceguard";
        else if ((int)itemType == 3) tempName += "Chestpiece";
        else Debug.LogError("Not sure how to name item of type " + (int)itemType + ". Constructing the name for this non-epic item failed.");        

        tempName += " ";

        //adding the sufix based on item level
        if (itemLevel < 5) tempName += "of the Apprentice";
        else if (itemLevel < 10) tempName += "of the Novice";
        else if (itemLevel < 15) tempName += "of the Adept";
        else if (itemLevel < 20) tempName += "of the Expert";
        else if (itemLevel >= 20) tempName += "of the Master";
        else Debug.LogError("Not sure how to add sufix for this item level: " + itemLevel);

        //the construction of the name is finished
        itemName = tempName;
    }

    //The stats for the item depend on the item's quality. Common means the item only has a base stat, but the value of the base stat is increased (capacity for revolver, damage reduction for armor). 
    //Uncommon means base stat + 1 additional stat. Rare is +2 stats and Epic is +2 stats with increased base stat. Common and uncommon equipment should be at about the same power level, while rare should be higher than that,
    //and epic should be best in slot. The game should be balanced without taking epics into consideration and they should be very rare. Epics should also scale with level, so if you get an epic you are done with that equipment slot.
    public void GenerateItemStats()
    {
        //adds base stats to an item. adds additional base stats if the item's quality is normal or epic.
        if (itemType == Types.Revolver)
        {            
            chamberCapacity = 6 + itemLevel % 4;
            if (itemQuality == Qualities.Common || itemQuality == Qualities.Epic)
            {
                chamberCapacity++;
            }
        }
        else if (itemType != Types.Revolver)
        {
            damageReduction = 5 + (itemLevel * 2);
            if (itemQuality == Qualities.Common || itemQuality == Qualities.Epic)
            {
                damageReduction += 2;
            }
        }

        //if the item quality is higher than common, add additional stats.
        if (itemQuality == Qualities.Uncommon)
        {
            RandomlyAddStats(1);
        }
        else if (itemQuality == Qualities.Rare || itemQuality == Qualities.Epic)
        {
            RandomlyAddStats(2);
        }
    }

    private void RandomlyAddStats(int amountOfStatsToAdd)
    {
        FisherYatesShuffle(stats);

        for (int i = 0; i < amountOfStatsToAdd; i++)
        {
            AddStat(stats[i]);
        }
    }

    //shuffles the given int array using the Fisher-Yates algorithm.
    private void FisherYatesShuffle(int[] stats)
    {
        // Loops through array
        for (int i = stats.Length - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            int temp = stats[i];

            // Swap the new and old values
            stats[i] = stats[rnd];
            stats[rnd] = temp;
        }
    }

    //adds the stat n to an item.
    private void AddStat(int n)
    {
        if (n == 1) AddMaximumHealthBasedOnItemLevel();
        else if (n == 2) AddMaximumManaBasedOnItemLevel();
        else if (n == 3) AddHealthRegenBasedOnItemLevel();
        else if (n == 4) AddManaRegenBasedOnItemLevel();
        else if (n == 5) AddDodgeChanceBasedOnItemLevel();
        else Debug.LogError("Invalid stat number. Didn't add anything!");
    }

    //the following functions decide the value of the non-base stats for an item of a given itemLevel, and add that stat to the item.    
    private void AddMaximumHealthBasedOnItemLevel()
    {
        maximumHealth += 10 + (itemLevel * 3);
    }
    
    private void AddMaximumManaBasedOnItemLevel()
    {
        maximumMana += 8 + (itemLevel * 2);
    }
    
    private void AddHealthRegenBasedOnItemLevel()
    {
        healthRegen += 5 + (3 * itemLevel);
    }

    private void AddManaRegenBasedOnItemLevel()
    {
        manaRegen += 5 + (2 * itemLevel);
    }

    private void AddDodgeChanceBasedOnItemLevel()
    {
        dodgeChance += 3 + itemLevel;
    }    

    //creates the starting revolver for the player.
    public static Item StartingRevolver()
    {
        Item temp = new Item
        {
            itemName = "Rusty Revolver",
            itemQuality = Qualities.Common,
            itemType = Types.Revolver,
            itemLevel = 1,
            chamberCapacity = 6
        };
        return temp;
    }
}
