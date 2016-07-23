using UnityEngine;

[System.Serializable] //Means unity will know how to save and load this class.
//wont show up in inspector without ^^
public class PlayerWeapon {

    public string name = "Glock";

    public int damage = 10;
    public float range = 100f;


}
