using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simplex
{
    class Parser
    {
        private List<string> Everything;
        private List<List<string>> converted;
        private List<int> values=new List<int>();
        private double[,] sorted;
        public Parser()
        {
            Everything = new List<string>();
            values = new List<int>();
            converted =new List<List<string>>();
           

        }
        private string _path="";
        public string path
        {
            get { return _path; }
        }
        public void Einlesen()
        {
            Everything=File.ReadAllLines(path).ToList();
                  
        }
        public void bearbeiten()
        {
            
            Everything.RemoveAt(0);
            Everything.RemoveAt(1);
            char[] remove = new char[] { ' ', '+', '*', '>', '=', ':',';' };
            string[] allLines = new string[Everything.Count];
         
            
            allLines=Everything.ToArray();
            allLines[0]=allLines[0].Remove(0,3);
            for (int i = 0; i < allLines.Length; i++)
            {
                allLines[i] = Regex.Replace(allLines[i], @"x...", "    ");
                allLines[i] = Regex.Replace(allLines[i], @"x.*?;", "    ");//Aufgrund des Sonderfall bei der Objectiv Funktion
                converted.Add(allLines[i].Split(remove,StringSplitOptions.RemoveEmptyEntries).ToList());

            }


            converted[0].Add("0");
           // Console.WriteLine(converted[0].Count());
           // Console.WriteLine(converted.Count());
            //Test
            foreach (var line in converted)
            {
                foreach (var zahl in line)
                {
                    //Console.Write(zahl + "#");
                    values.Add(int.Parse(zahl));
                }

            }

            Console.Write("\n");
         }
        public double[,] to2DArray()
        {
            int[] converter = values.ToArray();
            sorted= new double[converted.Count(), converted[0].Count()];
            int max_items = 0;
           for(int i=0;i<= converted.Count()-1; i++)
            {
                for(int y = 0; y <= converted[0].Count()-1; y++)
                {

                    sorted[i, y] = converter[max_items];
                    max_items++;
                    
                }

            }

           double[,] save = new double[sorted.GetLength(0), sorted.GetLength(1)];

            for (int i = 0; i < sorted.GetLength(0) ; i++)
            {
                for (int y = 0; y < sorted.GetLength(1) ; y++)
                {
                    save[i, y] = sorted[i, y];

                }
            }
            for (int i = 0; i < sorted.GetLength(0)-1; i++)
            {
                for (int y = 0; y < sorted.GetLength(1); y++)
                {
                    sorted[i, y] = save[i+1, y];

                }
            }
            for (int y = 0; y < sorted.GetLength(1); y++)
            {
                sorted[save.GetLength(0) - 1, y] = save[0, y];
            }

            return sorted;
        }
        public void Ausgabe()
        {
            //int index = 0;
            foreach(string line in Everything)
            {

                Console.WriteLine(line);/*+ index);
                index++;*/
            }

        }
        public void Auswaehlen(string value)
        {
            _path = value;

        }
    }
}
