using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TickerBackgroundChannel.Pricing.Models;

namespace TickerBackgroundChannel.Pricing.Services
{
    internal sealed class PricingGenerator : IPricingGenerator
    {
        private readonly Random _random = new();
        private readonly Dictionary<string, decimal> _currencyPairs = new()
        {
            ["EURUSD"] = 1.13M,
            ["EURGBP"] = 0.85M,
            ["EURCHF"] = 1.04M,
            ["EURPLN"] = 4.62M,

        };

        private readonly ILogger<PricingGenerator> _logger;
        private bool _isRunning;

        public PricingGenerator(ILogger<PricingGenerator> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync()
        {
            _isRunning = true;

            while (_isRunning)
            {
                foreach (var (symbol, pricing) in _currencyPairs)
                {
                    // return if someone decides to stop it.
                    if (!_isRunning) return;

                    // every 1 second provide new value.
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    var tick = NextTick();
                    var newPricing = pricing + tick;
                    _currencyPairs[symbol] = newPricing;

                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    _logger.LogInformation($"Updated pricing for: {symbol}, {pricing:F} -> {newPricing:F} {tick:F}");
                    var newCurrencyPair = new CurrencyPair(symbol, newPricing, timestamp);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }

        }

        public Task StopAsync()
        {
            _isRunning = false;
            return Task.CompletedTask;
        }

        private decimal NextTick()
        {
            var sign = _random.Next(0, 2) == 0 ? -1 : 1;
            var tick = _random.NextDouble() / 20;
            return (decimal)(sign * tick);
        }

    }
}