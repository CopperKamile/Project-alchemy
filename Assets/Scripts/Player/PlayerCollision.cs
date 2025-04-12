using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerCollisiosn : MonoBehaviour
{
    private CurrencyManager currencyManager;

    public MovingObjectSettings boulder;

    private void Start()
    {
        currencyManager = GetComponent<CurrencyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            currencyManager.AddPotions();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Boulder"))
        {
            currencyManager.RemovePotions();
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage()
    {
        TrollyController.instance.currentHealth -= boulder.damage;

        if(TrollyController.instance.currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("YOU DIED");
        }
    }
}
