using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

class Program
{
    static void Main(string[] args)
    {
        // Input data
        ulong[] inputNumbers = /*{ 125, 17 };//*/{ 5688, 62084, 2, 3248809, 179, 79, 0, 172169 };
        int stepCount = 75;
        LinkedList<ulong> numbers = new();
        numbers.AddFirst(inputNumbers[0]);
        for (int i = 1; i < inputNumbers.Length; i++)
        {
            numbers.AddLast(inputNumbers[i]);
        }
        
        // Run simulation
        var results = CalcWithQueue(inputNumbers, stepCount);

        // Output results
        Console.WriteLine("Simulation Results (per number):");
        ulong totalFinalCount = 0;
        foreach (var result in results)
        {
            Console.WriteLine($"Number: {result.Item1}, Final Count: {result.Item2}");
            totalFinalCount += result.Item2;
        }
        Console.WriteLine($"\nTotal Final Count (after {stepCount} steps): {totalFinalCount}");
    }

    static List<Tuple<ulong, ulong>> CalcWithQueue(ulong[] numbers, int stepCount)
    {
        var results = new List<Tuple<ulong, ulong>>();

        foreach (var number in numbers)
        {
            ulong finalCount = Calc2(number, stepCount);
            results.Add(new Tuple<ulong, ulong>(number, finalCount));
            Console.WriteLine($"Number: {number}, Final Count: {finalCount}");
        }

        return results;
    }

    static List<Tuple<ulong, ulong>> CalcWithParallelExecution(ulong[] numbers, int stepCount)
    {
        var results = new ConcurrentBag<Tuple<ulong, ulong>>();

        // Parallelize computation
        Parallel.ForEach(numbers, number =>
        {
            ulong finalCount = Calc2(number, stepCount);
            results.Add(new Tuple<ulong, ulong>(number, finalCount));
        });

        // Convert to list for return
        return new List<Tuple<ulong, ulong>>(results);
    }
    static ulong Calc(ulong number, int stepCount, int currentCount=0, ulong value=0)
    {
        var list = new List<ulong> { number };


        int currentSize = list.Count;

        for (int i = 0; i < currentSize; i++)
        {
            ulong currentNumber = list[i];
            if (currentNumber == 0)
            {
                list[i] = 1;
            }
            else if (GetDigitCount(currentNumber) % 2 == 0)
            {
                ulong firstHalf, secondHalf;
                SplitNumber(currentNumber, out firstHalf, out secondHalf);
                list[i] = secondHalf;
                list.Insert(i + 1, firstHalf); // Insert the second half
                i++; // Skip the next index due to insertion
            }
            else
            {
                list[i] = currentNumber * 2024;
            }
        }

        value += (ulong)list.Count();
        if (currentCount==stepCount)
        {
            return value;
        }
        currentCount++;
        foreach (var item in list)
        {
            Calc(item, stepCount, currentCount, value);
        }
        return value;
    }

    static ulong Calc3(ulong number, int stepcount,int position)
    {
        var input = number;
        var list=new List<ulong>();
        list.Add(input);
        for (int i = 0; i < stepcount; i++)
        {
            if (list[position] == 0)
            {
                list[position] = 1;
            }
            else if (input.ToString().Length % 2 == 0)
            {
                list[position]=(ulong.Parse(list[position].ToString().Substring(0, list[position].ToString().Length / 2)));
                //queue.Enqueue(ulong.Parse(input.ToString().Substring(input.ToString().Length / 2)));
            }
            else
            {
                list[position] = input * 2024;
            }
        }
    }
    static ulong Calc2(ulong number, int stepCount)
    {
        Queue<ulong> queue = new();
        queue.Enqueue(number);

        for (int step = 0; step < stepCount; step++)
        {
            Console.WriteLine($"Step:{step}");
            int currentSize = queue.Count;

            for (int i = 0; i < currentSize; i++)
            {
                ulong currentNumber = queue.Dequeue();

                if (currentNumber == 0)
                {
                    queue.Enqueue(1);
                }
                else if (currentNumber.ToString().Length % 2 == 0)
                {
                    queue.Enqueue(ulong.Parse(currentNumber.ToString().Substring(0, currentNumber.ToString().Length / 2)));
                    queue.Enqueue(ulong.Parse(currentNumber.ToString().Substring(currentNumber.ToString().Length / 2)));
                }
                else
                {
                    queue.Enqueue(currentNumber * 2024);
                }
            }
        }

        return (ulong)queue.Count;
    }
    //static ulong Calc2(ulong number, int stepCount)
    //{
    //    LinkedList<ulong> linkedList = new();
    //    linkedList.AddFirst(number);
    //    for (int step = 0; step < stepCount; step++)
    //    {
    //        Console.WriteLine($"Step:{step}");
    //        var node = linkedList.First;
    //        while ( node != null)
    //        {
    //            ulong currentNumber = node.Value;

    //            if (currentNumber == 0)
    //            {
    //                node.Value = 1;
    //            }
    //            else if (currentNumber.ToString().Length % 2 == 0)
    //            {
    //                node.Value = ulong.Parse(currentNumber.ToString().Substring(0, currentNumber.ToString().Length/2));
    //                linkedList.AddAfter(node, ulong.Parse(currentNumber.ToString().Substring(currentNumber.ToString().Length / 2, currentNumber.ToString().Length / 2)));
    //                node = node.Next;
    //            }
    //            else
    //            {
    //                node.Value = currentNumber * 2024;
    //            }
    //            if (node!=null)
    //            node = node.Next;
    //        }
    //    }

    //    return (ulong)linkedList.Count;
    //}


    // Method to get the number of digits using division instead of string conversion
    static int GetDigitCount(ulong number)
    {
        int digitCount = 0;
        while (number > 0)
        {
            digitCount++;
            number /= 10;
        }
        return digitCount;
    }

    // Method to split the number without string manipulation
    static void SplitNumber(ulong number, out ulong firstHalf, out ulong secondHalf)
    {
        int digitCount = GetDigitCount(number);
        int halfDigits = digitCount / 2;

        firstHalf = 0;
        secondHalf = 0;

        for (int i = 0; i < halfDigits; i++)
        {
            firstHalf = firstHalf * 10 + (number % 10);
            number /= 10;
        }

        secondHalf = number;
        firstHalf = ReverseNumber(firstHalf); // Reverse to get the first half correctly
    }

    // Method to reverse the number back to correct order
    static ulong ReverseNumber(ulong number)
    {
        ulong reversed = 0;
        while (number > 0)
        {
            reversed = reversed * 10 + (number % 10);
            number /= 10;
        }
        return reversed;
    }
}
