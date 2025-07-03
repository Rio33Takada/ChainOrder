using UnityEngine;

public enum SkillTriggerType
{
    Manual,      // �U���E�����X�L��
    OnSortie,    // �o�����i�����^�j
    Passive,     // ��݌��ʁi�����Ă��邾���ŉe���j
    OnChain,
}

public enum AttackTrait
{
    None, Cut, Pierce, Smash, Blast, Wave, Rise
}

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public SkillTriggerType triggerType;

    public SkillEffectType effectType;
    public int value;  // ���ʗʁi�咆���ł�OK�j

    // �ǉ��p�b�V�u�p�t�B�[���h�i��F�S�̍U���͏㏸�Ȃǁj
    //public PassiveEffectCondition passiveCondition;
}