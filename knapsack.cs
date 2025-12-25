using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static int Knapsack2D(int[] weights, int[] values, int capacity)
    {
        int n = weights.Length;
        int[,] dp = new int[n + 1, capacity + 1];

        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                if (weights[i - 1] <= w)
                {
                    dp[i, w] = Math.Max(
                        dp[i - 1, w],
                        dp[i - 1, w - weights[i - 1]] + values[i - 1]
                    );
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }
        return dp[n, capacity];
    }

    static int Knapsack1D(int[] weights, int[] values, int capacity)
    {
        int n = weights.Length;
        int[] dp = new int[capacity + 1];

        for (int i = 0; i < n; i++)
        {
            for (int w = capacity; w >= weights[i]; w--)
            {
                dp[w] = Math.Max(dp[w], dp[w - weights[i]] + values[i]);
            }
        }
        return dp[capacity];
    }

    static void Main()
    {
        var stopwatch = new Stopwatch();
        var random = new Random();
        int[] weights = { 2, 3, 4, 5, 6, 7, 8 };
        int[] values = { 3, 4, 5, 8, 10, 12, 13 };
        int capacity = 20;

        long memoryBefore = GC.GetTotalMemory(true);

        stopwatch.Restart();
        var result2D = Knapsack2D(weights, values, capacity);
        stopwatch.Stop();
        long time2D = stopwatch.ElapsedTicks;
        long memoryAfter2D = GC.GetTotalMemory(false);

        long memoryBefore1D = GC.GetTotalMemory(true);

        stopwatch.Restart();
        var result1D = Knapsack1D(weights, values, capacity);
        stopwatch.Stop();
        long time1D = stopwatch.ElapsedTicks;
        long memoryAfter1D = GC.GetTotalMemory(false);

        Console.WriteLine($"2D версия: {time2D,5} тиков, результат = {result2D}");
        Console.WriteLine($"1D версия: {time1D,5} тиков, результат = {result1D}");
        Console.WriteLine($"Ускорение: {time2D / time1D,4:0.0}x");
    }
}
