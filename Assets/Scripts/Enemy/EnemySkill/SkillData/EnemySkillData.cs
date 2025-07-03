using UnityEngine;

//“G‚ªs‚¤UŒ‚‚Ì–{‘Ì
[CreateAssetMenu(fileName = "EnemySkillData", menuName = "ScriptableObjects/EnemySkillData")]
public class EnemySkillData : ScriptableObject
{
    public string skillName;
    public int amount;
    public int countdown;
    public SkillEffectType effectType;
    // etc...
}
