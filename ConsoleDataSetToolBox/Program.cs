using System;
using ConsoleDataTool.Services;
using ConsoleDataTool.Utils;

namespace ConsoleDataTool
{
    /// <summary>
    /// Programme principal de l'application Console DataSet ToolBox
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Console DataSet ToolBox ===");
            
            try
            {
                // Choix du fichier d'entrée
                var loader = Menu.SelectLoader();
                var records = loader.Load();

                var dataService = new DataService(records);

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\nSélectionnez une opération :");
                    Console.WriteLine("1) Recherche (filtrage)");
                    Console.WriteLine("2) Tri");
                    Console.WriteLine("3) Groupement");
                    Console.WriteLine("4) Sélection de champs");
                    Console.WriteLine("5) Prévisualiser résultats");
                    Console.WriteLine("6) Exporter et quitter");
                    Console.WriteLine("7) Quitter sans sauvegarde");
                    Console.Write("Choix: ");
                    
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": dataService.Filter(); break;
                        case "2": dataService.Sort(); break;
                        case "3": dataService.Group(); break;
                        case "4": dataService.SelectColumns(); break;
                        case "5": dataService.Preview(); break;
                        case "6":
                            var exporter = Menu.SelectExporter(dataService.Current);
                            exporter.Export(dataService.Current);
                            exit = true;
                            break;
                        case "7": exit = true; break;
                        default: Console.WriteLine("Option invalide."); break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
            
            Console.WriteLine("Fin de l'application.");
        }
    }
}
