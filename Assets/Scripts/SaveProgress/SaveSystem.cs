using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveProgress(Dictionary<int, int> progressInformation)
    {
        var formatter = new BinaryFormatter();
        var path = GetProgressFilePath();
        var stream = new FileStream(path, FileMode.OpenOrCreate);

        var playerData = new PlayerData(progressInformation);
        formatter.Serialize(stream, playerData);
        stream.Close();
        Debug.Log($"Progress was saved in {path}");
    }

    public static PlayerData LoadProgress()
    {
        var path = GetProgressFilePath();
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.OpenOrCreate);
            var playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        }

        Debug.LogError($"Save Progress File has not been found in {path}");
        return null;
    }

    private static string GetProgressFilePath() => 
        Path.Combine(Directory.GetCurrentDirectory(), "player.fun");
}
