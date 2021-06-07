using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Level;
    public int Stars;

    public PlayerData(PlayerController player)
    {
        Level = player.level;
        Stars = player.startsAmount;
    }
}
