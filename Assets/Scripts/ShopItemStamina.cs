public class ShopItemStamina : ShopItem
{
    protected override SFX PurchaseSFX => SFX.StaminaPickup;

    protected override void OnPurchase()
    {
        PlayerController.Instance.MaxStamina += 5;
    }
}
