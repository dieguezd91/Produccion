using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : Fighter
{

    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    private void Awake()
    {
        this.stats = new Stats(21, 60, 50, 45, 20,30,30,30);
    }
    public override void InitTurn()
    {
        this.skillPanel.Show();

        for(int i = 0; i < this.skills.Length; i++)
        {
            this.skillPanel.ConfigureButtons(i, this.skills[i].skillName);
        }
    }

    public void ExecuteSkill(int index)
    {
        this.skillPanel.Hide();

        Skill skill = this.skills[index];//Usamos la habilidad

        skill.SetEmitterAndReciever(this, this.combatManager.GetOpposingFighter());//Asignamos al emisor y receptor

        this.combatManager.OnFighterSkill(skill);//Y mandamos la habilidad al combat manager
    }
}
