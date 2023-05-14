using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Fighter
{
    private void Awake()
    {
        this.stats = new Stats(21, 60, 50, 45, 20,30,30,30);
    }
    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = this.skills[Random.Range(0, this.skills.Length)];

        skill.SetEmitterAndReciever(this, this.combatManager.GetOpposingFighter());

        this.combatManager.OnFighterSkill(skill);
    }
}
