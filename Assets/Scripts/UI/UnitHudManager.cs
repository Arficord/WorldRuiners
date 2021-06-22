using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;

namespace My.UI.InfoBlocks
{
    public class UnitHudManager : MonoBehaviour
    {
        [SerializeField] private UnitHud hudPrefab;

        public void Initialize(List<BattleUnit> units)
        {
            Camera camera = Camera.main;
            foreach (var unit in units)
            {
                SpawnHud(unit, camera);
            }
        }

        private void SpawnHud(BattleUnit unit, Camera camera)
        {
            Vector3 hudPosition = camera.WorldToScreenPoint(unit.GetBottomMarkPosition());
            UnitHud hud = Instantiate(hudPrefab, hudPosition, Quaternion.identity, transform);
            hud.Initialize(unit);
        }
    }
}