public class ArmorTradingStrat : ITradeImplementation
{
    public void BuyItems() { }

    public string[] GetTradeItemsList() => new string[]
    {
        "light body armor",
        "medium body armor",
        "heavy body armor"
    };

    public void SellItems() { }
}
