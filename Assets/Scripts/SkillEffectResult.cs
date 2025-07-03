using UnityEngine;

public enum SkillEffectType
{
    Damage,
    Heal,
    Buff,
    Debuff,
    Shield,
    StatusEffect,
    Regen,         // 持続回復
    GainStack      // スタック付与
}

public class SkillEffectResult
{
    public BattleUnit Target { get; set; }
    public SkillEffectType EffectType { get; set; }
    public int Value { get; set; }
    public int Duration { get; set; }  // 持続ターン数（0なら即時）
    public string EffectName { get; set; } // 例：バフの名前や状態異常名（任意）

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