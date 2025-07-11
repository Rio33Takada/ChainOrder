using UnityEngine;
using System.Collections.Generic;

//�G�̍s����`(�s������Ȃǂ��܂߂�����
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