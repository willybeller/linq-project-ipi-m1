using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Exporters
{
    /// <summary>
    /// Exporteur de fichiers JSON
    /// </summary>
    public class JsonExporter : IDataExporter
    {
        private readonly string _path;

        public JsonExporter(string path) => _path = path;

        public void Export(List<DataRecord> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_path, json);
            Console.WriteLine($"Exporté JSON vers {_path}");
        }
    }
}
