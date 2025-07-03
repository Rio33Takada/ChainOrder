using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Enemy/Skill/SelfHeal")]


public class SelfHeal : EnemySkill
{
    public EnemySkillData heal;

    public int countDown;

    public override EnemySkillData GetSkill(BattleEnemyCharacter self)
    {
        return heal;
    }

    public override List<SkillEffectResult> GetSkillEffect(
        BattleEnemyCharacter self,
        List<BattleCharacter> allAllies,
        List<BattleEnemyCharacter> allEnemies
    )
    {
        BattleUnit unit = allAllies[0];

        BattleUnit target = self; // 先頭エラーが出ないよう仮置き
        int value = heal.amount;

        return new List<SkillEffectResult>
        {
            new SkillEffectResult(target, SkillEffectType.Heal, value, 0, "自己回復")
        };
    }
}
