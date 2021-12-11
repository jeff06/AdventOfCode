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
            List<string> lines = System.IO.File.ReadAllLines(@"2021/Day1.txt").ToList();

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
            List<string> lines = System.IO.File.ReadAllLines(@"2021/Day1.txt").ToList();
            int[] window = {-1, -1, -1, -1};
            int currentElementInArray = 0;
            int count = -1;
            int currentValue = 0;
            int previousDepth = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                int depth = Convert.ToInt32(lines[i]);
                for (int j = 0; j < window.Length; j++)
                {
                    if (window[j] == -1)
                    {
                        window[j] = depth;
                        currentElementInArray++;
                        break;
                    }
                }

                if (currentElementInArray == 3)
                {
                    int indexFirstElementNotFiller = -1;
                    for (int j = 0; j < window.Length; j++)
                    {
                        if (window[j] != -1)
                        {
                            if (indexFirstElementNotFiller == -1)
                            {
                                indexFirstElementNotFiller = j;
                            }
                            
                            currentValue += window[j];
                        }
                    }
                    
                    //Remove first element that is not -1;
                    window[indexFirstElementNotFiller] = -1;
                    currentElementInArray--;

                    if (previousDepth == -1)
                    {
                        Console.WriteLine("No previous");
                    }
                    else if (currentValue > previousDepth)
                    {
                        Console.WriteLine("Up");
                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Down");
                    }

                    previousDepth = currentValue;
                }
            }

            return count;
        }
    }
}