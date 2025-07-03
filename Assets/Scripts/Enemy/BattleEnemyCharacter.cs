using UnityEngine;

public class BattleEnemyCharacter : BattleUnit
{
    public EnemyData enemyData { get; private set; }

    //カウントダウン
    public int currentCountDown;

    //位置
    public int position;

    //場にいるかどうか
    public bool isDeployed;

    public EnemySkill nextSkill { get; private set; }

    public BattleEnemyCharacter(EnemyData baseData)
    {
        enemyData = baseData;
    }
}
