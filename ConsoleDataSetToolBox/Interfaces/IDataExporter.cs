using System.Collections.Generic;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Interfaces
{
    /// <summary>
    /// Interface pour l'export de données
    /// </summary>
    public interface IDataExporter
    {
        void Export(List<DataRecord> data);
    }
}
