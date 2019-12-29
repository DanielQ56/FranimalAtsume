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
            Debug.Log(franimal.name + " " + this.gameObject.name);
            System.TimeSpan time = System.DateTime.Now - oldTime;
            //Debug.Log(time.TotalSeconds + " " + timeRemaining);
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

    void SetupNewFranimal()
    {
        franimalTimeRemaining = Random.Range(currentFranimal.MinDuration, currentFranimal.MaxDuration);
    }

    void GivePlayerSeeds(Franimal newFran = null)
    {
        if(currentFranimal != null)
            GManager.instance.AddSeedsToPlayerTotal(Random.Range(currentFranimal.MinSeeds, currentFranimal.MaxSeeds));
        currentFranimal = newFran;
        activeFranimal.sprite = (newFran != null ? newFran.sprite : null);
    }

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
}
