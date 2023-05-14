using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;//Nombre de la habilidad
    public float animationDuration;//Cuanto dura su animacion

    public bool selfInflicted;//Si es auto infligida

    public GameObject effectPrefab;//Un prefab con la animacion que querramos instanciar

    protected Fighter emitter;//Quien emite la habilidad
    protected Fighter reciever;//Quien la recibe

    private void Animate()//Instancia la animacion  
    {
        var go = Instantiate(this.effectPrefab, this.reciever.transform.position, Quaternion.identity);
        Destroy(go, this.animationDuration);
    }

    public void Run()//Ejecutamos la habilidad
    {
        if (this.selfInflicted)
        {
            this.reciever = this.emitter;
        }

        this.Animate();//Ejecutamos la animacion

        this.OnRun();//Ejecutamos el metodo onRun
    }

    public void SetEmitterAndReciever(Fighter _emitter, Fighter _reciever)
    {
        this.emitter = _emitter;
        this.reciever = _reciever;
    }

    protected abstract void OnRun();//Se implementara en la categoria de implementacion de salud y en la de estadisticas
}
