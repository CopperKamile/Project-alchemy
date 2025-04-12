using UnityEngine;

public class CheckColliisionWeightBalance : MonoBehaviour
{
    private WeightBalance balanceScript;

    private void Start()
    {
        balanceScript = GetComponent<WeightBalance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Risk zone"))
        {
            Debug.Log("Ball entered risk zone");
            balanceScript.isBalanceOff = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Risk zone"))
        {
            Debug.Log("Ball exits risk zone");
            balanceScript.isBalanceOff = false;
        }
    }
}
