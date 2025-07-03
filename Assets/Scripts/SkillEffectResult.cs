using UnityEngine;

public enum SkillEffectType
{
    Damage,
    Heal,
    Buff,
    Debuff,
    Shield,
    StatusEffect,
    Regen,         // ������
    GainStack      // �X�^�b�N�t�^
}

public class SkillEffectResult
{
    public BattleUnit Target { get; set; }
    public SkillEffectType EffectType { get; set; }
    public int Value { get; set; }
    public int Duration { get; set; }  // �����^�[�����i0�Ȃ瑦���j
    public string EffectName { get; set; } // ��F�o�t�̖��O���Ԉُ햼�i�C�Ӂj

    public SkillEffectResult(
        BattleUnit target,
        SkillEffectType type,
        int value,
        int duration = 0,
        string effectName = "")
    {
        Target = target;
        EffectType = type;
        Value = value;
        Duration = duration;
        EffectName = effectName;
    }
}