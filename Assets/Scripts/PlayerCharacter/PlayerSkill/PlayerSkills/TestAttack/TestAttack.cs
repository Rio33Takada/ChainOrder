using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestAttack", menuName = "Character/Skill/TestAttack")]
public class TestAttack : PlayerSkill
{
    public override string skillName => "TestAttack";
    public override PlayerSkillTargetType TargetType => PlayerSkillTargetType.SingleEnemy;

    public PlayerSkillData skillData;

    public override PlayerSkillData GetSkill(BattleCharacter self)
    {
        return skillData;
    }

    public override List<SkillEffectResult> GetSkillEffect(BattleCharacter self, BattleUnit selected, List<BattleCharacter> allAllies, List<BattleEnemyCharacter> allEnemies)
    {
        BattleUnit target = selected;
        int damage = skillData.value;

        return new List<SkillEffectResult>
        {
            new SkillEffectResult(target, SkillEffectType.Damage, damage, 0, "testAttack")
        };
    }
}
