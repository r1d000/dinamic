using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static long FibonacciTopDown(int n, Dictionary<int, long> memo = null)
    {
        if (memo == null) memo = new Dictionary<int, long>();
        if (memo.ContainsKey(n)) return memo[n];
        if (n <= 1) return n;

        memo[n] = FibonacciTopDown(n - 1, memo) + FibonacciTopDown(n - 2, memo);
        return memo[n];
    }
    static long FibonacciBottomUp(int n)
    {
        if (n <= 1) return n;

        long[] dp = new long[n + 1];
        dp[0] = 0;
        dp[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 2];
        }

        return dp[n];
    }
    static void Main()
    {
        var stopwatch = new Stopwatch();
        var random = new Random();
        Console.WriteLine("Числа Фибоначчи:");
        int[] fibTestCases = { 35, 40, 45 };
        foreach (int n in fibTestCases)
        {
            stopwatch.Restart();
            var result1 = FibonacciTopDown(n);
            stopwatch.Stop();
            long time1 = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            var result2 = FibonacciBottomUp(n);
            stopwatch.Stop();
            long time2 = stopwatch.ElapsedTicks;

            Console.WriteLine($"n = {n}: Нисходящий = {time1,6} тиков, " +
                            $"Восходящий = {time2,4} тиков, " +
                            $"Отношение = {time1 / time2,4:0.0}x");
        }
    }
}
