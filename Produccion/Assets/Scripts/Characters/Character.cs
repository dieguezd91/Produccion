using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    [SerializeField] public CharacterBase Base { get; set; }
    [SerializeField] int level;

    public int Exp { get; set; }
    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Character(CharacterBase pBase, int pLevel)
    {
        Base = pBase;
        level = pLevel;

        Init();
    }

    public int Level
    {
        get { return level; }
    }

    public int Strenght
    {
        get { return Mathf.FloorToInt((Base.Strenght * Level) / 100f) + 5; }
    }

    public int Dexterity
    {
        get { return Mathf.FloorToInt((Base.Dexterity * Level) / 100f) + 5; }
    }

    public int Defence
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10;}
    }

    public void Init()
    {
        Moves = new List<Move>();
        foreach (var move in Base.Attacks)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }

        //Exp = Base.GetExpForLevel(Level);

        HP = MaxHp;


    }

    public bool TakeDamage(Move move, Character attacker)
    {
        /*float critical = 1f;
        if (Random.value * 100f <= 6.25f)
            critical = 2f;

        float modifiers = Random.Range(0.85f, 1f) * critical;*/

        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Fuerza * ((float)attacker.Strenght / Defence) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);
        Debug.Log(damage);

        HP -= damage;
        if(HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}
