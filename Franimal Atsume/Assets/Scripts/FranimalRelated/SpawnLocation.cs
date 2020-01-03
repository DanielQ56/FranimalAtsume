using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnLocation : MonoBehaviour
{
    [SerializeField] AnimalLocation location;

    #region Franimal Variables
    [SerializeField] SpriteRenderer activeFranimal;
    public Franimal currentFranimal;
    float franimalTimeRemaining = 0;
    [SerializeField] float getNewFranimalTimer = 0f;
    [SerializeField] float getNewFranimalProbability = 0.1f;
    [SerializeField] float franimalSpawnInterval = 10f;
    #endregion

    #region Startup and Pre-startup functions
    bool paused = false;

    //called when loading up a save file 
    public void Setup(Toy toy, Franimal franimal, System.DateTime oldTime, float timeRemaining)
    {
        currentToy = toy;
        currentFranimal = franimal;
        if(currentToy != null)
        {
            activeToy.sprite = currentToy.toySprite;
            currentToy.hasBeenPlaced = true;
        }
        if(currentFranimal != null)
        {
            System.TimeSpan time = System.DateTime.Now - oldTime;
            if(time.TotalSeconds < timeRemaining)
            {
                activeFranimal.sprite = currentFranimal.sprite;
                franimalTimeRemaining = timeRemaining - (float)time.TotalSeconds;
            }
            else
            {
                GivePlayerSeeds();
            }
        }
    }

    private void Start()
    {
        if(currentToy != null)
        {
            activeToy.sprite = currentToy.toySprite;
        }
        getNewFranimalTimer = franimalSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        DecrementCurrentFranimal();
        CheckForNewFranimal();
    }

    public void PauseTimers()
    {
        paused = !paused;
    }


    #endregion


    #region Handles Clicking on a Location

    private void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (GManager.instance.HasUnlockedExpansion(location))
                GManager.instance.LookAtLocation((currentToy == null ? null : currentToy.toySprite), (currentFranimal == null ? null : currentFranimal.sprite),
                (currentToy == null ? "None" : currentToy.name), (currentFranimal == null ? "None" : currentFranimal.name), this);
            else
                GManager.instance.HasNotUnlockedExpansion(location);
        }
    }
    #endregion

    #region Toy-Specific Functions/Variables
    [SerializeField] Toy currentToy;
    [SerializeField] SpriteRenderer activeToy;
    public void ChangeToy(Toy t)
    {
        ChangeToyInfo(t); 
        GivePlayerSeeds();
    }

    void ChangeToyInfo(Toy t)
    {
        if(currentToy != null)
            currentToy.hasBeenPlaced = false;
        currentToy = t;
        activeToy.sprite = (currentToy == null ? null : currentToy.toySprite);
        if (currentToy != null)
        {
            currentToy.hasBeenPlaced = true;
        }
    }
    #endregion

    #region Handles franimal arrival and departure
    //decrement the timer/make franimal leave when its timer is done
    void DecrementCurrentFranimal()
    {
        if (currentFranimal != null && !paused)
        {
            franimalTimeRemaining -= Time.deltaTime;

            if (franimalTimeRemaining < 0f && currentFranimal != null)
            {
                activeFranimal.sprite = null;
                GivePlayerSeeds();
            }
        }
    }

    //uses probability to decide whether or not to spawn a new franimal at this SpawnLocation
    void CheckForNewFranimal()
    {
        if (currentToy != null && !paused)
        {
            if (getNewFranimalTimer <= 0)
            {
                if (Random.value < getNewFranimalProbability)
                {
                    AttractNewFranimal();
                }
                getNewFranimalTimer = franimalSpawnInterval;

            }
            else
            {
                getNewFranimalTimer -= Time.deltaTime;
            }
        }
    }

    //Gets a new franimal to inhabit this location
    void AttractNewFranimal()
    {
        if(currentToy != null)
        {
            Franimal newFran = Utilities.instance.GetAnimalWithSpecies(currentToy);
            if (currentFranimal == null)
            {
                GivePlayerSeeds(newFran);
                SetupNewFranimal();
            }
            else
            {
                if (newFran.PowerLevel > currentFranimal.PowerLevel)
                {
                    GivePlayerSeeds(newFran);
                    SetupNewFranimal();
                }
            }
            
        }
    }

    //fetching the time this franimal should stay at this location
    void SetupNewFranimal()
    {
        franimalTimeRemaining = Random.Range(currentFranimal.MinDuration, currentFranimal.MaxDuration);
    }


    //gives player seeds once the franimal leaves
    void GivePlayerSeeds(Franimal newFran = null)
    {
        if(currentFranimal != null)
            GManager.instance.AddSeedsToPlayerTotal(Random.Range(currentFranimal.MinSeeds, currentFranimal.MaxSeeds));
        currentFranimal = newFran;
        activeFranimal.sprite = (newFran != null ? newFran.sprite : null);
    }

    #endregion

    #region accessory function for getting info from this Spawn location
    public string GetToyName()
    {
        return (currentToy == null ? "" : currentToy.name);
    }

    public string GetFranimalInfo()
    {
        if (currentFranimal != null)
            return string.Format("{0},{1}", currentFranimal.name, currentFranimal.species);
        else
            return "";
    }

    public float GetTimeRemaining()
    {
        return franimalTimeRemaining;
    }

    #endregion
}
