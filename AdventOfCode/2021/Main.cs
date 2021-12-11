using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Event2021
    {
        public int Day1_1()
        {
            //https://adventofcode.com/2021/day/1
            int previousDepth = -1;
            int countUp = 0;
            List<string> lines = System.IO.File.ReadAllLines(@"2021/Day1_1.txt").ToList();
            
            foreach (var depthString in lines)
            {
                int depth = Convert.ToInt32(depthString);
                if (previousDepth == -1)
                {
                    Console.WriteLine("No previous measurement");
                }
                else if (depth == previousDepth)
                {
                    Console.WriteLine("Same as the last");
                }
                else if (depth > previousDepth)
                {
                    Console.WriteLine("Up");
                    countUp++;
                }
                else
                {
                    Console.WriteLine("Down");
                }
                previousDepth = depth;
            }
            
            return countUp;
        }

        public int Day1_2()
        {
            List<string> lines = System.IO.File.ReadAllLines(@"2021/Day1_1.txt").ToList();
            List<int> lastThree = new List<int>();
            int lastMeasurement = 0;
            for (int i = 0; i <= lines.Count; i++)
            {
                int depth = Convert.ToInt32(lines[i]);
                if (lastThree.Count < 3)
                {
                    lastThree.Add(depth);
                    if (lastThree.Count == 3)
                    {
                        foreach (var depthValue in lastThree)
                        {
                            lastMeasurement += depthValue;
                        }

                        lastThree.RemoveAt(0);
                    }
                }
            }

            return 0;
        }
    }
}