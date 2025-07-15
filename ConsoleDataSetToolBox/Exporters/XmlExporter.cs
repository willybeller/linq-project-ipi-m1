using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Exporters
{
    /// <summary>
    /// Exporteur de fichiers XML
    /// </summary>
    public class XmlExporter : IDataExporter
    {
        private readonly string _path;

        public XmlExporter(string path) => _path = path;

        public void Export(List<DataRecord> data)
        {
            var root = new XElement("Records",
                data.Select(r =>
                    new XElement("Record",
                        r.Select(kv => new XElement(kv.Key, kv.Value))
                    )
                )
            );
            
            var doc = new XDocument(root);
            doc.Save(_path);
            Console.WriteLine($"Exporté XML vers {_path}");
        }
    }
}
