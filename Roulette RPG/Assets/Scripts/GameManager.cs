using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private UIManager ui;

    public Player currentPlayer = new Player();

    // Use this for initialization
    void Start() {
        ui = FindObjectOfType<UIManager>();

        ui.holder.SetActive(false);
    }

    public void StartGame()
    {
        currentPlayer = new Player();
        ui.UpdateUI();
        ui.holder.SetActive(true);
    }

    public void EndGame()
    {
        ui.holder.SetActive(false);
    }

    public void IncrementBullets()
    {
        if (currentPlayer.currentBullets < currentPlayer.maxBullets)
        {
            currentPlayer.currentBullets++;
            ui.UpdateUI();
        }
        else
        {
            Debug.Log("The revolver can't hold any more bullets!");
        }       
    }

    public void DecrementBullets()
    {
        if (currentPlayer.currentBullets > 0)
        {
            currentPlayer.currentBullets--;
            ui.UpdateUI();
        }
        else
        {
            Debug.Log("No bullets left to remove!");
        }
    }

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
            if (currentPlayer.currentHealth < currentPlayer.Damage())
            {
                EndGame();
            }
            currentPlayer.currentHealth -= currentPlayer.Damage();
        }
        else
        {
            Debug.Log("An empty round was fired.");
            currentPlayer.AddExperience();
        }
        ui.UpdateUI();
    }
    //This is for testing purposes.
    public void GenerateItem()
    {

        Item newItem = new Item();

        newItem.requiredLevel = currentPlayer.level;
        newItem.RollItemType();
        newItem.RollItemQuality();
        newItem.GenerateItemName(currentPlayer.level);

        ui.DisplayItem(newItem);

        Debug.Log("Item type: " + newItem.itemType);
        Debug.Log("Item quality: " + newItem.itemQuality);
    }
}
