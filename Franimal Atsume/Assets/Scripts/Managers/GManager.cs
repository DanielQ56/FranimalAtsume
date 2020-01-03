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
        if (instance == null)
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
    List<Toy> expansions = new List<Toy>();

    List<Toy> unownedToys = new List<Toy>();

    public void OpenStore()
    {
        store.OpenStore(unownedToys, SeedCounter, expansions);
    }

    public void UnlockArea(AnimalLocation location)
    {
        switch(location)
        {
            case AnimalLocation.Land:
                expansions[0].isOwned = true;
                break;
            case AnimalLocation.Aquatic:
                expansions[1].isOwned = true;
                break;
            case AnimalLocation.Avian:
                expansions[2].isOwned = true;
                break;
        }
    }

    public bool HasUnlockedExpansion(AnimalLocation location)
    {
        switch (location)
        {
            case AnimalLocation.Land:
                return expansions[0].isOwned;
            case AnimalLocation.Aquatic:
                return expansions[1].isOwned;
            case AnimalLocation.Avian:
                return expansions[2].isOwned;
            default:
                return false;
        }
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

    public void AddToyToInventory(Toy t, bool isExpansion)
    {
        SeedCounter -= t.cost;
        t.isOwned = true;
        if(!isExpansion)
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
        WorldData data = SaveGameScript.LoadGame();
        expansions.AddRange(Utilities.instance.NewExpansions);
        if (data != null)
        {
            SeedCounter = data.seeds;
            int counter = 0;
            player.SetInventory(Utilities.instance.SeparateToys(out unownedToys, data.toysOwned));
            foreach (Transform t in LocationParent.gameObject.transform)
            {
                t.GetComponent<SpawnLocation>().Setup(player.GetToy(data.toys[counter]), Utilities.instance.GetFranimalByNameAndSpecies(data.franimals[counter]), System.DateTime.Parse(data.oldTime), data.timeRemaining[counter]);
                counter += 1;
            }
            for(int i = 0; i < data.expansions.Length; ++i)
            {
                expansions[i].isOwned = data.expansions[i];
            }
        }
        else
        {
            SeedCounter = 30;
            player.SetInventory(Utilities.instance.SeparateToys(out unownedToys));
 
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
        List<bool> exp = new List<bool>();
        foreach(Toy t in expansions)
        {
            exp.Add(t.isOwned);
        }
        SaveGameScript.SaveGame(franimal, toys, System.DateTime.Now.ToString(), timeRemaining, SeedCounter, player.GetNamesOfOwnedToys(), exp);
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


    #endregion

    #region Handles Location Functions
    [SerializeField] SpawnParent LocationParent;
    [SerializeField] LocationInformation LocationPanel;
    [SerializeField] GameObject notUnlockedPanel;
    [SerializeField] TextMeshProUGUI errorText;

    public void LookAtLocation(Sprite toy, Sprite franimal, string toyN, string franN, SpawnLocation location)
    {
        LocationPanel.LookAtInformation(toy, franimal, toyN, franN, location);
        PauseGame();
    }

    public void ChangeToyAtLocation(Toy t)
    {
        LocationPanel.UpdateToyAtLocation(t);
    }

    public void HasNotUnlockedExpansion(AnimalLocation loc)
    {
        notUnlockedPanel.SetActive(true);
        errorText.text = string.Format("You have not unlocked the {0} expansion yet!", loc.ToString());
    }

    

    #endregion

    #region Utility Functions
    public void PauseGame()
    {
        LocationParent.PauseSpawning();
    }
    #endregion
}
