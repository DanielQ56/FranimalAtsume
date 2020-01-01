using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParent : MonoBehaviour
{
    [SerializeField] List<SpawnLocation> locations;
    
    public void PauseSpawning()
    {
        foreach(SpawnLocation s in locations)
        {
            s.PauseTimers();
        }
    }

}
