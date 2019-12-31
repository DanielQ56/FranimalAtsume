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

    public WorldData(string[] f, string[] t, string time, float[] timeRemain, int numSeeds)
    {
        franimals = f;
        toys = t;
        oldTime = time;
        timeRemaining = timeRemain;
        seeds = numSeeds;
    }

}

public static class SaveGameScript
{
    static string path = "/worlddata.dat";

    public static void SaveGame(List<string> f, List<string> t, string time, List<float> timeRemaining,  int numSeeds)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string p = Application.persistentDataPath + path;
        FileStream stream = new FileStream(p, FileMode.Create);
        WorldData world = new WorldData(f.ToArray(), t.ToArray(), time, timeRemaining.ToArray(), numSeeds);
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
            Debug.Log("Error");
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
