using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Franimal", menuName = "Franimal")]
public class Franimal : ScriptableObject
{
    public string franimalName;
    public Sprite sprite;
    public AnimalSpecies species;
    public int MinSeeds;
    public int MaxSeeds;
    public int PowerLevel;
    public int MinDuration;
    public int MaxDuration;
    public Toy FavoriteToy = null;
}
