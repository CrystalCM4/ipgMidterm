using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade")]
public class Upgrades : ScriptableObject
{
    public string upgradeName;
    public double damageAdd;
    public double damageMult;
    public int healthAdd;
    public string specificCondition;
    public string description;
    public bool chosen;

}
