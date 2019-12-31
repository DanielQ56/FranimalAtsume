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
        //SaveGameScript.DeleteData();
        SetupDataFromSave();
    }
    #endregion

    #region Handles Player Inventory
    [SerializeField] TextMeshProUGUI seedText;
    [SerializeField] PlayerInventory player;
    [SerializeField] InventoryDisplay inventory;

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
        player.AddToy(t);
    }

    public void DisplayInventory()
    {
        inventory.OpenInventory(player.GetOwnedToys());
    }

    #endregion

    #region Handles save data
    void SetupDataFromSave()
    {
        WorldData data = SaveGameScript.LoadGame();
        if (data != null)
        {
            SeedCounter = data.seeds;
            int counter = 0;
            foreach (Transform t in LocationParent.transform)
            {
                t.GetComponent<SpawnLocation>().Setup(Utilities.instance.GetToy(data.toys[counter]), Utilities.instance.GetFranimalByNameAndSpecies(data.franimals[counter]), System.DateTime.Parse(data.oldTime), data.timeRemaining[counter]);
                counter += 1;
            }
        }
        else
        {
            SeedCounter = 0;
        }
    }

    void SaveData()
    {
        List<string> toys = new List<string>();
        List<string> franimal = new List<string>();
        List<float> timeRemaining = new List<float>();
        SpawnLocation sl;
        foreach(Transform t in LocationParent.transform)
        {
            sl = t.GetComponent<SpawnLocation>();
            toys.Add(sl.GetToyName());
            franimal.Add(sl.GetFranimalInfo());
            timeRemaining.Add(sl.GetTimeRemaining());
        }
        SaveGameScript.SaveGame(franimal, toys, System.DateTime.Now.ToString(), timeRemaining, SeedCounter);
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


    #endregion

    #region Handles Location Functions
    [SerializeField] GameObject LocationParent;
    [SerializeField] LocationInformation LocationPanel;

    public void LookAtLocation(Sprite toy, Sprite franimal, string toyN, string franN, SpawnLocation location)
    {
        PauseGame();
        LocationPanel.LookAtInformation(toy, franimal, toyN, franN, location);
    }

    

    #endregion

    #region Utility Functions
    public void PauseGame()
    {
        Time.timeScale = (Time.timeScale < 1 ? 1 : 0);
    }
    #endregion
}
