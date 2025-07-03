using UnityEngine;

public class BattleEnemyCharacter : BattleUnit
{
    public EnemyData enemyData { get; private set; }

    //�J�E���g�_�E��
    public int currentCountDown;

    //�ʒu
    public int position;

    //��ɂ��邩�ǂ���
    public bool isDeployed;

    public EnemySkill nextSkill { get; private set; }

    public BattleEnemyCharacter(EnemyData baseData)
    {
        enemyData = baseData;
    }
}
