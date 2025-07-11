using UnityEngine;
using UnityEngine.UI;

public class EnemyStandUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer targetableIcon;

    [SerializeField] private Button button;

    public BattleEnemyCharacter BattleEnemy { get; private set; }
    public BattleInputManager InputManager { get; private set; }

    public void Initialize(BattleEnemyCharacter enemy, BattleInputManager inputManager)
    {
        //•\Ž¦‰Šú‰»
        BattleEnemy = enemy;
        InputManager = inputManager;
        spriteRenderer.sprite = enemy.enemyData.enemySprite;
        targetableIcon.enabled = false;
        if (button == null)
        {
            button.onClick.AddListener(() => { InputManager.OnTargetSelected(BattleEnemy); });
        }
    }

    public void MoveTo(Vector3 position, float scale, int order)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        spriteRenderer.sortingOrder = order;

        button.gameObject.GetComponent<RectTransform>().sizeDelta = new(spriteRenderer.bounds.size.x / scale, spriteRenderer.bounds.size.x / scale);
    }

    public void ShowTargetIcon()
    {
        targetableIcon.enabled = true;
    }

    public void HideTargetIcon()
    {
        targetableIcon.enabled = false;
    }
}
