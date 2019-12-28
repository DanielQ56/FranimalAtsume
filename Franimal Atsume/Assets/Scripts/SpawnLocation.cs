using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public Toy currentToy;

    bool hasFranimal = false;

    Franimal currentFranimal;
    float franimalTimeRemaining = 0;


    float getNewFranimalTimer = 300f;
    float getNewFranimalProbability = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DecrementCurrentFranimal();
        CheckForNewFranimal();
    }

    void DecrementCurrentFranimal()
    {
        if (hasFranimal)
        {
            franimalTimeRemaining -= Time.deltaTime;
        }
        if(franimalTimeRemaining < 0f)
        {

        }
    }

    void CheckForNewFranimal()
    {
        if (getNewFranimalTimer <= 0)
        {
            if (Random.value < getNewFranimalProbability)
            {
                getNewFranimalTimer = 300f;
                AttractNewFranimal();
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

        }
    }
}
