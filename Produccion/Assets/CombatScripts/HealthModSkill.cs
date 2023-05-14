using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthModType
{
    ATAQUE_MELEE,ATAQUE_RANGO,FIXED,PERCENTAGE//Basadas en Stats(ATAQUE_MELEE/ATAQUE_RANGO), Daño o restauracion fija (FIXED), en porcentaje(PERCENTAGE)
}
public class HealthModSkill : Skill//Representa la categoria de habilidades que modifican la salud
{
    [Header("Health Mod")]
    public float amount;//El numero que modificara los valores de cada categoria

    public HealthModType modType;
    Stats emitterStats;
    Stats recieverStats;
    float rawDamage;
    bool canHit;

    protected override void OnRun()
    {
        float amount = this.GetModification();//El valor a modificar

        this.reciever.ModifyHealth(amount);//Y modificamos la salud del receptor
    }
    private bool CalculateIfCanHit(Stats emitterStats, Stats recieverStats)
    {
        float hitChance = 1f - Mathf.Max(recieverStats.evasion - emitterStats.precision, 0) / (float)recieverStats.evasion;

        float dice = Random.Range(0f, 1f);
        return dice < hitChance;
    } 
    public float GetModification()
    {
        switch (this.modType)
        {
            case HealthModType.ATAQUE_MELEE://Si la habilidad esta basada en stats
                emitterStats = this.emitter.GetCurrentStats();
                recieverStats = this.reciever.GetCurrentStats();

               
                //Formula: http://bulbapedia.bulbagarden.net/wiki/Damage formula basada en pokemon
                rawDamage = (((2 * emitterStats.level) / 5) + 2) * this.amount * (emitterStats.fuerza / recieverStats.defensa);//Calculamos el daño que va a recibir el receptor

                return (rawDamage / 50) + 2;

                
              
            case HealthModType.ATAQUE_RANGO:
                emitterStats = this.emitter.GetCurrentStats();
                recieverStats = this.reciever.GetCurrentStats();
                //Formula: http://bulbapedia.bulbagarden.net/wiki/Damage formula basada en pokemon
                rawDamage = (((2 * emitterStats.level) / 5) + 2) * this.amount * (emitterStats.destreza / recieverStats.defensa);//Calculamos el daño que va a recibir el receptor

                return (rawDamage / 50) + 2;
            case HealthModType.FIXED://Para la modificacion de valor fijo solo retornamos el valor
                return this.amount;
            case HealthModType.PERCENTAGE://Agarramos la vida maxima de quien recibe y la multiplicamos por este valor 
                Stats rStats = this.reciever.GetCurrentStats();

                return rStats.maxHealth * this.amount;
        }

        throw new System.InvalidOperationException("HealthModSkill::GetDamage. Unreacheable!");//Esto es una excepcion por si el programa no encuentra un valor a devolver
    }
}
