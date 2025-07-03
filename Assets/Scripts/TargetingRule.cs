using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum PlayerSkillTargetType
{
    SingleEnemy,
    SingleAlly,
    AllEnemies,
    AllAllies,

}

public enum EnemySkillTargetType
{
    FrontAlly,
    SingleEnemy,
}

public class TargetingRule : MonoBehaviour
{
    /// <summary>
    /// スキル選択時選択可能な対象を示す
    /// </summary>
    /// <param name="targetType"></param>
    /// <param name="allAllies"></param>
    /// <param name="allEnemies"></param>
    /// <returns></returns>
    public List<BattleUnit> GetSelectableTarget(PlayerSkillTargetType targetType, List<BattleCharacter> allAllies, List<BattleEnemyCharacter> allEnemies)
    {
        List<BattleUnit> target;
        switch (targetType)
        {
            case PlayerSkillTargetType.SingleEnemy:
                target = allEnemies.Where(e => e.isDeployed).Cast<BattleUnit>().ToList();
                return target;

            case PlayerSkillTargetType.SingleAlly:
                target = allAllies.Where(a => a.isDeployed).Cast<BattleUnit>().ToList();
                return target;

            case PlayerSkillTargetType.AllEnemies:
                target = allEnemies.Where(e => e.isDeployed).Cast<BattleUnit>().ToList();
                return target;

            case PlayerSkillTargetType.AllAllies:
                target = allAllies.Where(a => a.IsAlive).Cast<BattleUnit>().ToList();
                return target;
            default:
                return null;
        }
    }

    /// <summary>
    /// 対象にマウスオーバー時影響範囲を示す
    /// </summary>
    /// <returns></returns>
    public List<BattleUnit> GetHighlightEffectTarget(PlayerSkillTargetType targetType, BattleUnit selected, List<BattleCharacter> allAllies, List<BattleEnemyCharacter> allEnemies)
    {
        List<BattleUnit> target;
        switch (targetType)
        {
            default:
                return null;
        }
    }
}
