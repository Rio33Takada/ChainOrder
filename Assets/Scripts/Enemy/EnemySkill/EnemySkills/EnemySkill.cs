using UnityEngine;
using System.Collections.Generic;

//“G‚Ìs“®’è‹`(s“®•ªŠò‚È‚Ç‚ğŠÜ‚ß‚½‚à‚Ì
public abstract class EnemySkill : ScriptableObject
{
    public abstract EnemySkillData GetSkill(BattleEnemyCharacter self);

    public abstract List<SkillEffectResult> GetSkillEffect(
        BattleEnemyCharacter self,
        List<BattleCharacter> allAllies,
        List<BattleEnemyCharacter> allEnemies
    );
}