using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleDataTool.Interfaces;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Loaders
{
    /// <summary>
    /// Chargeur de fichiers CSV
    /// </summary>
    public class CsvLoader : IDataLoader
    {
        private readonly string _path;

        public CsvLoader(string path) => _path = path;

        public List<DataRecord> Load()
        {
            var lines = File.ReadAllLines(_path);
            if (lines.Length < 2) return new List<DataRecord>();
            
            var headers = lines[0].Split(',');
            var list = new List<DataRecord>();
            
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var rec = new DataRecord();
                
                for (int i = 0; i < headers.Length; i++)
                {
                    rec[headers[i]] = parts.Length > i ? parts[i] : string.Empty;
                }
                
                list.Add(rec);
            }
            
            return list;
        }
    }
}
