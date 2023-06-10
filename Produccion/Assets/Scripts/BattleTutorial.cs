using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTutorial : MonoBehaviour
{
    public void StartBattleTutorial()
    {
        BattleManager.instance.StartBattle(null, "Dinnie");
    }
}
