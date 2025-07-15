using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Exporters
{
    /// <summary>
    /// Exporteur de fichiers CSV
    /// </summary>
    public class CsvExporter : IDataExporter
    {
        private readonly string _path;

        public CsvExporter(string path) => _path = path;

        public void Export(List<DataRecord> data)
        {
            if (!data.Any()) return;
            
            var cols = data.First().Keys;
            var sb = new StringBuilder();
            sb.AppendLine(string.Join(",", cols));
            
            foreach (var r in data)
            {
                sb.AppendLine(string.Join(",", cols.Select(c => r[c]?.ToString())));
            }
            
            File.WriteAllText(_path, sb.ToString());
            Console.WriteLine($"Exporté CSV vers {_path}");
        }
    }
}
