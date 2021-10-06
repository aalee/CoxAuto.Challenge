using System;
using System.Threading.Tasks;

namespace CoxAuto.Challenge.UI
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Working...");

            var messageAnswer = await Core.Workers.Challenge.RunAsync();

            Console.WriteLine($"Was success this execution?, {messageAnswer.Success}");
            Console.WriteLine($"Message from API: {messageAnswer.Message}");
            Console.WriteLine(
                $"This execution took {messageAnswer.TotalMilliseconds / 1000} seconds or {messageAnswer.TotalMilliseconds} Milliseconds");
            Console.WriteLine();
            Console.WriteLine("Press any key to terminate...");
            Console.ReadKey();
        }
    }
}