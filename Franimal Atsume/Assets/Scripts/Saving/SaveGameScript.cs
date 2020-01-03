using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class WorldData
{
    public string[] franimals;
    public string[] toys;
    public string oldTime;
    public float[] timeRemaining;
    public int seeds;
    public string[] toysOwned;
    public bool[] expansions; //index 0: land, 1: aquatic, 2: avian

    public WorldData(string[] f, string[] t, string time, float[] timeRemain, int numSeeds, string[] tO, bool[] exp)
    {
        franimals = f;
        toys = t;
        oldTime = time;
        timeRemaining = timeRemain;
        seeds = numSeeds;
        toysOwned = tO;
        expansions = exp;
    }

}

public static class SaveGameScript
{
    static string path = "/worlddata.dat";

    public static void SaveGame(List<string> f, List<string> t, string time, List<float> timeRemaining,  int numSeeds, List<string> tOwned, List<bool> exp)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string p = Application.persistentDataPath + path;
        FileStream stream = new FileStream(p, FileMode.Create);
        WorldData world = new WorldData(f.ToArray(), t.ToArray(), time, timeRemaining.ToArray(), numSeeds, tOwned.ToArray(), exp.ToArray());
        formatter.Serialize(stream, world);
        stream.Close();
    }

    public static WorldData LoadGame()
    {
        string p = Application.persistentDataPath + path;
        if (File.Exists(p))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(p, FileMode.Open);
            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteData()
    {
        string p = Application.persistentDataPath + path;
        if (File.Exists(p))
        {
            File.Delete(p);
        }
    }
}
