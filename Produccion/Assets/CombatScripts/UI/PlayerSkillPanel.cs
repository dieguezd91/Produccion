using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject[] skillButtons;//Lista de botones
    public Text[] skillButtonLabels;//Lista de etiquetas de botones

    private void Awake()//Ocultamos los botones al inicio de la pelea
    {
        this.Hide();

        foreach(var button in this.skillButtons)
        {
            button.SetActive(false);
        }
    }

    public void ConfigureButtons(int index, string skillName)//Activamos individualmente los botones y asignamos sus etiquetas
    {
        this.skillButtons[index].SetActive(true);
        this.skillButtonLabels[index].text = skillName;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
