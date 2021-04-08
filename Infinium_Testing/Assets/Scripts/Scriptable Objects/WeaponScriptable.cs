using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Light,
    Medium,
    Heavy
}
[CreateAssetMenu(fileName = "UnnamedWeaponConfig", menuName = "Infinium/WeaponConfig", order = 0)]
public class WeaponScriptable : ScriptableObject
{
    public string weaponName;
    public string weaponDescription;
    public WeaponType weaponType;
    public float damage;
    public float attackSpeed;
    //Possibly not needed
    public float weight;

    public string GetName()
    {
        return weaponName;
    }
}

