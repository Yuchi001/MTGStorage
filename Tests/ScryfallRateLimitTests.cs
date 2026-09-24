using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MTGStorage.Database.Endpoints;

internal static class ScryfallRateLimitTests
{
    private sealed class Handler : HttpMessageHandler
    {
        public readonly List<double> Starts = new List<double>();
        private readonly Stopwatch clock = Stopwatch.StartNew();
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Starts.Add(clock.Elapsed.TotalMilliseconds);
            if (Starts.Count == 2) throw new HttpRequestException("Simulated failure");
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }

    private static void Main() { Run().GetAwaiter().GetResult(); }

    private static async Task Run()
    {
        var method = typeof(ScryfallEndpoints).GetMethod("GetNamedResponse", BindingFlags.Static | BindingFlags.NonPublic);
        using (var handler = new Handler())
        using (var client = new HttpClient(handler))
        {
            var calls = new List<Task>();
            for (int i = 0; i < 4; i++)
            {
                var task = (Task<HttpResponseMessage>)method.Invoke(null, new object[] { client,
                    "https://api.scryfall.com/cards/named?" + (i % 2 == 0 ? "exact" : "fuzzy") + "=test" });
                calls.Add(Observe(task));
            }
            await Task.WhenAll(calls);
            if (handler.Starts.Count != 4) throw new Exception("Failed request blocked subsequent requests.");
            for (int i = 1; i < handler.Starts.Count; i++)
            {
                var interval = handler.Starts[i] - handler.Starts[i - 1];
                // Allow a small measurement overhead on the first HttpClient invocation.
                if (interval < 590) throw new Exception("Requests too close: " + interval + " ms");
                Console.WriteLine("Request interval: " + interval.ToString("F1") + " ms");
            }
            Console.WriteLine("PASS: concurrent named requests are throttled and failures release the queue.");
        }
    }

    private static async Task Observe(Task<HttpResponseMessage> task)
    {
        try { using (var response = await task) { } }
        catch (HttpRequestException) { }
    }
}
