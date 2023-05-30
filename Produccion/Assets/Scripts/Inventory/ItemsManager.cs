using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType { Item, MeleeWeapon, RangeWeapon}
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

    public void UseItem(int characterToUseOn)
    {

        PlayerStats selectedCharacter = GameManager.instance.GetPlayerStats()[characterToUseOn]; 
        if(affectType == AffectType.HP)
        {
            selectedCharacter.AddHP(amountOfAffect);
        }
        else if(itemType == ItemType.MeleeWeapon)
        {
            if(selectedCharacter.equippedMeleeWeaponName != "")
            {
                Inventory.instance.AddItems(selectedCharacter.equipedMeleeWeapon);
            }

            selectedCharacter.EquipMeleeWeapon(this);
        }
        else if(itemType == ItemType.RangeWeapon)
        {
            if(selectedCharacter.equippedRangeWeaponName != "")
            {
                Inventory.instance.AddItems(selectedCharacter.equipedRangeWeapon);
            }

            selectedCharacter.EquipRangeWeapon(this);
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
