using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackAfterCombat : MonoBehaviour
{
    public GameObject lifeRestoredText;
    [SerializeField] TextMeshProUGUI creditsGainedText;

    public IEnumerator ShowNewCredits(string creditsGained)
    {
        creditsGainedText.text = $"+{creditsGained} NeoCoins";
        creditsGainedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        creditsGainedText.gameObject.SetActive(false);
    }
    public IEnumerator ShowLifeRestored()
    {
        lifeRestoredText.SetActive(true);
        yield return new WaitForSeconds(3f);
        lifeRestoredText.SetActive(false);
    }


}
