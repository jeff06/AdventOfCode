using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Helper
    {
        public static List<string> GetFileContent(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath).ToList();
        }
    }
}
