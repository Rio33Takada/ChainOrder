using UnityEngine;
using UnityEngine.UI;

public class EnemyStandUI : StandUI
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public BattleEnemyCharacter BattleEnemy { get; private set; }
    public BattleInputManager InputManager { get; private set; }

    public void Initialize(BattleEnemyCharacter enemy, BattleInputManager inputManager)
    {
        //•\Ž¦‰Šú‰»
        BattleEnemy = enemy;
        InputManager = inputManager;
        spriteRenderer.sprite = enemy.enemyData.enemySprite;
        HideTargetIcon();
        if (button != null)
        {
            button.onClick.AddListener(() => { InputManager.OnTargetSelected(BattleEnemy); });
        }
    }

    public void MoveTo(Vector3 position, float scale, int order)
    {
        float sizeX = spriteRenderer.bounds.size.x;
        transform.position = position;
        spriteRenderer.transform.localScale = Vector3.one * scale / sizeX;
        spriteRenderer.sortingOrder = order;

        UICanvas.sortingOrder = order;
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);
        hpBar.transform.localPosition = new(0, 0.75f, 0);
    }

    public void UpdateState()
    {
        hpBar.value = Mathf.Clamp(BattleEnemy.currentHP / BattleEnemy.MaxHP, 0, 1);
    }
}