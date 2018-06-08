using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public enum Types { Revolver, Pendant, Helmet, Tunic }
    public enum Qualities { Common, Uncommon, Rare, Epic };

    //The stats for the item depend on the item's quality. Common means the item only has a base stat, but the value of the base stat is increased (capacity for revolver, damage reduction for armor). 
    //Uncommon means base stat + 1 additional stat. Rare is +2 stats and Epic is +2 stats with increased base stat. Common and uncommon equipment should be at about the same power level, while rare should be higher than that,
    //and epic should be best in slot. The game should be balanced without taking epics into consideration and they should be very rare. Epics should also scale with level, so if you get an epic you are done with that equipment slot.

    public Types itemType;
    public Qualities itemQuality;

    public int requiredLevel;
    public string itemName;

    public void RollItemType()
    {
        int enumSize = Enum.GetNames(typeof(Types)).Length;
        int temp = UnityEngine.Random.Range(0, enumSize);
        itemType = (Types)temp;
    }

    public void RollItemQuality()
    {
        int quality = -1;
        int temp = UnityEngine.Random.Range(1, 101);
            if (temp <= 55)
            {
                quality = 0;
            }
            else if (temp <= 85)
            {
                quality = 1;
            }
            else if (temp <= 95)
            {
                quality = 2;
            }
            else if (temp > 85)
            {
                quality = 3;
            }

            if (quality < 0 || quality > 3)
            {
                Debug.Log("RollItemQuality failed to produce a valid quality.");
            return;
            }
        itemQuality = (Qualities)quality;   
    }
    public void GenerateItemName(int ilvl)
    {
        int quality = (int)itemQuality;
        int type = (int)itemType;

        if (quality == 3)   //if the item's quality is epic
        {
            switch (type)
            {
                case 3:     //if tunic
                    itemName = "Chestguard of the Visually Impaired Marksman";
                    return;
                case 2:     //if helm
                    itemName = "Faceguard of the Visually Impaired Marksman";
                    return;
                case 1:     //if pendant
                    itemName = "Pendant of the Visually Impaired Marksman";
                    return;
                case 0:     //if revolver
                    itemName = "Handgun of the Visually Impaired Marksman";
                    return;
                default:    //if not working properly
                    itemName = "Undefined";
                    return;
            }
        }

        string tempName = "";

        switch (quality)
        {
            case 2:     //if rare
                tempName += "Rare";
                break;
            case 1:     //if uncommon
                tempName += "Uncommon";
                break;
            default:    //if common
                tempName += "Common";
                break;
        }

        tempName += " ";

        switch (type)
        {
            case 3:     //if tunic
                tempName += "Chestguard";
                break;
            case 2:     //if helm
                tempName += "Helmet";
                break;
            case 1:     //if pendant
                tempName += "Necklace";
                break;
            case 0:
                tempName += "Pistol";
                break;
            default:    //if revolver
                tempName += "Undefined";
                break;
        }

        tempName += " ";

        if (ilvl < 5) tempName += "of the Apprentice";
        else if (ilvl < 10) tempName += "of the Novice";
        else if (ilvl < 15) tempName += "of the Adept";
        else if (ilvl < 20) tempName += "of the Expert";
        else if (ilvl > 20) tempName += "of the Master";

        itemName = tempName;
    }
}
