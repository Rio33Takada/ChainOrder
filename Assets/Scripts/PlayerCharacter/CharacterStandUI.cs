using UnityEngine;
using UnityEngine.UI;

public class CharacterStandUI : StandUI
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private Animator animator;

    public BattleCharacter Character { get; private set; }
    public BattleInputManager InputManager { get; private set; }

    public void Initialize(BattleCharacter character, BattleInputManager inputManager)
    {
        Character = character;
        InputManager = inputManager;
        spriteRenderer.sprite = character.BaseData.sdCharacter; // SOŽQÆ
        HideTargetIcon();
        if (button != null)
        {
            button.onClick.AddListener(() => { InputManager.OnTargetSelected(character); });
        }
    }

    public void MoveTo(Vector3 position, float scale, int order)
    {
        float sizeX = spriteRenderer.bounds.size.x;
        transform.position = position;
        spriteRenderer.transform.localScale = Vector3.one * scale / sizeX;
        spriteRenderer.sortingOrder = order;

        if(order == 4)
        {
            UICanvas.enabled = false;
        }
        else
        {
            UICanvas.enabled = true;
        }

        UICanvas.sortingOrder = order;
        button.GetComponent<RectTransform>().sizeDelta = Vector2.one * scale;
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(50 + 70 * scale, 20);
        hpBar.transform.localPosition = new(0, 0.75f * scale, 0);
    }

    public void UpdateState()
    {
        hpBar.value = Mathf.Clamp(Character.currentHP / Character.MaxHP, 0, 1);
    }
}