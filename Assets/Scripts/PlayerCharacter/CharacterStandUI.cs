using UnityEngine;
using UnityEngine.UI;

public class CharacterStandUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer targetableIcon;
    //[SerializeField] private Animator animator;

    [SerializeField] private Button button;

    public BattleCharacter Character { get; private set; }
    public BattleInputManager InputManager { get; private set; }

    public void Initialize(BattleCharacter character, BattleInputManager inputManager)
    {
        Character = character;
        InputManager = inputManager;
        spriteRenderer.sprite = character.BaseData.sdCharacter; // SOŽQÆ
        targetableIcon.enabled = false;
        if (button != null)
        {
            button.onClick.AddListener(() => { InputManager.OnTargetSelected(character); });
        }
    }

    public void MoveTo(Vector3 position, float scale, int order)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        spriteRenderer.sortingOrder = order;
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