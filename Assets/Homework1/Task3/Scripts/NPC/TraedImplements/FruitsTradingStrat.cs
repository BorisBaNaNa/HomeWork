public class FruitsTradingStrat : ITradeImplementation
{
    public void BuyItems() { }

    public string[] GetTradeItemsList() => new string[]
    {
        "Ананасы",
        "апельсины",
        "бананы",
        "кокосы",
        "гранаты"
    };

    public void SellItems() { }
}
