using UnityEngine;

public class ScorePanel : MonoBehaviour, IHUDPart
{
    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
