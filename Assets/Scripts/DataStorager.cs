using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataStorager
{
    public bool [] donebools = new bool [9];

    public DataStorager(PlayerStats levels)
    {

        donebools [levels.currentlevel] = true;
    }
}
