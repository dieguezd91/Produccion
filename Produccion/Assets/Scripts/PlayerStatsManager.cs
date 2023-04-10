using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public UIManager manager;
    public float life;
    public float maxLife;
    public float magic;
    public float maxMagic;
    public float XP;
    public float maxXP;
    public int level;
    public int credits;

    private void Update()
    {
        manager.UpdateStatBars((int)life, (int)maxLife, (int)magic, (int)maxMagic, (int)XP, (int)maxXP);
        manager.UpdateCredits(credits);
        manager.UpdateLevel(level);
    }
        
    public void GetDamage(float damageTaken)
    {
        life -= damageTaken;
        if (life <= 0) Destroy(gameObject);
    }

    public void RestoreHealth(float healthRestored)
    {
        life += healthRestored;

        if (life >= maxLife) life = maxLife;
    }

    public void UseMagic(float magicUsed)
    {
        magic -= magicUsed;
        if (magic <= 0) magic = 0;
    }

    public void RestoreMagic(float magicRestored)
    {
        magic += magicRestored;
        if (magic >= maxMagic) magic = maxMagic;
    }

    public void UseCredits(int creditsUsed)
    {
        if (credits >= creditsUsed) credits -= creditsUsed;
        else Debug.Log("Not enough credits");
    }

    public void CollectCredits(int creditsTaken)
    {
        credits += creditsTaken;
    }

    public void UpdateXP(float XPGained)
    {
        XP += XPGained;
        if (XP >= maxXP)
        {
            XP = 0;
            level++;
        }
    }
}