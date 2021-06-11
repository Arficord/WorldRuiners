using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;

public class SkillCastingUI : MonoBehaviour
{
    public event Action OnSkillCasted;
    private List<Unit> selectedForSkillUnits;
    private Skill skillToUse;
    private Unit skillCaster;
    
    public void Initialize(Unit caster, Skill skill)
    {
        selectedForSkillUnits = new List<Unit>();
        skillCaster = caster;
        skillToUse = skill;
    }

    public void MarkValidTargets(List<BattleUnit> units)
    {
        foreach (var unit in units)
        {
            unit.ShowTargetMark(skillToUse.IsPossibleTarget(skillCaster, unit.UnitModel));
        }
    }

    public void ClearTargetMarks(List<BattleUnit> units)
    {
        foreach (var unit in units)
        {
            unit.ShowTargetMark(false);
        }
    }
    
    public bool IsUnitSelectedForSkill(Unit unit)
    {
        return selectedForSkillUnits.Contains(unit);
    }

    public void ToggleUnitForSkill(Unit unit)
    {
        if (!skillToUse.IsPossibleTarget(skillCaster, unit))
        {
            Debug.Log("Tried to select invalid target for skill");
            return;
        }
        
        if (selectedForSkillUnits.Contains(unit))
        {
            selectedForSkillUnits.Remove(unit);
        }
        else
        {
            if (skillToUse.ManualTargetAmount <= selectedForSkillUnits.Count)
            {
                Debug.LogWarning("Tried to add unit to target list. But it is already full!");
                return;
            }
            selectedForSkillUnits.Add(unit);
            UseSkill();
        }
    }

    private void UseSkill()
    {
        skillToUse.Use(skillCaster, selectedForSkillUnits);
        OnSkillCasted?.Invoke();
    }
}
