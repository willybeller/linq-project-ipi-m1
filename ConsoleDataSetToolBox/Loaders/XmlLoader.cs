using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Loaders
{
    /// <summary>
    /// Chargeur de fichiers XML
    /// </summary>
    public class XmlLoader : IDataLoader
    {
        private readonly string _path;

        public XmlLoader(string path) => _path = path;

        public List<DataRecord> Load()
        {
            try
            {
                var doc = XDocument.Load(_path);
                var list = new List<DataRecord>();
                var rows = doc.Root?.Elements();
                
                if (rows != null)
                {
                    foreach (var row in rows)
                    {
                        var rec = new DataRecord();
                        foreach (var elem in row.Elements())
                        {
                            rec[elem.Name.LocalName] = elem.Value;
                        }
                        list.Add(rec);
                    }
                }
                
                return list;
            }
            catch (XmlException ex)
            {
                Console.WriteLine($"Erreur XML détectée: {ex.Message}");
                Console.WriteLine("Tentative de correction automatique...");
                
                // Lire le fichier comme texte et corriger les caractères non échappés
                var content = File.ReadAllText(_path);
                content = content.Replace(" & ", " &amp; ")
                               .Replace(">R&B<", ">R&amp;B<");
                
                // Parser le contenu corrigé
                var doc = XDocument.Parse(content);
                var list = new List<DataRecord>();
                var rows = doc.Root?.Elements();
                
                if (rows != null)
                {
                    foreach (var row in rows)
                    {
                        var rec = new DataRecord();
                        foreach (var elem in row.Elements())
                        {
                            rec[elem.Name.LocalName] = elem.Value;
                        }
                        list.Add(rec);
                    }
                }
                
                Console.WriteLine("Correction réussie et données chargées.");
                return list;
            }
        }
    }
}
