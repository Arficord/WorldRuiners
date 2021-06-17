using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;

public class UnitHudManager : MonoBehaviour
{
    [SerializeField] private UnitHud hudPrefab;

    public void Initialize(List<BattleUnit> units)
    {
        foreach (var unit in units)
        {
            SpawnHud(unit);
        }
    }

    private void SpawnHud(BattleUnit unit)
    {
        Vector3 hudPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
        UnitHud hud = Instantiate(hudPrefab, hudPosition, Quaternion.identity, transform);
        hud.Initialize(unit);
    }
}
