using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP, currentHP;

    public int level;
    public int exp, exp_Max;

    public int fuerza, destreza, defensa;

    public int puntos;

    public Text HP_texto, level_texto, exp_texto;
    public Text fuerza_texto, destreza_texto, defensa_texto;

    public Text puntos_texto;

    private void Update()
    {
        SubirExp();
        Texto_UI();
    }

    void SubirExp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            exp += 10;
        }

        if (exp >= exp_Max)
        {
            level++;
            exp_Max = Mathf.RoundToInt(exp_Max * 1.3f);
            puntos += 5;
        }
    }

    public void SubirFuerza()
    {
        if (puntos > 0)
        {
            fuerza++;
            puntos--;
        }
    }

    public void SubirDestreza()
    {
        if (puntos > 0)
        {
            destreza++;
            puntos--;
        }
    }

    public void SubirDefensa()
    {
        if (puntos > 0)
        {
            defensa++;
            puntos--;
        }
    }

    public void Texto_UI()
    {
        HP_texto.text = "" + currentHP + "/" + maxHP;
        level_texto.text = "" + level;
        exp_texto.text = "" + exp + "/" + exp_Max;

        fuerza_texto.text = "" + fuerza;
        destreza_texto.text = "" + destreza;
        defensa_texto.text = "" + defensa;

        puntos_texto.text = "" + puntos;
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
