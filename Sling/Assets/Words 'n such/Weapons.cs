using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected string weaponName;
    protected int damage;
    protected int ammo;

    enum WeaponType
    {
        sword,
        throwable,
        grapple,
        melee
    }

    public string Name
    {
        get { return weaponName; }
        set { weaponName = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }
    }

    public Weapons()
    {
        weaponName = "Glock";
        damage = 50;
        ammo = 14;
    }

    public Weapons(string name, int damage, int ammo)
    {
        this.weaponName = name;
        this.damage = damage;
        this.ammo = ammo;
    }

    public string toString()
    {
        return weaponName + "/" + damage + "/" + ammo;
    }
}
