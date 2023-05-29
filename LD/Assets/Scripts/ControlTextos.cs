using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlTextos : MonoBehaviour
{
    [SerializeField] PlantillaTextos plantilla;
    [SerializeField] PlantillaTextos[] arrayPlantillas;

    [SerializeField] TextMeshProUGUI textoNarracion;
    [SerializeField] TextMeshProUGUI textoRespuestaUno;
    [SerializeField] TextMeshProUGUI textoRespuestaDos;

    [SerializeField] GameObject[] arrayBotones;

    void Start()
    {
        plantilla = arrayPlantillas[0];
        muestraTexto();
    }

    public void muestraTexto()
    {
        textoNarracion.text = plantilla.textoNarrativo;
        textoRespuestaUno.text = plantilla.respuestaUno;
        textoRespuestaDos.text = plantilla.respuestaDos;
    }

    public void controlBotones (int indice)
    {
        plantilla = arrayPlantillas[plantilla.arrayReferencias[indice]];
        if(plantilla.quitaBotones == true)
        {
            DesactivaBotones();
        }
        muestraTexto();
    }

    public void DesactivaBotones()
    {
        foreach (var boton in arrayBotones)
        {
            boton.SetActive(false);
        }
    }
}
