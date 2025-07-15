using System;
using System.Collections.Generic;
using System.IO;
using ConsoleDataTool.Exporters;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Loaders;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Utils
{
    /// <summary>
    /// Menu interactif pour l'application
    /// </summary>
    public static class Menu
    {
        public static IDataLoader SelectLoader()
        {
            while (true)
            {
                Console.Write("Chemin du fichier à charger: ");
                var path = Console.ReadLine();
                
                if (!File.Exists(path)) 
                { 
                    Console.WriteLine("Fichier introuvable."); 
                    continue; 
                }
                
                var ext = Path.GetExtension(path).ToLower();
                return ext switch
                {
                    ".csv" => new CsvLoader(path),
                    ".json" => new JsonLoader(path),
                    ".xml" => new XmlLoader(path),
                    _ => throw new NotSupportedException("Format non supporté."),
                };
            }
        }

        public static IDataExporter SelectExporter(List<DataRecord> data)
        {
            while (true)
            {
                Console.Write("Chemin de sortie (avec extension .csv/.json/.xml): ");
                var path = Console.ReadLine();
                
                if (string.IsNullOrEmpty(path))
                {
                    Console.WriteLine("Ce champ ne peut pas être vide.");
                    continue;
                }
                
                var ext = Path.GetExtension(path)?.ToLower();
                if (data == null) 
                { 
                    Console.WriteLine("Aucune donnée."); 
                    continue; 
                }
                
                return ext switch
                {
                    ".csv" => new CsvExporter(path),
                    ".json" => new JsonExporter(path),
                    ".xml" => new XmlExporter(path),
                    _ => throw new NotSupportedException("Format d'export non supporté."),
                };
            }
        }
    }
}
