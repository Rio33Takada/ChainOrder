using UnityEngine;
using System.Collections.Generic;

public enum AttackTrait
{
    None, Cut, Pierce, Smash, Blast, Wave, Rise
}

public abstract class PlayerSkill : ScriptableObject
{
    [SerializeField]
    private string _skillName = "New Skill";

    [SerializeField]
    private bool _isChainSkill = false;

    [SerializeField]
    private PlayerSkillTargetType _targetType = PlayerSkillTargetType.SingleAlly;

    [SerializeField]
    private AttackTrait _attackTrait = AttackTrait.Blast;

    [SerializeField]
    private AttackTrait _targetAttackTrait = AttackTrait.None;

    public string skillName => _skillName;
    public bool isChainSkill => _isChainSkill;
    public PlayerSkillTargetType TargetType => _targetType;

    public AttackTrait attackTrait => _attackTrait;

    public AttackTrait targetAttackTrait => _targetAttackTrait;

    public abstract PlayerSkillData GetSkill(BattleCharacter self);

    public abstract List<SkillEffectResult> GetSkillEffect(
        BattleCharacter self,
        BattleUnit selected,
        List<BattleCharacter> allAllies,
        List<BattleEnemyCharacter> allEnemies
    );
}
