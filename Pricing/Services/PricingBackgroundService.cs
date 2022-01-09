using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TickerBackgroundChannel.Pricing.Services
{
    internal class PricingBackgroundService : IHostedService
    {
        private readonly IPricingGenerator _pricingGenerator;

        public PricingBackgroundService(IPricingGenerator pricingGenerator)
        {
            _pricingGenerator = pricingGenerator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _pricingGenerator.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _pricingGenerator.StopAsync();
        }
    }
}
