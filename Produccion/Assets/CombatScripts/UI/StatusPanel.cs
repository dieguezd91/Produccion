using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    public Text nameLabel;//Nombre
    public Text levelLabel;//Nivel

    public Slider healthSlider;
    public Image healthSliderBar;
    public Text healthLabel;

    public void  SetStats(string name, Stats stats) //Set de nombre y estadisticas
    {
        this.nameLabel.text = name;//Seteamos nombre

        this.levelLabel.text = $"N.{stats.level}";//Seteamos nivel
        this.SetHealth(stats.health, stats.maxHealth);//Seteamos vida con una funcion
    }

    public void SetHealth(float health, float maxHealth) 
    {
        this.healthLabel.text = $"{Mathf.RoundToInt(health)}/{Mathf.RoundToInt(maxHealth)}";//Etiqueta de texto con la salud y la salud maxima. Pasamos los valores a enteros para el texto
        float percentage = health / maxHealth;//Calculamos un porcentaje entre la vida maxima y la actual

        this.healthSlider.value = percentage;//Se la colocamos al slider

        if (percentage < 0.33f)//Si es menor que el 33%
        {
            this.healthSliderBar.color = Color.red;//Cambiamos el estado de la barra a roja
        }
    }

}
