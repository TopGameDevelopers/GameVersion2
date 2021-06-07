using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveProgress(PlayerController player)
    {
        var formatter = new BinaryFormatter();
        var path = "./player.fun";
        var stream = new FileStream(path, FileMode.OpenOrCreate);

        var playerData = new PlayerData(player);
        formatter.Serialize(stream, playerData);
        stream.Close();
        Debug.Log($"Progress was saved in {path}");
    }

    public static PlayerData LoadProgress()
    {
        var path = "./player.fun";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        }

        Debug.LogError($"Save Progress File has not been found in {path}");
        return null;
    }
}
