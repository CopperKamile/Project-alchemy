using TMPro;
using UnityEngine;

public class CollisionWithPotions : MonoBehaviour
{
    [SerializeField] private int potionsCount = 0;
    public TextMeshProUGUI countText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            AddPotions();
            Destroy(collision.gameObject);
        }
    }

    private void AddPotions()
    {
        potionsCount += 1;
        countText.text = "Amount of potions: " + potionsCount.ToString();
    }
}
