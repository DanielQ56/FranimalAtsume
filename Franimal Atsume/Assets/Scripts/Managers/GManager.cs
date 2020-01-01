using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[InitializeOnLoadAttribute]
public class GManager : MonoBehaviour
{
    public static GManager instance = null;



    #region Awake/Start Functions
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
    }

    private void Start()
    {
        SaveGameScript.DeleteData();
        SetupDataFromSave();
    }
    #endregion

    #region Shop
    [SerializeField] StoreDisplay store;

    List<Toy> unownedToys = new List<Toy>();

    public void OpenStore()
    {
        PauseGame();
        store.OpenStore(unownedToys);
    }

    #endregion

    #region Handles Player Inventory
    [SerializeField] TextMeshProUGUI seedText;
    [SerializeField] InventoryDisplay inventory;

    PlayerInventory player = new PlayerInventory();

    int SeedCounter
    {
        get { return player.GetSeeds(); }
        set
        {
            player.SetSeeds(value);
            seedText.text = "Seeds: " + player.GetSeeds().ToString();
        }
    }


    public void AddSeedsToPlayerTotal(int seeds)
    {
        SeedCounter += seeds;
    }

    public void AddToyToInventory(Toy t)
    {
        SeedCounter -= t.cost;
        unownedToys.Remove(t);
        player.AddToy(t);
    }

    public void DisplayInventory()
    {
        inventory.OpenInventory(player.GetOwnedToys());
    }

    public bool CanPurchaseToy(Toy t)
    {
        return player.HasSufficientFunds(t.cost);
    }

    #endregion

    #region Handles save data
    void SetupDataFromSave()
    {
        Time.timeScale = 1;
        WorldData data = SaveGameScript.LoadGame();
        if (data != null)
        {
            Debug.Log("Not null?");
            SeedCounter = data.seeds;
            int counter = 0;
            player.SetInventory(Utilities.instance.SeparateToys(data.toysOwned, out unownedToys));
            foreach (Transform t in LocationParent.gameObject.transform)
            {
                t.GetComponent<SpawnLocation>().Setup(player.GetToy(data.toys[counter]), Utilities.instance.GetFranimalByNameAndSpecies(data.franimals[counter]), System.DateTime.Parse(data.oldTime), data.timeRemaining[counter]);
                counter += 1;
            }
        }
        else
        {
            Debug.Log("Here");
            string[] names = { };
            player.SetInventory(Utilities.instance.SeparateToys(names, out unownedToys));
            SeedCounter = 100;
        }
    }

    void SaveData()
    {
        List<string> toys = new List<string>();
        List<string> franimal = new List<string>();
        List<float> timeRemaining = new List<float>();
        SpawnLocation sl;
        foreach(Transform t in LocationParent.gameObject.transform)
        {
            sl = t.GetComponent<SpawnLocation>();
            toys.Add(sl.GetToyName());
            franimal.Add(sl.GetFranimalInfo());
            timeRemaining.Add(sl.GetTimeRemaining());
        }
        SaveGameScript.SaveGame(franimal, toys, System.DateTime.Now.ToString(), timeRemaining, SeedCounter, player.GetNamesOfOwnedToys());
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


    #endregion

    #region Handles Location Functions
    [SerializeField] SpawnParent LocationParent;
    [SerializeField] LocationInformation LocationPanel;

    public void LookAtLocation(Sprite toy, Sprite franimal, string toyN, string franN, SpawnLocation location)
    {
        LocationPanel.LookAtInformation(toy, franimal, toyN, franN, location);
        PauseGame();
    }

    public void ChangeToyAtLocation(Toy t)
    {
        LocationPanel.UpdateToyAtLocation(t);
    }

    

    #endregion

    #region Utility Functions
    public void PauseGame()
    {
        LocationParent.PauseSpawning();
    }
    #endregion
}
