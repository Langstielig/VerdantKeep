using UnityEngine;

public class MobileControlsUI : MonoBehaviour, IHUDPart
{
    [SerializeField] private GameObject moveStick;
    [SerializeField] private GameObject AttackButtons;

    public void SetVisible(bool visible)
    {
        moveStick.SetActive(visible);
        AttackButtons.SetActive(visible);
    }
}
