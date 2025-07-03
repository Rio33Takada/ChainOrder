using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Enemy/Skill/NormalAttack")]
public class NormalAttack : EnemySkill
{
    public EnemySkillData attack;

    public int countDown;
    public override EnemySkillData GetSkill(BattleEnemyCharacter self)
    {
        return attack;
    }

    public override List<SkillEffectResult> GetSkillEffect(
        BattleEnemyCharacter self,
        List<BattleCharacter> allAllies,
        List<BattleEnemyCharacter> allEnemies
    )
    {
        BattleUnit unit = allAllies[0];

        BattleUnit target = self; // 先頭エラーが出ないよう仮置き
        int damage = attack.amount;

        return new List<SkillEffectResult>
        {
            new SkillEffectResult(target, SkillEffectType.Damage, damage, 0, "通常攻撃")
        };
    }
}
