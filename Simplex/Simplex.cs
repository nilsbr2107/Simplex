using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    class Simplex
    {
       private double[,] lhs;
       private double[] oFunction;
       private double[,] raw;
        private int pivotRow { get; set; }
        private int pivotColumn { get; set; }
            
       
       private double[] RHS { get; set; }
       private int RHSindex;
        public Simplex(double[,] array) {
            this.raw = array;
          }
     


        public void ausgebenRHS() { 
            foreach(int value in RHS)
            {
                Console.Write(value+"  ");

            } 
        }       
        public void ausgebenOFunction()
        {
            foreach (int value in oFunction)
            {
                Console.Write(value + "  ");

            }
        }

 
        public void transform()//Transformieren von den Daten(Minimierungsproblem
        {
            int breite, hoehe;
            double[,] save=new double[raw.GetLength(0), raw.GetLength(1)];
            double[,] save2 = new double[raw.GetLength(1), raw.GetLength(0)];
            breite = raw.GetLength(1);//Breite
            hoehe=raw.GetLength(0);//Höhe
            // Console.WriteLine(breite);
            // Console.WriteLine(hoehe);
            //Save werte zuweisen
            for (int i = 0; i <= hoehe - 1; i++)
            {
                for (int y = 0; y <= breite - 1; y++)
                {
                    save[i, y] = raw[i, y];
                    //this.setLHS(i, y, 0);
                   // lhs[i, y] = 0;
                }
            }
            Console.ReadLine();
            //raw Ausgeben
            for (int i = 0; i <= hoehe-1; i++)
            {
                for (int y = 0; y <= breite-1; y++)
                {
                    Console.Write(raw[i, y]+"  ");

                }
                Console.Write("  \n");
            }
            //Transformieren
            for (int i = 0; i < hoehe; i++)
            {
                for (int y = 0; y < breite ; y++)
                {
                   save2[y,i] = save[i,y];
                }
            }
            raw = save2;
            
            for (int i = 0; i <= save2.GetLength(0) - 1; i++)
            {
                for (int y = 0; y <= save2.GetLength(1) - 1; y++)
                {
                    Console.Write(save2[i,y]);

                }
                Console.Write("  \n");
            }
        }

        public void setup()//Alles herrichten für LHS mit Pivot und RHS Array Funktion
        {
            this.transform();

            RHSindex = raw.GetLength(1)-1;


            
            this.slipVariablesSetup();
            this.oFunction = new double[lhs.GetLength(1)];
            for (int i = 0; i < lhs.GetLength(1); i++)
            {
                oFunction[i] = lhs[lhs.GetLength(0) - 1, i];
            }
            this.RHSUpdate();
            this.oFunctionNegativ();
            this.ausgebenLHS();
           

        }
        public void oFunctionNegativ()//negieren für die Berechnung
        {
            for (int i = 0; i < oFunction.Length; i++)
            {

                    oFunction[i] = oFunction[i] * (-1);
                
            }
            for (int i = 0; i < lhs.GetLength(0) - 1; i++)
            {
                lhs[lhs.GetLength(0) - 1, i] = lhs[lhs.GetLength(0) - 1, i]*(-1);
            }
        }
        public void slipVariablesSetup() {
            this.lhs = new double[raw.GetLength(0), (raw.GetLength(0)+raw.GetLength(1)-1)];
            for (int i = 0; i < lhs.GetLength(0) ; i++)
            {
                for (int y = 0; y < lhs.GetLength(1) ; y++)
                {
                    lhs[i, y] = 0;

                }
            }
            for (int i = 0; i < raw.GetLength(0); i++)
            {
                for (int y = 0; y < raw.GetLength(1) ; y++)
                {
                    lhs[i, y] = raw[i, y];
                }
            }



            for (int i = 0; i < lhs.GetLength(0)-1; i++)
            {
                lhs[i, i + RHSindex+1] = 1;
            }



        }

        public void RHSUpdate()
        {
            this.RHS = new double[lhs.GetLength(0)-1];
           
            for (int i = 0; i < lhs.GetLength(0) - 1; i++)
            {
                RHS[i] = lhs[i, RHSindex];
            }



        }
        public void updateOFunction() {
            for (int i = 0; i < (lhs.GetLength(1)-1); i++)
            {
                oFunction[i] = lhs[lhs.GetLength(0) - 1, i];
            }
        }
        public void ausgebenLHS()
        {
            Console.Write("\n");
            for (int i = 0; i <= lhs.GetLength(0) - 1; i++)
            {
                for (int y = 0; y <= lhs.GetLength(1) - 1; y++)
                {
                    Console.Write(lhs[i,y].ToString("n2")+"    ");

                }
                Console.Write("  \n");
            }
        }
        public void selectPivotRow()
        {
            double save = 100000;
            pivotRow = 0;
            RHSUpdate();

            for(int i = 0; i < lhs.GetLength(0) -1; i++)
            {
                if ((RHS[i] / lhs[i, pivotColumn]) < save)
                {
                    save = RHS[i] / lhs[i, pivotColumn];
                    pivotRow = i;
                }

            }


        }
        public void selectPivotColumn()
        {
            double smallestValue = 0;
            updateOFunction();
            pivotColumn = 0;
            for (int i = 0; i < oFunction.GetLength(0); i++)
            {
                if ((i != RHSindex) && (oFunction[i] < smallestValue))
                {
                        smallestValue = oFunction[i];
                        //Console.WriteLine(smallestValue);
                        pivotColumn = i;
                    
                }
            }

        }
        public void calculate()
        {
            double[,] save = new double[lhs.GetLength(0), lhs.GetLength(1)];
            double multiplier;
            for (int i = 0; i < lhs.GetLength(0); i++)
            {
                for (int y = 0; y < lhs.GetLength(1); y++)
                {
                   save[i, y] =lhs[i, y];
                }
            }


            for (int i = 0; i < lhs.GetLength(0) ; i++)
            {
                for (int y = 0; y < lhs.GetLength(1) ; y++)
                {
                    if (i == pivotRow)
                    {
                        save[i, y] = lhs[pivotRow, y] / lhs[pivotRow, pivotColumn];

                    }
                    else
                    {
                        multiplier= lhs[i,pivotColumn]/ lhs[pivotRow, pivotColumn];
                        save[i, y] = lhs[i, y] - (multiplier * lhs[pivotRow, y]);

                    }
                }
            }

            for (int i = 0; i <lhs.GetLength(0); i++)
            {
                for (int y = 0; y < lhs.GetLength(1) ; y++)
                {
                    lhs[i, y] = save[i, y];
                }
            }
            
            updateOFunction();
            ausgebenLHS();
        }
        public Boolean checkNegativ()
        {
            updateOFunction();
            for(int i=0;i<oFunction.Length;i++)
            {
                

                if (oFunction[i] < 0)
                {


                    return true;
                }
            }

            return false;
        }
        public Boolean checkPivotColumn()
        {
            for (int i = 0; i < oFunction.GetLength(0) ; i++)
            {
                if (lhs[i, pivotColumn]<=0)
                {
                    return true;
                }

            }
            return false;
        }

        public void ausgabeLoesung()
        {
            int i = 0;
            Console.WriteLine("Minimal:" + lhs[lhs.GetLength(0) - 1, RHSindex ]);
            for (int y = RHSindex+1 ; y < (lhs.GetLength(1)); y++)
            {

                Console.WriteLine("X" + i + "=" + lhs[lhs.GetLength(0) - 1, y]);
                i++;
            }

        }

     }
}
