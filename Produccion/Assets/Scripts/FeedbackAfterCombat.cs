using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackAfterCombat : MonoBehaviour
{
    [SerializeField] GameObject lifeRestoredText;
    [SerializeField] TextMeshProUGUI creditsGainedText;

    public IEnumerator ShowNewCredits(string creditsGained)
    {
        Debug.Log("Show new credits");
        creditsGainedText.text = $"+{creditsGained} creditos";
        creditsGainedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        creditsGainedText.gameObject.SetActive(false);
        Debug.Log("New credits shown");
    }
    public IEnumerator ShowLifeRestored()
    {
        Debug.Log("Show life restored");
        lifeRestoredText.SetActive(true);
        yield return new WaitForSeconds(5f);
        lifeRestoredText.SetActive(false);
        Debug.Log("Life restored shown");
    }


}
