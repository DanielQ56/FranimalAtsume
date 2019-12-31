using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<Toy> ownedToys;
    int seedsOwned = 0;

    public void SetSeeds(int seeds)
    {
        seedsOwned = seeds;
    }

    public int GetSeeds()
    {
        return seedsOwned;
    }

    public void AddToy(Toy t)
    {
        ownedToys.Add(t);
    }

    public List<Toy> GetToy(Toy t)
    {
        return ownedToys;
    }
}
