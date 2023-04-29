public class ShopItemHealth : ShopItem
{
    protected override void OnPurchase()
    {
        PlayerHealthController.Instance.MaxHP += 10;
    }
}
