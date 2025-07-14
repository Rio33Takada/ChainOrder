using UnityEngine;
using UnityEngine.UI;

public abstract class StandUI : MonoBehaviour
{
    public Slider hpBar;
    public Canvas UICanvas;
    public Button button;
    public SpriteRenderer targetableIcon;

    public void ShowTargetIcon()
    {
        targetableIcon.enabled = true;
    }

    public void HideTargetIcon()
    {
        targetableIcon.enabled = false;
    }
}
