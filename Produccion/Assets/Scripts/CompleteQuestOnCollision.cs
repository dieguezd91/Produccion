using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuestOnCollision : MonoBehaviour
{
    QuestObject questObject;
    void Start()
    {
        questObject = gameObject.GetComponentInParent<QuestObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            QuestManager.instance.MarkQuestComplete(questObject.questToComplete);
            gameObject.SetActive(false);
        }
    }
}
