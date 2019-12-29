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

    [SerializeField] TextMeshProUGUI seedText;
    [SerializeField] GameObject LocationParent;

    int seedCounter;
    int SeedCounter
    {
        get { return seedCounter; }
        set {
            seedCounter = value;
            seedText.text = "Seeds: " + seedCounter.ToString();
        }
    }

    #region Testing

    public GManager()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }


    private void LogPlayModeState(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.ExitingPlayMode)
        {
            Debug.Log("Saving");
            SaveData();
        }
    }
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
    }

    private void Start()
    {
        WorldData data = SaveGameScript.LoadGame();
        if(data != null)
        {
            SeedCounter = data.seeds;
            int counter = 0;
            foreach(Transform t in LocationParent.transform)
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

    private void OnApplicationQuit()
    {
        
    }

    public void AddSeedsToPlayerTotal(int seeds)
    {
        Debug.Log(seeds);
        SeedCounter += seeds;
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
        Debug.Log("Heyo");
    }
}
