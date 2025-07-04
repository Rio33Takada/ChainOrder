using UnityEngine;

public class EnemyStandUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public BattleEnemyCharacter BattleEnemy { get; private set; }

    public void Initialize(BattleEnemyCharacter enemy)
    {
        //表示初期化
        BattleEnemy = enemy;
        spriteRenderer.sprite = enemy.enemyData.enemySprite;
    }

    public void MoveTo(Vector3 position, float scale, int order)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        spriteRenderer.sortingOrder = order;
    }
}
