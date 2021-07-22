using System;

namespace Simplex
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Dateiname mit Dateiendung eingeben");
            Parser parser = new Parser();
            parser.Auswaehlen(Console.ReadLine());
            Console.WriteLine($"{parser.path}");
            parser.Einlesen();
            parser.bearbeiten();
            parser.Ausgabe();
           
                     
           Simplex SimplexCalculator = new Simplex(parser.to2DArray());
           SimplexCalculator.setup();
           while(SimplexCalculator.checkNegativ()==true){

                SimplexCalculator.selectPivotColumn();

                if (SimplexCalculator.checkPivotColumn()) {
                    SimplexCalculator.selectPivotRow();
                    SimplexCalculator.calculate();
                   


                }
                else
                {
                    Console.WriteLine("Es gibt unendlich viele Lösungen");
                    break;
                }
                
            }
            
            SimplexCalculator.ausgebenLHS();
            SimplexCalculator.updateOFunction();
            SimplexCalculator.ausgabeLoesung();
            Console.ReadLine();

        }
    }
}
