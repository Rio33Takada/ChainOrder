using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerSkill : ScriptableObject
{
    public virtual string skillName => "";
    public virtual PlayerSkillTargetType TargetType => PlayerSkillTargetType.SingleAlly;
    public abstract PlayerSkillData GetSkill(BattleCharacter self);

    public abstract List<SkillEffectResult> GetSkillEffect(
        BattleCharacter self,
        BattleUnit selected,
        List<BattleCharacter> allAllies,
        List<BattleEnemyCharacter> allEnemies
        );
}
