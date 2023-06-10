using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtonsOnTutorial : MonoBehaviour
{
    public GameObject runButton;
    public GameObject itemsButton;
    public GameObject rangeButton;

    void Update()
    {
        runButton.SetActive(!GameManager.instance.tutorial);
        itemsButton.SetActive(!GameManager.instance.tutorial);
        rangeButton.SetActive(!GameManager.instance.tutorial);
    }
}
