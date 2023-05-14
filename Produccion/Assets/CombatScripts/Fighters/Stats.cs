using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats 
{
    public float health;
    public float maxHealth;


    public int level;
    public float fuerza;
    public float destreza;
    public float defensa;
    
    public float evasion;
    public float precision;
    public float critico;

    public Stats(int level, float maxHealth, float fuerza, float destreza, float defensa,float evasion,float precision,float critico) 
    {
        this.level = level;

        this.maxHealth = maxHealth;
        this.health = maxHealth;

        this.fuerza = fuerza;
        this.destreza = destreza;
        this.defensa = defensa;
        this.evasion = evasion;
        this.precision = precision;
        this.critico = critico;


    }

    public Stats Clone() 
    {
        return new Stats(this.level, this.maxHealth, this.fuerza, this.destreza,this.defensa, this.evasion,this.precision,this.critico);
    }
}
