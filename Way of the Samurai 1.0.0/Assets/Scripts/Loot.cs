using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public static lootTypes _lootType { get; set; }


    public lootTypes GetLootType()
    {
        return _lootType;
    }

    public enum lootTypes { Coin, Weapon }
}
