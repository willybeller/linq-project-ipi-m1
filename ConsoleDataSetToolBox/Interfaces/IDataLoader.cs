using System.Collections.Generic;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Interfaces
{
    /// <summary>
    /// Interface pour le chargement de données
    /// </summary>
    public interface IDataLoader
    {
        List<DataRecord> Load();
    }
}
