using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Character);
        enemyHud.SetData(enemyUnit.Character);

        dialogBox.SetMoveNames(playerUnit.Character.Moves);

        yield return dialogBox.TypeDialog("Aca va el dialogo");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Elegi una accion"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Character.Moves[currentMove];
        move.PP--;
        yield return dialogBox.TypeDialog($"{ playerUnit.Character.Base.Name} uso {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.Character.TakeDamage(move, playerUnit.Character);
        yield return enemyHud.UpdateHP();
        
        if(isDead)
        {
            yield return dialogBox.TypeDialog($"{ enemyUnit.Character.Base.Name} es derrotado");

            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Character.GetRandomMove();
        move.PP--;
        yield return dialogBox.TypeDialog($"{ enemyUnit.Character.Base.Name} uso {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.Character.TakeDamage(move, playerUnit.Character);
        yield return playerHud.UpdateHP();

        if (isDead)
        {
            yield return dialogBox.TypeDialog($"{ playerUnit.Character.Base.Name} es derrotado");
            
            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator HandleCharacterDefeated(BattleUnit defeatedUnit)
    {
        yield return dialogBox.TypeDialog($"{ defeatedUnit.Character.Base.Name} es derrotado");

        yield return new WaitForSeconds(2f);

        //if(!defeatedUnit.IsPlayerUnit)
        //{
        //    //Exp Gain
        //    int expYield = defeatedUnit.Character.Base.ExpYield;
        //    int enemyLevel = defeatedUnit.Character.Level;
        //    int expGain = Mathf.FloorToInt((expYield * enemyLevel) / 7);
        //    playerUnit.Character.Exp += expGain;
        //    yield return dialogBox.TypeDialog($"{playerUnit.Character.Base.Name} gano {expGain} exp");
        //}


    }

    public void HandleUpdate()
    {
        if(state==BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if(state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(currentAction == 0)
            {
                //fight
                PlayerMove();
            }
            else if(currentAction ==1)
            {
                //run
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Character.Moves.Count - 1)
                ++currentMove ;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Character.Moves.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Character.Moves[currentMove]);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
