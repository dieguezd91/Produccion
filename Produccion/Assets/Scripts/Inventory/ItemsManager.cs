using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;

    public enum ItemType { Item, MeleeWeapon, RangeWeapon}
    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueCoins;
    public Sprite icon;

    public int amountOfAffect;

    public enum AffectType { HP }
    public AffectType affectType;

    public int weaponDexterity;
    public int weaponStrength;

    public bool isStackable;
    public int amount;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UseItem(int characterToUseOn)
    {
        PlayerStats selectedCharacter = GameManager.instance.GetPlayerStats()[characterToUseOn]; 
        if(affectType == AffectType.HP)
        {
            selectedCharacter.AddHP(amountOfAffect);
        }
        if(itemType == ItemType.MeleeWeapon)
        {
            if (selectedCharacter.equippedMeleeWeaponName != "")
            {
                Inventory.instance.AddItems(selectedCharacter.equipedMeleeWeapon);
            }

            selectedCharacter.EquipMeleeWeapon(this);
        }
        if(itemType == ItemType.RangeWeapon)
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
