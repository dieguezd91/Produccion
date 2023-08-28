using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackAfterCombat : MonoBehaviour
{
    public GameObject lifeRestoredText;
    [SerializeField] TextMeshProUGUI creditsGainedText;
    [SerializeField] TextMeshProUGUI ammoGainedText;

    public IEnumerator ShowNewCredits(string creditsGained)
    {
        creditsGainedText.text = $"+{creditsGained} NeoCoins";
        creditsGainedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        creditsGainedText.gameObject.SetActive(false);
    }

    public IEnumerator ShowLifeRestored()
    {
        lifeRestoredText.SetActive(true);
        yield return new WaitForSeconds(2f);
        lifeRestoredText.SetActive(false);
    }

    public IEnumerator ShowAmmoRewards(string ammoGained)
    {
        ammoGainedText.text = $"{ammoGained} balas de pistola";
        ammoGainedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        ammoGainedText.gameObject.SetActive(false);
    }


}
