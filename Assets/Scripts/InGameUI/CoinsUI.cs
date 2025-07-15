using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    private int coinsCount;

    private void Start()
    {
        coinsCount = 0;
    }

    public void AddCoin()
    {
        coinsCount++;
        coinsText.text = coinsCount.ToString();
        Debug.Log("Add coin in CoinsUI");
    }

    public int GetCoinsCount()
    {
        return coinsCount;
    }
}
