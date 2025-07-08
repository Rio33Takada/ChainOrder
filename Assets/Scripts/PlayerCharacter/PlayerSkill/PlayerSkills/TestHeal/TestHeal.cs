using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestHeal", menuName = "Character/Skill/TestHeal")]
public class TestHeal : PlayerSkill
{
    public override PlayerSkillTargetType TargetType => PlayerSkillTargetType.SingleAlly;

    public PlayerSkillData skillData;

    public override PlayerSkillData GetSkill(BattleCharacter self)
    {
        return skillData;
    }

    public override List<SkillEffectResult> GetSkillEffect(BattleCharacter self, BattleUnit selected, List<BattleCharacter> allAllies, List<BattleEnemyCharacter> allEnemies)
    {
        BattleUnit target = selected;
        int healAmount = skillData.value;

        return new List<SkillEffectResult>
        {
            new SkillEffectResult(target, SkillEffectType.Heal, healAmount, 0, "testHeal")
        };
    }
}
