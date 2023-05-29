using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType { Item, Weapon}
    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueCoins;
    public Sprite itemsImage;

    public int amountOfAffect;

    public enum AffectType { HP }
    public AffectType affectType;

    public int weaponDexterity;
    public int weaponStrength;

    public bool isStackable;
    public int amount;

    public void UseItem()
    {
        if(affectType == AffectType.HP)
        {
            PlayerStats.instance.AddHP(amountOfAffect);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inventory.instance.AddItems(this);
            SelfDestroy();
            Debug.Log("colision");
        }
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
