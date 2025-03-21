using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal,
    }
    [Header("# Main Info")]
    public int itemID;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    public ItemType itemType;
    


    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    
   
   
}
