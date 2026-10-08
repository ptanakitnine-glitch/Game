using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;

    private void Update()
    {
        currencyText.text = "$" + LevelManager.main.currency;
    }
}