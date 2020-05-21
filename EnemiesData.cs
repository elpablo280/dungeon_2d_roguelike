﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesData : MonoBehaviour
{
    public Enemies[] enemies;
}

[System.Serializable]
public class Enemies
{
    public string name;
    public Sprite sprite;
    public int spawnChance;
    public int hp;
    public int baseAttack;
    public int diceDamage;
    public int sidesDamage;
    public int bonusDamage;
    public int ac;
}