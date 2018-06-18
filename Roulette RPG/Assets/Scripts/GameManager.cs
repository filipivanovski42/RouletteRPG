using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private UIManager ui; 

    //creates a new player object which has the default player stats defined in player.cs.
    public Player currentPlayer = new Player();

    //this list is holding the items which the player sifts through to find upgrades.
    private List<Item> loot = new List<Item>();     //TODO

    void Start() {
        ui = FindObjectOfType<UIManager>();

        //when the game starts, the player doens't see the player stats or the item display panel.
        ui.inGameUIObjectsHolder.SetActive(false);
        ui.itemDisplayPanel.SetActive(false);
    }

    //starts a new game.
    public void StartGame()
    {
        currentPlayer = new Player();
        currentPlayer.equippedRevolver = Item.StartingRevolver();
        currentPlayer.UpdateInventoryStatBonuses();
        ui.DisplayPlayerStats();
        ui.inGameUIObjectsHolder.SetActive(true);
    }

    //handles player death.
    public void EndGame()
    {
        ui.inGameUIObjectsHolder.SetActive(false);
    }

    //increments bullets in chamber.
    public void IncrementBullets()
    {
        if (currentPlayer.currentBullets < currentPlayer.maxBullets)
        {
            currentPlayer.currentBullets++;
            ui.DisplayPlayerStats();
        }
        else
        {
            Debug.Log("The revolver can't hold any more bullets!");
        }       
    }

    //decrements bullets in chamber.
    public void DecrementBullets()
    {
        if (currentPlayer.currentBullets > 0)
        {
            currentPlayer.currentBullets--;
            ui.DisplayPlayerStats();
        }
        else
        {
            Debug.Log("No bullets left to remove!");
        }
    }

    //handles firing the gun.
    public void Fire()
    {
        if (currentPlayer.currentBullets < 1)
        {
            Debug.Log("Can't fire without any bullets in the chamber.");
            return;
        }

        int bulletFired = Random.Range(1, currentPlayer.maxBullets);

        if (bulletFired <= currentPlayer.currentBullets)
        {
            Debug.Log("A bullet was fired.");

            //if the player didn't dodge the bullet, proceed to damage the player.
            if (!currentPlayer.Dodge())
            {
                Debug.Log("You didn't dodge the bullet.");
                if (currentPlayer.currentHealth < currentPlayer.Damage())
                {
                    EndGame();
                }
                currentPlayer.currentHealth -= currentPlayer.Damage();
            }
            else
            {
                Debug.Log("You dodged a bullet. Phew.");
            }
        }
        else
        {
            Debug.Log("An empty round was fired.");

            currentPlayer.currentHealth += currentPlayer.healthRegenOnSafeShot;
            Debug.Log("Healing on safe shot for " + currentPlayer.healthRegenOnSafeShot);

            currentPlayer.currentMana += currentPlayer.manaRegenOnSafeShot;
            Debug.Log("Regenerating mana on safe shot for " + currentPlayer.manaRegenOnSafeShot);

            currentPlayer.AddExperience();
        }
        ui.DisplayPlayerStats();
    }

    //This is for testing purposes.
    public void GenerateItem()
    {
        
        
        ui.DisplayItem(NewRandomItem());
    }

    //generates a random item and returns it
    private Item NewRandomItem()
    {
        Item tempItem = new Item();

        tempItem.itemLevel = currentPlayer.level;    //This has to be called first because the functions below use the itemLevel variable.
        tempItem.RollItemType();
        tempItem.RollItemQuality();
        tempItem.GenerateItemName();
        tempItem.GenerateItemStats();

        return tempItem;
    }
}