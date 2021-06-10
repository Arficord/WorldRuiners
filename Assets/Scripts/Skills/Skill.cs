using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;

public abstract class Skill
{
    public int ManualTargetAmount { get; protected set; } = 1;
    public abstract bool IsPossibleTarget(Unit caster, Unit target);
    public abstract void Use(Unit caster, List<Unit> targets);
}
