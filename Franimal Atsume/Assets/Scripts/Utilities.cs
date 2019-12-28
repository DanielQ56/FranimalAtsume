using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalSpecies
{
    Primate,
    Feline
}

public enum AnimalLocation
{
    Land,
    Aquatic,
    Avian  
}



public class Utilities: MonoBehaviour
{
    public List<Franimal> Primate = new List<Franimal>();
    public List<Franimal> Feline = new List<Franimal>();

    public Franimal GetAnimalWithSpecies(AnimalSpecies species)
    {
        switch(species)
        {
            case AnimalSpecies.Feline:
                return Feline[Random.Range(0, Feline.Count)];
            case AnimalSpecies.Primate:
                return Primate[Random.Range(0, Primate.Count)];
            default:
                return null;
        }
    }

}
