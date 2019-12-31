using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [SerializeField] Toy currentToy;
    [SerializeField] SpriteRenderer activeToy;
    [SerializeField] SpriteRenderer activeFranimal;
    [SerializeField] AnimalLocation location;


    public Franimal currentFranimal;
    float franimalTimeRemaining = 0;


    float getNewFranimalTimer = 0f;
    float getNewFranimalProbability = 0.50f;

    //called when loading up a save file 
    public void Setup(Toy toy, Franimal franimal, System.DateTime oldTime, float timeRemaining)
    {
        currentToy = toy;
        currentFranimal = franimal;
        if(currentToy != null)
        {
            activeToy.sprite = currentToy.toySprite;
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
    }

    // Update is called once per frame
    void Update()
    {
        DecrementCurrentFranimal();
        CheckForNewFranimal();
    }

    private void OnMouseDown()
    {
        GManager.instance.LookAtLocation((currentToy == null ? null : currentToy.toySprite), (currentFranimal == null ? null : currentFranimal.sprite),
            (currentToy == null ? "None" : currentToy.name), (currentFranimal == null ? "None" : currentFranimal.name), this);
    }

    public void ChangeToy(Toy t)
    {
        ChangeToyInfo(t); 
        GivePlayerSeeds();
    }

    #region Handles franimal arrival and departure
    //decrement the timer/make franimal leave when its timer is done
    void DecrementCurrentFranimal()
    {
        if (currentFranimal != null)
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
        if (currentToy != null)
        {
            if (getNewFranimalTimer <= 0)
            {
                if (Random.value < getNewFranimalProbability)
                {
                    AttractNewFranimal();
                }
                getNewFranimalTimer = 5f;

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

    void ChangeToyInfo(Toy t)
    {
        currentToy = t;
        currentToy.toySprite = (currentToy == null ? null : currentToy.toySprite);
    }

    #endregion

    #region accessory function for getting info from this Spawn location
    public string GetToyName()
    {
        return currentToy.name;
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
