using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [SerializeField] string[] questNames;
    [SerializeField] bool[] questCompleted;

    public static QuestManager instance;

    //public event EventHandler OnQuestMarked;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        instance = this;
        questCompleted = new bool[questNames.Length];    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            MarkQuestComplete("Vencer a Dinnie");
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogWarning("Quest: " + questToFind + " does not exist");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);

        Debug.Log(questNumberToCheck);
        return questCompleted[questNumberToCheck];
    }

    //public void UpdateQuestObjects()
    //{
    //    QuestObject[] questObjects = FindObjectsOfType<QuestObject>();

    //    if(questObjects.Length > 0)
    //    {
    //        foreach(QuestObject questObject in questObjects)
    //        {
    //            questObject.CheckForCompletion();
    //        }
    //    }
    //}

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questCompleted[questNumberToCheck] = true;

        //OnQuestMarked?.Invoke(this, EventArgs.Empty);
        //UpdateQuestObjects();
    }
}
