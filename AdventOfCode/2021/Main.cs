using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Event2021
    {
        #region Day 1

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
            int count = 0;
            int nbPerSliding = 3;
            int lastMeasurement = -1;
            // Initialize result
            int max_sum = int.MinValue;

            // Consider all blocks starting
            // with i.
            for (int i = 0; i < lines.Count - nbPerSliding + 1; i++)
            {
                int currentMeasurement = 0;
                for (int j = 0; j < nbPerSliding; j++)
                {
                    currentMeasurement += Convert.ToInt32(lines[i + j]);
                }

                if (lastMeasurement == -1)
                {
                    Console.WriteLine("No previous");
                }
                else if (currentMeasurement > lastMeasurement)
                {
                    Console.WriteLine("Up");
                    count++;
                }
                else if (currentMeasurement == lastMeasurement)
                {
                    Console.WriteLine("No change");
                }
                else
                {
                    Console.WriteLine("Down");
                }

                lastMeasurement = currentMeasurement;
            }

            return count;
        }

        #endregion

        #region Day 2
        public int Day2_1()
        {
            List<string> instructions = Helper.GetFileContent("2021/Day2.txt");
            int horizontal = 0;
            int depth = 0;
            foreach (var instruction in instructions)
            {
                string[] step = instruction.Split(" ");
                string action = step[0];
                int value = Convert.ToInt32(step[1]);
                if (action.Contains("forward"))
                {
                    horizontal += value;
                }
                else if (action.Contains("down"))
                {
                    depth += value;
                }
                else if (action.Contains("up"))
                {
                    depth -= value;
                }
                else
                {
                    throw new Exception("Wrong instruction");
                }
            }

            return horizontal * depth;
        }

        public int Day2_2()
        {
            List<string> instructions = Helper.GetFileContent("2021/Day2.txt");
            int horizontal = 0;
            int depth = 0;
            int aim = 0;
            foreach (var instruction in instructions)
            {
                string[] step = instruction.Split(" ");
                string action = step[0];
                int value = Convert.ToInt32(step[1]);
                if (action.Contains("forward"))
                {
                    horizontal += value;
                    if (aim != 0)
                    {
                        depth += value * aim;
                    }
                }
                else if (action.Contains("down"))
                {
                    aim += value;
                }
                else if (action.Contains("up"))
                {
                    aim -= value;
                }
                else
                {
                    throw new Exception("Wrong instruction");
                }
            }

            return depth * horizontal;
        }
        #endregion

        #region Day 3

        public int Day3_1()
        {
            List<string> reportValues = Helper.GetFileContent("2021/Day3.txt");
            string gamma = "";
            string epsilon = "";
            string[] arr = { "", "", "", "", "", "", "", "", "", "", "", "", };
            foreach (var value in reportValues)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    arr[i] += value[i];
                }
            }

            foreach (var value in arr)
            {
                string currentBit = value.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key.ToString();

                if (currentBit == "0")
                {
                    gamma += "0";
                    epsilon += "1";
                }
                else
                {
                    gamma += "1";
                    epsilon += "0";
                }
            }

            int currentCount = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
            return currentCount;
        }

        public int Day3_2()
        {
            List<string> reportValuesOxy = Helper.GetFileContent("2021/Day3.txt");
            List<string> reportValuesCo2 = Helper.GetFileContent("2021/Day3.txt");

            string[] arr = { "", "", "", "", "", "", "", "", "", "", "", "" };
            foreach (var value in reportValuesOxy)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    arr[i] += value[i];
                }
            }

            string oxygen = "";
            string co2 = "";

            foreach (var value in arr)
            {
                int countOne = value.Count(f => f == '1');
                int countZero = value.Count(f => f == '0');

                if(countOne >= countZero)
                {
                    oxygen += "1";
                }
                else
                {
                    oxygen += "0";
                }

                reportValuesOxy = reportValuesOxy.Where(x => x.StartsWith(oxygen)).ToList();

                if (reportValuesOxy.Count == 1)
                {
                    oxygen = reportValuesOxy.First();
                    break;
                }
            }

            foreach (var value in arr)
            {
                int countOne = value.Count(f => f == '1');
                int countZero = value.Count(f => f == '0');

                if (countOne <= countZero)
                {
                    co2 += "0";
                }
                else
                {
                    co2 += "1";
                }

                reportValuesCo2 = reportValuesCo2.Where(x => x.StartsWith(co2)).ToList();

                if (reportValuesCo2.Count == 1)
                {
                    co2 = reportValuesCo2.First();
                    break;
                }
            }

            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }
        #endregion
    }
}