using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class InstructionScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] string instruction;

    private void Start()
    {
        instructionText = MenuManager.instance.instruction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) StartCoroutine(EnableInstruction());
    }

    private IEnumerator EnableInstruction()
    {
        instructionText.text = instruction;
        instructionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        instructionText.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
