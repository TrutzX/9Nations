using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Deklaration
            double value1, value2, value3, value4, value5, result;
            
            Console.WriteLine("Programm zur Berechnung des harmonischen Mittelwertes");

            // Eingabe der Werte durch den Benutzer und Umwandlung
            // Ohne Eingabeprüfung
            Console.WriteLine("1.Wert:");
            value1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("2.Wert:");
            value2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("3.Wert:");
            value3 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("4.Wert:");
            value4 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("5.Wert:");
            value5 = Convert.ToDouble(Console.ReadLine());
            
            // Verarbeitung
            result = 5 / (1 / value1 + 1 / value2 + 1 / value3 + 1 / value4 + 1 / value5);

            // Ausgabe
            Console.WriteLine("Das harmonische Mittel ist: {0}", result);

            Console.ReadKey();
        }
    }
}