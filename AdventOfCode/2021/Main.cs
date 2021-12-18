using System;
using System.Collections.Generic;
using System.IO;
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
            List<string> lines = File.ReadAllLines(@"2021/Day1.txt").ToList();

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
            List<string> lines = File.ReadAllLines(@"2021/Day1.txt").ToList();
            int count = 0;
            int nbPerSliding = 3;
            int lastMeasurement = -1;
            // Initialize result

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
            string[] arr = {"", "", "", "", "", "", "", "", "", "", "", "",};
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
            string[] arr = {"", "", "", "", "", "", "", "", "", "", "", "",};

            InitArray(ref arr, reportValuesOxy);

            string oxygen = "";
            string co2 = "";
            int charToSkip = 0;

            while (reportValuesOxy.Count > 1)
            {
                int countOne = arr[charToSkip].Count(f => f == '1');
                int countZero = arr[charToSkip].Count(f => f == '0');

                if (countOne >= countZero)
                {
                    oxygen += "1";
                }
                else
                {
                    oxygen += "0";
                }

                reportValuesOxy = reportValuesOxy.Where(x => x.StartsWith(oxygen)).ToList();
                charToSkip++;

                arr = new[] {"", "", "", "", "", "", "", "", "", "", "", "",};

                InitArray(ref arr, reportValuesOxy);
            }

            oxygen = reportValuesOxy.First();

            arr = new [] {"", "", "", "", "", "", "", "", "", "", "", "",};
            charToSkip = 0;
            InitArray(ref arr, reportValuesCo2);

            while (reportValuesCo2.Count > 1)
            {
                int countOne = arr[charToSkip].Count(f => f == '1');
                int countZero = arr[charToSkip].Count(f => f == '0');

                if (countOne >= countZero)
                {
                    co2 += "0";
                }
                else
                {
                    co2 += "1";
                }

                reportValuesCo2 = reportValuesCo2.Where(x => x.StartsWith(co2)).ToList();
                charToSkip++;

                arr = new [] {"", "", "", "", "", "", "", "", "", "", "", "",};

                InitArray(ref arr, reportValuesCo2);
            }

            co2 = reportValuesCo2.First();

            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }

        private void InitArray(ref string[] arr, List<string> lst)
        {
            foreach (var value in lst)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    arr[i] += value[i];
                }
            }
        }

        #endregion

        #region Day 4

        public int Day4_1()
        {
            List<string> numberToDraw = File.ReadAllText("2021/Day4_numbers.txt").Split(",").ToList();
            List<string[,]> cards = ReadCardsFile();
            int rowLenght = 5;
            int colLenght = 5;

            foreach (var number in numberToDraw)
            {
                foreach (var card in cards)
                {
                    for (int j = 0; j < rowLenght; j++)
                    {
                        for (int k = 0; k < colLenght; k++)
                        {
                            if (card[j, k] == number)
                            {
                                card[j, k] = "*" + number;
                                var winningInfo = IsAWinningCard(card);

                                if (winningInfo.isWinning)
                                {
                                    return winningInfo.totalValueOfUnmarked * Convert.ToInt32(number);
                                }
                            }
                        }
                    }
                }
            }

            return -1;
        }

        public int Day4_2()
        {
            List<string> numberToDraw = File.ReadAllText("2021/Day4_numbers.txt").Split(",").ToList();
            List<string[,]> cards = ReadCardsFile();
            int rowLenght = 5;
            int colLenght = 5;
            List<int> bordThatAlreadyWon = new List<int>();
            int lastWinningBord = 0;

            foreach (var number in numberToDraw)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (!bordThatAlreadyWon.Contains(i))
                    {
                        for (int j = 0; j < rowLenght; j++)
                        {
                            for (int k = 0; k < colLenght; k++)
                            {
                                if (cards[i][j, k] == number)
                                {
                                    cards[i][j, k] = "*" + number;
                                    var winningInfo = IsAWinningCard(cards[i]);

                                    if (winningInfo.isWinning)
                                    {
                                        bordThatAlreadyWon.Add(i);
                                        lastWinningBord = winningInfo.totalValueOfUnmarked * Convert.ToInt32(number);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return lastWinningBord;
        }

        private (bool isWinning, int totalValueOfUnmarked) IsAWinningCard(string[,] card)
        {
            int rowLenght = 5;
            int colLenght = 5;
            int starCount;
            bool isAWinningCard = false;
            int totalValueOfUnmarked = 0;

            //Is it a winning row
            for (int j = 0; j < rowLenght; j++)
            {
                starCount = 0;
                for (int k = 0; k < colLenght; k++)
                {
                    string slotValue = card[j, k];
                    if (card[j, k].Contains("*"))
                    {
                        if (starCount < 5)
                        {
                            starCount++;
                        }

                        if (starCount == 5)
                        {
                            isAWinningCard = true;
                        }
                    }
                    else
                    {
                        totalValueOfUnmarked += Convert.ToInt32(slotValue);
                    }
                }
            }

            totalValueOfUnmarked = 0;
            //Is it a winning col
            for (int j = 0; j < colLenght; j++)
            {
                starCount = 0;
                for (int k = 0; k < rowLenght; k++)
                {
                    string slotValue = card[k, j];
                    if (card[k, j].Contains("*"))
                    {
                        if (starCount < 5)
                        {
                            starCount++;
                        }

                        if (starCount == 5)
                        {
                            isAWinningCard = true;
                        }
                    }
                    else
                    {
                        totalValueOfUnmarked += Convert.ToInt32(slotValue);
                    }
                }
            }

            if (!isAWinningCard)
            {
                totalValueOfUnmarked = -1;
            }

            return (isAWinningCard, totalValueOfUnmarked);
        }

        private List<string[,]> ReadCardsFile()
        {
            List<string[,]> returnArrayList = new List<string[,]>();

            int rowCount = 0;
            string[,] result = new string[5, 5];
            foreach (string row in File.ReadAllText("2021/Day4_cards.txt").Split('\n'))
            {
                if (row != "\r")
                {
                    var rowPositionCount = 0;
                    foreach (var col in row.Trim().Split(' '))
                    {
                        if (col != "")
                        {
                            result[rowCount, rowPositionCount] = col;
                            rowPositionCount++;
                        }
                    }

                    rowCount++;
                }
                else
                {
                    rowCount = 0;
                    returnArrayList.Add(result);
                    result = new string[5, 5];
                }
            }

            returnArrayList.Add(result);

            return returnArrayList;
        }

        #endregion

        #region Day 5

        public int Day5_1()
        {
            //Load fichier
            List<string> fileEntry = Helper.GetFileContent("2021/Day5.txt");
            int maxX = 0;
            int maxY = 0;
            int numberOfDangerous = 0;

            foreach (var v in fileEntry)
            {
                //0,9 -> 5,9
                //x,y
                string[] splitedLine = v.Split(" ");
                string setOfCoords1 = splitedLine[0];
                string setOfCoords2 = splitedLine[2];

                string[] coords1 = setOfCoords1.Split(",");
                string[] coords2 = setOfCoords2.Split(",");

                //trouver max X
                //trouver max Y
                if (Convert.ToInt32(coords1[0]) > maxX)
                {
                    maxX = Convert.ToInt32(coords1[0]);
                }

                if (Convert.ToInt32(coords1[1]) > maxY)
                {
                    maxY = Convert.ToInt32(coords1[1]);
                }

                if (Convert.ToInt32(coords2[0]) > maxX)
                {
                    maxX = Convert.ToInt32(coords2[0]);
                }

                if (Convert.ToInt32(coords2[1]) > maxY)
                {
                    maxY = Convert.ToInt32(coords2[1]);
                }
            }

            //créer un tableau de dimension x,y
            int[,] map = new int[maxX + 1, maxY + 1];

            foreach (var v in fileEntry)
            {
                string[] splitedLine = v.Split(" ");
                string setOfCoords1 = splitedLine[0];
                string setOfCoords2 = splitedLine[2];

                string[] coords1 = setOfCoords1.Split(",");
                string[] coords2 = setOfCoords2.Split(",");

                //Prendre coords x1,x2
                int x1 = Convert.ToInt32(coords1[0]);
                int x2 = Convert.ToInt32(coords2[0]);
                int y1 = Convert.ToInt32(coords1[1]);
                int y2 = Convert.ToInt32(coords2[1]);

                //trouver le plus grand des deux pour déterminer dans quel direction
                //faire +1 dans le tableau sur les position
                if (y1 == y2)
                {
                    if (x1 > x2)
                    {
                        for (int i = x2; i <= x1; i++)
                        {
                            map[i, y1]++;
                        }
                    }
                    else
                    {
                        for (int i = x1; i <= x2; i++)
                        {
                            map[i, y1]++;
                        }
                    }
                }
                else if (x1 == x2)
                {
                    if (y1 > y2)
                    {
                        for (int i = y2; i <= y1; i++)
                        {
                            map[x1, i]++;
                        }
                    }
                    else
                    {
                        for (int i = y1; i <= y2; i++)
                        {
                            map[x1, i]++;
                        }
                    }
                }
            }

            foreach (var v in map)
            {
                if (v >= 2)
                {
                    numberOfDangerous++;
                }
            }

            //Scanner tout le tableau pour trouver le nombre de case qui ont un chiffre plus gros que 2

            return numberOfDangerous;
        }

        public int Day5_2()
        {
            //Load fichier
            List<string> fileEntry = Helper.GetFileContent("2021/Day5.txt");
            int maxX = 0;
            int maxY = 0;
            int numberOfDangerous = 0;

            foreach (var v in fileEntry)
            {
                //0,9 -> 5,9
                //x,y
                string[] splitedLine = v.Split(" ");
                string setOfCoords1 = splitedLine[0];
                string setOfCoords2 = splitedLine[2];

                string[] coords1 = setOfCoords1.Split(",");
                string[] coords2 = setOfCoords2.Split(",");

                //trouver max X
                //trouver max Y
                if (Convert.ToInt32(coords1[0]) > maxX)
                {
                    maxX = Convert.ToInt32(coords1[0]);
                }

                if (Convert.ToInt32(coords1[1]) > maxY)
                {
                    maxY = Convert.ToInt32(coords1[1]);
                }

                if (Convert.ToInt32(coords2[0]) > maxX)
                {
                    maxX = Convert.ToInt32(coords2[0]);
                }

                if (Convert.ToInt32(coords2[1]) > maxY)
                {
                    maxY = Convert.ToInt32(coords2[1]);
                }
            }

            //créer un tableau de dimension x,y
            int[,] map = new int[maxX + 1, maxY + 1];

            foreach (var v in fileEntry)
            {
                string[] splitedLine = v.Split(" ");
                string setOfCoords1 = splitedLine[0];
                string setOfCoords2 = splitedLine[2];

                string[] coords1 = setOfCoords1.Split(",");
                string[] coords2 = setOfCoords2.Split(",");

                //Prendre coords x1,x2
                int x1 = Convert.ToInt32(coords1[0]);
                int x2 = Convert.ToInt32(coords2[0]);
                int y1 = Convert.ToInt32(coords1[1]);
                int y2 = Convert.ToInt32(coords2[1]);

                //trouver le plus grand des deux pour déterminer dans quel direction
                //faire +1 dans le tableau sur les position
                if (y1 == y2)
                {
                    if (x1 > x2)
                    {
                        for (int i = x2; i <= x1; i++)
                        {
                            map[i, y1]++;
                        }
                    }
                    else
                    {
                        for (int i = x1; i <= x2; i++)
                        {
                            map[i, y1]++;
                        }
                    }
                }
                else if (x1 == x2)
                {
                    if (y1 > y2)
                    {
                        for (int i = y2; i <= y1; i++)
                        {
                            map[x1, i]++;
                        }
                    }
                    else
                    {
                        for (int i = y1; i <= y2; i++)
                        {
                            map[x1, i]++;
                        }
                    }
                }
                else
                {
                    //Est une diagonale
                    //Trouver la direction de la diagonale

                    int spaceVisited = 0;
                    if (x1 > x2 && y1 > y2)
                    {
                        for (int i = x2; i <= x1; i++)
                        {
                            map[i, y2 + spaceVisited]++;
                            spaceVisited++;
                        }
                    }
                    else if (x1 > x2 && y1 < y2)
                    {
                        for (int i = x1; i >= x2; i--)
                        {
                            map[i, y1 + spaceVisited]++;
                            spaceVisited++;
                        }
                    }
                    else if (x1 < x2 && y1 > y2)
                    {
                        for (int i = x1; i <= x2; i++)
                        {
                            map[i, y1 - spaceVisited]++;
                            spaceVisited++;
                        }
                    }
                    //x-,y- ⬊
                    //0,0
                    //8,8
                    else if (x1 < x2 && y1 < y2)
                    {
                        for (int i = x2; i >= x1; i--)
                        {
                            map[i, y2 - spaceVisited]++;
                            spaceVisited++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Impossible case");
                    }
                }
            }

            foreach (var v in map)
            {
                if (v >= 2)
                {
                    numberOfDangerous++;
                }
            }

            //Scanner tout le tableau pour trouver le nombre de case qui ont un chiffre plus gros que 2

            return numberOfDangerous;
        }

        #endregion

        #region Day 6

        private class LanternFish
        {
            public double NbFish { get; set; }
            public double NbDay { get; init; }
        }

        public double Day6_1()
        {
            return CalculateNbFishBasedOnNbDay(80);
        }
        
        public double Day6_2()
        {
            return CalculateNbFishBasedOnNbDay(256);
        }

        private double CalculateNbFishBasedOnNbDay(int numberOfDay)
        {
            int maxFishCycle = 8;
            double totalFish = 0;
            List<string> fileContent = Helper.GetFileContent("2021/Day6.txt");
            List<string> fishFromFile = fileContent[0].Split(",").OrderBy(x => x).ToList();
            List<LanternFish> lstFish = new List<LanternFish>();

            for (int i = 0; i < maxFishCycle + 1; i++)
            {
                LanternFish fish = new LanternFish
                {
                    NbDay = i,
                    NbFish = 0
                };
                lstFish.Add(fish);
            }

            foreach (var v in lstFish)
            {
                v.NbFish = fishFromFile.Count(x => x == v.NbDay.ToString());
            }

            for (int i = 0; i < numberOfDay; i++)
            {
                lstFish = SpawnFish(lstFish, maxFishCycle);
            }

            foreach (var v in lstFish)
            {
                totalFish += v.NbFish;
            }

            return totalFish;
        }

        private List<LanternFish> SpawnFish(List<LanternFish> lstFish, int maxFishCycle)
        {
            List<LanternFish> lstFishBackup = new List<LanternFish>();
            for (int i = 0; i <= maxFishCycle; i++)
            {
                lstFishBackup.Add(new LanternFish(){NbDay = i});
            }

            for (int i = maxFishCycle; i >= 0; i--)
            {
                if (i == 0)
                {
                    //Each day, a 0 becomes a 6 and adds a new 8 to the end of the list, while each other number decreases by 1 if it was present at the start of the day.
                    double currentNbFish = lstFish[i].NbFish;
                    lstFishBackup[6].NbFish += currentNbFish;
                    lstFishBackup[maxFishCycle].NbFish += currentNbFish;
                }
                else
                {
                    lstFishBackup[i - 1].NbFish = lstFish[i].NbFish;
                }
            }

            return lstFishBackup;
        }

        #endregion

        #region Day 7

        

        #endregion
    }
}