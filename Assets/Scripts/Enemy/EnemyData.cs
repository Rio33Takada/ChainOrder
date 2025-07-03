using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int id;
    public string enemyName;
    public Sprite enemySprite;

    [Header("��b�X�e�[�^�X")]
    public int maxHP;
    public int attack;
    public int defense;

    [Header("�s���p�^�[��")]
    public List<EnemySkill> skills;// �G��p�X�L���i�X�L��SO�܂��͕ʃN���X�ŊǗ��j
}
