using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalSpecies
{
    Primate,
    Feline,
    None
}

public enum AnimalLocation
{
    Land,
    Aquatic,
    Avian,
    None
}



public class Utilities: MonoBehaviour
{
    public static Utilities instance = null;

    public List<Franimal> Primate = new List<Franimal>();
    public List<Franimal> Feline = new List<Franimal>();
    public List<Toy> toys = new List<Toy>();
    public List<Toy> NewExpansions = new List<Toy>();

    private void Awake()
    {
        instance = this;
    }

    public Franimal GetAnimalWithSpecies(Toy toy)
    {
        Franimal newFran;
        switch (toy.attractedSpecies)
        {
            case AnimalSpecies.Feline:
                do
                {
                    newFran = Feline[Random.Range(0, Feline.Count)];
                } while (newFran.FavoriteToy != null || newFran.FavoriteToy == toy);
                return newFran;
            case AnimalSpecies.Primate:
                do
                {
                    newFran = Primate[Random.Range(0, Primate.Count)];
                } while (newFran.FavoriteToy != null || newFran.FavoriteToy == toy);
                return newFran;
            default:
                return null;
        }
    }

    public Franimal GetFranimalByNameAndSpecies(string fullinfo)
    {
        string name;
        switch(GetSpeciesFromString(fullinfo, out name))
        {
            case AnimalSpecies.Feline:
                foreach(Franimal f in Feline)
                {
                    if (f.name == name)
                        return f;
                }
                return null;
            case AnimalSpecies.Primate:
                foreach (Franimal f in Primate)
                {
                    if (f.name == name)
                        return f;
                }
                return null;
            default:
                return null;
        }
    }

    AnimalSpecies GetSpeciesFromString(string species, out string name)
    {
        if(string.IsNullOrEmpty(species))
        {
            name = "";
            return AnimalSpecies.None;
        }
        else
        {
            string[] info = species.Split(',');
            System.Enum.TryParse<AnimalSpecies>(info[1], out AnimalSpecies s);
            name = info[0];
            return s;
        }
    }

    public List<Toy> SeparateToys(out List<Toy> unowned, string[] names = null)
    {
        List<Toy> owned = new List<Toy>();
        if (names != null)
        {
            List<string> toyNames = new List<string>();
            toyNames.AddRange(names);
            foreach (Toy t in toys)
            {
                if (toyNames.Contains(t.toy))
                {
                    owned.Add(t);
                    t.isOwned = true;
                }
            }
        }
        unowned = toys;
        return owned;
    }

    public Toy GetToy(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            foreach (Toy t in toys)
            {
                if (t.toy == name)
                {
                    return t;
                }
            }
        }
        Debug.Log("Toy does not exist");
        return null;
    }

}
