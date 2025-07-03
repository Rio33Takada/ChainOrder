using UnityEngine;

public enum SkillTriggerType
{
    Manual,      // 攻撃・特化スキル
    OnSortie,    // 出撃時（発動型）
    Passive,     // 常在効果（持っているだけで影響）
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
    public int value;  // 効果量（大中小でもOK）

    // 追加パッシブ用フィールド（例：全体攻撃力上昇など）
    //public PassiveEffectCondition passiveCondition;
}