using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerCollisiosn : MonoBehaviour
{
    [SerializeField] private int potionsCount = 0;
    public TextMeshProUGUI countText;

    public MovingObjectSettings boulder;

    private void Start()
    {
        //Debug.Log("Current health: " + TrollyController.instance.currentHealth);
        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            AddPotions();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Boulder"))
        {
            RemovePotions();
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    private void AddPotions()
    {
        potionsCount += 1;
        UpdateText();
    }

    private void RemovePotions() // players collided with boulder, so they lost some potions
                                    // they flew over trolly on the ground?
    {
        if(potionsCount > 0)
        {
            potionsCount -= 1;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        countText.text = "Amount of potions: " + potionsCount.ToString();
    }

    private void TakeDamage()
    {
        //Debug.Log("Current health: " + TrollyController.instance.currentHealth);
        TrollyController.instance.currentHealth -= boulder.damage;
        //Debug.Log("Current health: " + TrollyController.instance.currentHealth);

        if(TrollyController.instance.currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("YOU DIED");
        }
    }
}
