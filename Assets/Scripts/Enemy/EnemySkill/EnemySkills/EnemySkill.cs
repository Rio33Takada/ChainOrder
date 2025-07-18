using UnityEngine;
using System.Collections.Generic;

//敵の行動定義(行動分岐などを含めたもの
public abstract class EnemySkill : ScriptableObject
{
    public virtual string skillName => "";

    public abstract EnemySkillData GetSkill(BattleEnemyCharacter self);

    public abstract List<SkillEffectResult> GetSkillEffect(
        BattleEnemyCharacter self,
        List<BattleCharacter> allAllies,
        List<BattleEnemyCharacter> allEnemies
    );
}