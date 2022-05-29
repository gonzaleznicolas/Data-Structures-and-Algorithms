using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main(String[] args)
    {
        int maxSubsetSum;
        int[] subset;
        SubsetSum(new int[] {5, 10, 12, 7, 15, 13, 7, 25}, 21, out maxSubsetSum, out subset);
        Console.WriteLine($"maxSubsetSum: {maxSubsetSum}");
        Console.WriteLine($"subset:");
        subset.ToList().ForEach(n => { Console.Write($"{n}, "); });
        Console.WriteLine();
    }
    // sumLimit is a positive int
    // nums are all positive ints
    // outputs subset which is a subset of nums with maximum sum <= sumLimit
    // outputs maxSubsetSum which is the sum of subset
    public static void SubsetSum(int[] nums, int sumLimit, out int maxSubsetSum, out int[] subset) {
        var table = new int[nums.Length + 1, sumLimit + 1];
        for (int numNums = 1; numNums <= nums.Length; numNums++) {
            for (int maxSum = 1; maxSum <= sumLimit; maxSum++) {
                var lastNum = nums[numNums-1];
                if (lastNum > maxSum) {
                    // lastNum definitely not in subset
                    table[numNums, maxSum] = table[numNums-1, maxSum];
                } else { // lastNum <= maxSum
                    table[numNums, maxSum] = Math.Max(
                        table[numNums-1, Math.Max(maxSum-lastNum, 0)] + lastNum,
                        table[numNums-1, maxSum]
                    );
                }
            }
        }
        maxSubsetSum = table[nums.Length, sumLimit];

        // now lets figure out the subset
        var subsetList = new List<int>(nums.Length);
        var numNums1 = nums.Length;
        var maxSum1 = sumLimit;
        while (numNums1 != 0 && maxSum1 > 0) {
            var current = table[numNums1, maxSum1];
            var lastNum = nums[numNums1 - 1];
            if (lastNum > maxSum1) {
                numNums1 = numNums1 - 1;
            } else { // lastNum <= maxSum1
                var sumIncludingLastNum = table[numNums1-1, maxSum1 - lastNum] + lastNum;
                var sumWithoutLastNum = table[numNums1-1, maxSum1];
                if (sumIncludingLastNum > sumWithoutLastNum) {
                    subsetList.Add(lastNum);
                    numNums1 = numNums1-1;
                    maxSum1 = maxSum1 - lastNum;
                } else {
                    numNums1 = numNums1-1;
                }
            }
        }
        subset = subsetList.ToArray();
    }
}