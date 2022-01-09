namespace TickerBackgroundChannel.Pricing.Models
{
    internal sealed record CurrencyPair(string Symbol, decimal value, long Timestamp);
}