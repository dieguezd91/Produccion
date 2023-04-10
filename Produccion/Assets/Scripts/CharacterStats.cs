using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterStats : MonoBehaviour
{
    public int HP;
    public int HP_Max;
    public int Nivel;
    public int Exp;
    public int Exp_Max;

    public int Vitalidad = 50;
    public int Fuerza = 15;
    public int Agilidad = 10;
    public int Magia = 5;
    public int Defensa = 15;

    public int Puntos;
    
    public Text HP_texto;
    public Text Nivel_texto;
    public Text Exp_texto;

    public Text Vitalidad_texto;
    public Text Fuerza_texto;
    public Text Agilidad_texto;
    public Text Magia_texto;
    public Text Defensa_texto;

    public Text Puntos_texto;


    private void Update()
    {
        SubirExp();
        Texto_UI();
    }

    void SubirExp()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Exp += 10;
        }

        if(Exp >= Exp_Max)
        {
            Nivel++;
            Exp_Max = Mathf.RoundToInt(Exp_Max * 1.3f);
            Puntos += 5;
        }
    }

    public void SubirVitalidad()
    {
        if(Puntos > 0)
        {
            Vitalidad++;
            Puntos--;
        }
    }

    public void SubirFuerza()
    {
        if (Puntos > 0)
        {
            Fuerza++;
            Puntos--;
        }
    }

    public void SubirAgilidad()
    {
        if (Puntos > 0)
        {
            Agilidad++;
            Puntos--;
        }
    }

    public void SubirMagia()
    {
        if (Puntos > 0)
        {
            Magia++;
            Puntos--;
        }
    }

    public void SubirDefensa()
    {
        if (Puntos > 0)
        {
            Defensa++;
            Puntos--;
        }
    }    

    public void Texto_UI()
    {
        HP_texto.text = "" + HP + "/" + HP_Max;
        Nivel_texto.text = "" + Nivel;
        Exp_texto.text = "" + Exp + "/" + Exp_Max;

        Vitalidad_texto.text = "" + Vitalidad;
        Fuerza_texto.text = "" + Fuerza;
        Agilidad_texto.text = "" + Agilidad;
        Magia_texto.text = "" + Magia;
        Defensa_texto.text = "" + Defensa;

        Puntos_texto.text = "" + Puntos;
    }

}
