using UnityEngine;

public class TrolleyBalanceBrokenAnimation : MonoBehaviour
{
    //For now it is not an animation, just indications via changing colors of sprites
    private SpriteRenderer trolley;
    public Color trolleyBrokenBalanceColor;
    private Color originalColor;

    private void Start()
    {
        trolley = GetComponent<SpriteRenderer>();
        originalColor = trolley.color;
    }


    public void FullyBrokenBalanceIndications()
    {
        if (TrollyController.instance.currentHealth > 0 || trolley.gameObject != null)
        {
            trolley.color = trolleyBrokenBalanceColor;
        }
    }

    public void FixedTrolleyBalanceIndications()
    {
        if (TrollyController.instance.currentHealth > 0 || trolley.gameObject != null)
        {
            trolley.color = originalColor;
        }
    }
}
