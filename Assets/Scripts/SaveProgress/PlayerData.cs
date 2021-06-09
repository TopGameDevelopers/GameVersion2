using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public HashSet<int> LevelsCompleted;
    //public int Level;
    public int Stars;

    public PlayerData(PlayerController player)
    {
        LevelsCompleted = player.levelsCompleted;
        Stars = player.startsAmount;
    }
}
