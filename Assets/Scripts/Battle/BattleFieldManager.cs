using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;

namespace My.Base.Battle
{
    public class BattleFieldManager : MonoBehaviour
    {
        [SerializeField] private Transform[] firstTeamPoints;
        [SerializeField] private Transform[] secondTeamPoints;

        public Transform GetEmptyPlace(Team team)
        {
            switch (team)
            {
                case Team.First:
                    return GetFirstEmptySpace(firstTeamPoints);
                case Team.Second:
                    return GetFirstEmptySpace(secondTeamPoints);
                default:
                    throw new InvalidEnumArgumentException($"case for {team} is undefined");
            }
        }

        private Transform GetFirstEmptySpace(Transform[] points)
        {
            foreach (var point in points)
            {
                if (point.childCount > 0) continue;
                return point;
            }

            Debug.Log($"No more empty space on field in {points[0].parent.name} object. Check it");
            return null;
        }
    }
}