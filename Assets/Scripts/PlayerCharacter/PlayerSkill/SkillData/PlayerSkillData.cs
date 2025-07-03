using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillData", menuName = "ScriptableObjects/PlayerSkillData")]
public class PlayerSkillData : ScriptableObject
{
    public string skillName;
    public int value;
    public SkillEffectType effectType;
}
