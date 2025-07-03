using UnityEngine;

public class EnemyStandUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public BattleEnemyCharacter BattleEnemy { get; private set; }

    public void Initialize(BattleEnemyCharacter enemy)
    {
        //ï\é¶èâä˙âª
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
