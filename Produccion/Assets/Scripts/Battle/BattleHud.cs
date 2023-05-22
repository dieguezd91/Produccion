using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] GameObject expBar;

    Character _character;

    public void SetData(Character character)
    {
        _character = character;
        nameText.text = character.Base.Name;
        levelText.text = "Lvl " + character.Level;
        hpBar.SetHP((float)character.HP / character.MaxHp);
        //SetExp();
    }

    //public void SetExp()
    //{
    //    if (expBar == null) return;

    //    float normalizedExp = GetNormalizedExp();
    //    expBar.transform.localScale = new Vector3(normalizedExp, 1, 1);
    //}

    //float GetNormalizedExp()
    //{
    //    int currLevelExp = _character.Base.GetExpForLevel(_character.Level);
    //    int nextLevelExp = _character.Base.GetExpForLevel(_character.Level +1);

    //    float normalizedExp = (float)(_character.Exp - currLevelExp) / (nextLevelExp - currLevelExp);
    //    return Mathf.Clamp01(normalizedExp);
    //}

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_character.HP / _character.MaxHp);
    }
}
