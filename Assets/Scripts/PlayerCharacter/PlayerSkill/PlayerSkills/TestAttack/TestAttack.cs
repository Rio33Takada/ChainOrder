using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestAttack", menuName = "Character/Skill/TestAttack")]
public class TestAttack : PlayerSkill
{
    public override PlayerSkillTargetType TargetType => PlayerSkillTargetType.SingleEnemy;

    public PlayerSkillData attack;

    public override PlayerSkillData GetSkill(BattleCharacter self)
    {
        return attack;
    }
}
