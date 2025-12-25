
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static int CoinChangeTopDown(int[] coins, int amount, Dictionary<int, int> memo = null)
    {
        if (memo == null) memo = new Dictionary<int, int>();
        if (memo.ContainsKey(amount)) return memo[amount];
        if (amount == 0) return 0;
        if (amount < 0) return -1;

        int minCoins = int.MaxValue;
        foreach (int coin in coins)
        {
            int result = CoinChangeTopDown(coins, amount - coin, memo);
            if (result >= 0 && result < minCoins)
            {
                minCoins = result + 1;
            }
        }

        memo[amount] = (minCoins == int.MaxValue) ? -1 : minCoins;
        return memo[amount];
    }

    static int CoinChangeBottomUp(int[] coins, int amount)
    {
        int[] dp = new int[amount + 1];
        Array.Fill(dp, amount + 1);
        dp[0] = 0;

        for (int i = 1; i <= amount; i++)
        {
            foreach (int coin in coins)
            {
                if (coin <= i)
                {
                    dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                }
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }

    static void Main()
    {
        var stopwatch = new Stopwatch();
        var random = new Random();
        int[] coins = { 1, 3, 4, 5 };
        int[] amounts = { 10, 15, 20 }; 

        foreach (int amount in amounts)
        {
            try
            {
                Console.WriteLine($"Testing amount = {amount}...");

                stopwatch.Restart();
                var result1 = CoinChangeTopDown(coins, amount);
                stopwatch.Stop();
                long time1 = stopwatch.ElapsedTicks;

                stopwatch.Restart();
                var result2 = CoinChangeBottomUp(coins, amount);
                stopwatch.Stop();
                long time2 = stopwatch.ElapsedTicks;

                Console.WriteLine($"Amount = {amount,4}: Нисходящий = {time1,6} тиков, " +
                                $"Восходящий = {time2,4} тиков, " +
                                $"Отношение = {time1 / (double)time2,4:0.0}x");
                Console.WriteLine($"Results: TopDown = {result1}, BottomUp = {result2}");
            }
            catch (StackOverflowException)
            {
                Console.WriteLine($"Amount = {amount}: Stack Overflow in TopDown approach!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Amount = {amount}: Error - {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}
