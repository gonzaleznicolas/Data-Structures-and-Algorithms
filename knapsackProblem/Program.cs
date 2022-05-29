using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main(String[] args)
    {
        int maxValue;
        Tuple<int, int>[] subset;
        KnapsackProblem(
            new Tuple<int, int>[] {
                new Tuple<int, int>(5, 5),
                new Tuple<int, int>(9, 2),
                new Tuple<int, int>(3, 7),
                new Tuple<int, int>(6, 1),
                new Tuple<int, int>(1, 2)
            },
            5,
            out maxValue,
            out subset);
        Console.WriteLine($"maxValue: {maxValue}");
        Console.WriteLine($"subset:");
        subset.ToList().ForEach(p => { Console.Write($"{p}, "); });
        Console.WriteLine();
    }

    // weightLimit is a positive int
    // pairs are value-weight pairs where all values and weights are positive ints
    // outputs subset which is a subset of pairs with maximum total value and total weight <= weightLimit
    // outputs maxValue which is the total value of subset
    public static void KnapsackProblem(Tuple<int, int>[] pairs, int weightLimit, out int maxValue, out Tuple<int, int>[] subset) {
        var table = new int[pairs.Length + 1, weightLimit + 1];
        for (int i = 1; i <= pairs.Length; i++) {
            for (int wl = 1; wl <= weightLimit; wl++) {
                var currentPair = pairs[i-1];
                var currentValue = currentPair.Item1;
                var currentWeight = currentPair.Item2;
                if (currentWeight > wl) {
                    table[i, wl] = table[i-1, wl];
                } else { // currentWeight <= wl
                    table[i, wl] = Math.Max(
                        table[i-1, Math.Max(wl-currentWeight, 0)] + currentValue,
                        table[i-1, currentWeight]
                    );
                }
            }
        }
        maxValue = table[pairs.Length, weightLimit];

        // now lets figure out the subset
        var subsetList = new List<Tuple<int,int>>(pairs.Length);
        var i1 = pairs.Length;
        var wl1 = weightLimit;
        while (i1 != 0 && wl1 > 0) {
            var current = table[i1, wl1];
            var currentPair = pairs[i1 - 1];
            var currentValue = currentPair.Item1;
            var currentWeight = currentPair.Item2;
            if (currentWeight > wl1) {
                i1 = i1 - 1;
            } else { // currentWeight <= wl
                var valueIncludingCurrentPair = table[i1-1, wl1 - currentWeight] + currentValue;
                var valueWithoutCurrentPair = table[i1-1, wl1];
                if (valueIncludingCurrentPair > valueWithoutCurrentPair) {
                    subsetList.Add(currentPair);
                    i1 = i1-1;
                    wl1 = wl1 - currentWeight;
                } else {
                    i1 = i1-1;
                }
            }
        }
        subset = subsetList.ToArray();
    }
}