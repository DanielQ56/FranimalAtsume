using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Franimal", menuName = "Franimal")]
public class Franimal : ScriptableObject
{
    public AnimalSpecies species;
    public AnimalLocation location;
    public int MinSeeds;
    public int MaxSeeds;
    public int PowerLevel;
    public int MinDuration;
    public int MaxDuration;
    public Toy FavoriteToy;
}
