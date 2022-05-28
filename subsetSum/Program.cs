using System;
using System.Linq;

public class Program
{
    public static void Main(String[] args)
    {
        int maxSubsetWeight;
        int[] subset;
        SubsetSum(21, new int[] {5, 10, 12, 7, 15, 13, 8, 25}, out maxSubsetWeight, out subset);
        Console.WriteLine($"maxSubsetWeight: {maxSubsetWeight}");
        Console.WriteLine($"subset:");
        subset.ToList().ForEach(n => { Console.Write(n); });
        Console.WriteLine();
    }

    public static void SubsetSum(int weightLimit, int[] nums, out int maxSubsetWeight, out int[] subset) {
        maxSubsetWeight = 0;
        subset = new int[] { 0 };
    }
}
