using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int id;
    public string enemyName;
    public Sprite enemySprite;

    [Header("基礎ステータス")]
    public int maxHP;
    public int attack;
    public int defense;

    [Header("行動パターン")]
    public List<EnemySkill> skills;// 敵専用スキル（スキルSOまたは別クラスで管理）
}
