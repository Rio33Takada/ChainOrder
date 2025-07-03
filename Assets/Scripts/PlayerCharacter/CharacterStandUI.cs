using UnityEngine;

public class CharacterStandUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer targetableIcon;
    //[SerializeField] private Animator animator;

    public BattleCharacter Character { get; private set; }

    public void Initialize(BattleCharacter character)
    {
        Character = character;
        spriteRenderer.sprite = character.BaseData.sdCharacter; // SOéQè∆
        targetableIcon.enabled = false;
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