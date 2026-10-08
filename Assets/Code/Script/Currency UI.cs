using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI heartText;

    private void Update()
    {
        currencyText.text = "" + LevelManager.main.currency;
        heartText.text ="" + LevelManager.main.waveHeart;
    }
}