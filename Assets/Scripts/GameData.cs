using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{

    public int totalCoins;
    public int highscore;

    public bool sound;
    public bool music;


    public GameData()
        {
        totalCoins = 0;
        highscore = 0;
        sound = true;
        music = true;
        }
}
