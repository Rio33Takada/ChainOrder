using UnityEngine;

//�G���s���U���̖{��
[CreateAssetMenu(fileName = "EnemySkillData", menuName = "ScriptableObjects/EnemySkillData")]
public class EnemySkillData : ScriptableObject
{
    public string skillName;
    public int amount;
    public int countdown;
    public SkillEffectType effectType;
    // etc...
}
