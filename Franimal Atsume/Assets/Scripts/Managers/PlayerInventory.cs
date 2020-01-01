using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    List<Toy> ownedToys;
    List<string> toyNames;
    int seedsOwned;

    public PlayerInventory()
    {
        ownedToys = new List<Toy>();
        toyNames = new List<string>();
    }

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
        toyNames.Add(t.toy);
    }

    public void SetInventory(List<Toy> t)
    {
        ownedToys.AddRange(t);
    }

    public Toy GetToy(string name)
    {
        foreach(Toy t in ownedToys)
        {
            if(t.toy == name)
            {
                return t;
            }
        }
        return null;
    }

    public List<Toy> GetOwnedToys()
    {
        return ownedToys;
    }

    public List<string> GetNamesOfOwnedToys()
    {
        return toyNames;
    }

    public bool HasSufficientFunds(int cost)
    {
        return seedsOwned >= cost;
    }
}
