using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int potionsCount = 0;
    public TextMeshProUGUI countText;

    private void Start()
    {
        UpdateText();
    }
    public void AddPotions()
    {
        potionsCount += 1;
        UpdateText();
    }

    public void RemovePotions() // players collided with boulder, so they lost some potions
                                 // they flew over trolly on the ground?
    {
        if (potionsCount > 0)
        {
            potionsCount -= 1;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        countText.text = "Amount of potions: " + potionsCount.ToString();
    }
}
