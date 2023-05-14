using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public string idName;
    public StatusPanel statusPanel;//Su respectivo panel de estado

    public CombatManager combatManager;//Referencia al gestor de combate

    protected Stats stats;//Sus estadisticas

    protected Skill[] skills;//Habilidades como una array de skills

    public bool isAlive//un getter para saber si el luchador esta vivo o muerto
    {
        get => this.stats.health > 0;
    }

    protected virtual void Start() 
    {
        this.statusPanel.SetStats(this.idName, this.stats);//Colocamos las estadisticas en el panel
        this.skills = this.GetComponentsInChildren<Skill>();//Los obtenemos de los componetes de los objetos hijos
    }

    public void ModifyHealth(float amount) //Una funcion que modifica la vida
    {
        this.stats.health = Mathf.Clamp(this.stats.health + amount, 0f, this.stats.maxHealth);//Suma o resta la cantidad que proviene, si es negativo es daño recibido, si es positivo es cura, entre 0 y vida maxima

        this.stats.health = Mathf.Round(this.stats.health);//Redondeamos la vida luego de una modificacion
        this.statusPanel.SetHealth(this.stats.health, this.stats.maxHealth);//Despues de modificar la salud la agregamos al panel de estado
    }

    public Stats GetCurrentStats()//Devolvemos los stats
    {
        //TODO: Stat Modification
        return this.stats;
    }

    public abstract void InitTurn();
}
