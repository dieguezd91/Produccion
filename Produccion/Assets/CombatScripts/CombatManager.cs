using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatStatus//Estados del combate
{
    WAITING_FOR_FIGHTERS,
    FIGHTER_ACTION,
    CHECK_FOR_VICTORY,
    NEXT_TURN
}
public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Fighter[] fighters;//Referencia a los luchadores en un array
    public int fighterIndex;//Un index para saber de quien es el turno

    private bool isCombatActive;

    private CombatStatus combatStatus;

    private Skill currentFighterSkill;

    private void Start()
    {
        LogPanel.Write("Battle begins.");

        foreach(var fgtr in this.fighters)//Es un bucle que asigna al combat manager a cada luchador para que puedan enviar las habilidades que se estan ejecutando
        {
            fgtr.combatManager = this; 
        }

        this.combatStatus = CombatStatus.NEXT_TURN;

        this.fighterIndex = -1;
        this.isCombatActive = true;//Cuando termine el combate, lo cambiaremos a false
        StartCoroutine(this.CombatLoop());
    }

    IEnumerator CombatLoop() 
    {
        while (this.isCombatActive)
        {
            switch (this.combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGHTERS:
                    yield return null;//Skip 1 frame
                    break;
                case CombatStatus.FIGHTER_ACTION:
                    LogPanel.Write($"{this.fighters[this.fighterIndex].idName} uses {currentFighterSkill.skillName}.");//El respectivo luchador uso la respectiva habilidad

                    yield return null;

                    currentFighterSkill.Run();//Ejecuta la habilidad del luchador

                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);//Ejecuta su animacion
                    this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;//Pasamos al estado de Check for victory

                    currentFighterSkill = null;
                    break;
                case CombatStatus.CHECK_FOR_VICTORY:
                    foreach(var fgrt in this.fighters)
                    {
                        if(fgrt.isAlive == false)//Si es falso, quiere decir que esta muerto
                        {
                            this.isCombatActive = false;//Cambiamos el combate a false y termina el combate

                            LogPanel.Write("Victory!");//Mensaje de victoria
                        }
                        else
                        {
                            this.combatStatus = CombatStatus.NEXT_TURN;//Si es verdadero, continuamos al siguiente turno
                        }
                    }
                    yield return null;
                    break;
                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);//Esperamos 1 segundos
                   
                    this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;//Sumamos uno al indice para que despues le toque al siguiente

                    var currentTurn = this.fighters[this.fighterIndex];//Vemos de quien es el turno

                    LogPanel.Write($"{currentTurn.idName} has the turn");
                    currentTurn.InitTurn();//Llamamos al inicio del turno correspondiente


                    this.combatStatus = CombatStatus.WAITING_FOR_FIGHTERS;//Cambias el estado de combate

                    break;
            }
        }
    }

    public Fighter GetOpposingFighter()//Obtenemos la referencia de a que luchador estoy enfrentando
    {
        if(this.fighterIndex == 0)
        {
            return this.fighters[1];
        }
        else
        {
            return this.fighters[0];
        }
    }

    public void OnFighterSkill(Skill skill)//El luchador elije una habilidad y cambiamos el estado a accion del luchador
    {
        this.currentFighterSkill = skill;
        this.combatStatus = CombatStatus.FIGHTER_ACTION;
    }
}
