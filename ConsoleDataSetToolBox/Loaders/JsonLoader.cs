using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Loaders
{
    /// <summary>
    /// Chargeur de fichiers JSON
    /// </summary>
    public class JsonLoader : IDataLoader
    {
        private readonly string _path;

        public JsonLoader(string path) => _path = path;

        public List<DataRecord> Load()
        {
            var json = File.ReadAllText(_path);
            using var doc = JsonDocument.Parse(json);
            var list = new List<DataRecord>();
            
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in doc.RootElement.EnumerateArray())
                {
                    var rec = new DataRecord();
                    foreach (var prop in elem.EnumerateObject())
                    {
                        rec[prop.Name] = prop.Value.ToString();
                    }
                    list.Add(rec);
                }
            }
            
            return list;
        }
    }
}
