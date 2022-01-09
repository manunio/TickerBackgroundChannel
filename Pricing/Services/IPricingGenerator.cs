using System.Threading.Tasks;

namespace TickerBackgroundChannel.Pricing.Services
{
    internal interface IPricingGenerator
    {
        Task StartAsync();
        Task StopAsync();
    }
}