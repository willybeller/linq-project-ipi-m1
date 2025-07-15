using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleDataTool.Models;

namespace ConsoleDataTool.Services
{
    /// <summary>
    /// Service de traitement des données
    /// </summary>
    public class DataService
    {
        public List<DataRecord> Current { get; private set; }
        private readonly List<DataRecord> _initial;

        public DataService(List<DataRecord> initial)
        {
            _initial = initial;
            Current = new List<DataRecord>(_initial);
        }

        public void Filter()
        {
            Console.Write("Colonne pour filtrer: ");
            var col = Console.ReadLine();
            Console.Write("Valeur à rechercher: ");
            var val = Console.ReadLine();
            Console.Write("Type de recherche (1=exacte, 2=contient): ");
            var searchType = Console.ReadLine();
            
            if (string.IsNullOrEmpty(col) || string.IsNullOrEmpty(val))
            {
                Console.WriteLine("Ces champs ne peuvent pas être vides.");
                return;
            }
            
            if (searchType == "2")
            {
                Current = Current.Where(r => r.ContainsKey(col) && 
                    r[col]?.ToString()?.Contains(val, StringComparison.OrdinalIgnoreCase) == true).ToList();
                Console.WriteLine($"Filtrage appliqué sur {col} contenant '{val}'. {Current.Count} résultats.");
            }
            else
            {
                Current = Current.Where(r => r.ContainsKey(col) && r[col]?.ToString() == val).ToList();
                Console.WriteLine($"Filtrage appliqué sur {col} = {val}. {Current.Count} résultats.");
            }
        }

        public void Sort()
        {
            Console.Write("Colonne pour trier: ");
            var col = Console.ReadLine();
            Console.Write("Ordre (asc/desc): ");
            var ord = Console.ReadLine();
            
            if (string.IsNullOrEmpty(col))
            {
                Console.WriteLine("Ce champ ne peut pas être vide.");
                return;
            }
            
            // Détecter si la colonne contient des nombres
            bool isNumeric = Current.Where(r => r.ContainsKey(col) && r[col] != null)
                                   .All(r => double.TryParse(r[col].ToString(), out _));
            
            if (isNumeric)
            {
                // Tri numérique
                if (ord?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Current = Current.OrderByDescending(r => 
                    {
                        if (r.ContainsKey(col) && r[col] != null && double.TryParse(r[col].ToString(), out double value))
                            return value;
                        return double.MinValue;
                    }).ToList();
                }
                else
                {
                    Current = Current.OrderBy(r => 
                    {
                        if (r.ContainsKey(col) && r[col] != null && double.TryParse(r[col].ToString(), out double value))
                            return value;
                        return double.MaxValue;
                    }).ToList();
                }
                Console.WriteLine("Tri numérique appliqué.");
            }
            else
            {
                // Tri alphabétique
                if (ord?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true)
                    Current = Current.OrderByDescending(r => r.ContainsKey(col) ? r[col]?.ToString() : null).ToList();
                else
                    Current = Current.OrderBy(r => r.ContainsKey(col) ? r[col]?.ToString() : null).ToList();
                Console.WriteLine("Tri alphabétique appliqué.");
            }
        }

        public void Group()
        {
            Console.Write("Colonnes à grouper (séparées par virgule): ");
            var input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ce champ ne peut pas être vide.");
                return;
            }
            
            var cols = input.Split(',').Select(c => c.Trim()).ToArray();
            var groups = Current.GroupBy(r => string.Join("|", cols.Select(c => r.ContainsKey(c) ? r[c]?.ToString() : "")));
            var result = new List<DataRecord>();
            
            foreach (var g in groups)
            {
                var keys = g.Key.Split('|');
                var rec = new DataRecord();
                for (int i = 0; i < cols.Length; i++) rec[cols[i]] = keys[i];
                rec["Count"] = g.Count();
                result.Add(rec);
            }
            
            Current = result;
            Console.WriteLine($"Groupement appliqué sur [{string.Join(",", cols)}]. {Current.Count} groupes.");
        }

        public void SelectColumns()
        {
            Console.Write("Colonnes à afficher (séparées par virgule): ");
            var input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Ce champ ne peut pas être vide.");
                return;
            }
            
            var cols = input.Split(',').Select(c => c.Trim()).ToArray();
            Current = Current.Select(r => {
                var rec = new DataRecord();
                foreach (var c in cols)
                    if (r.ContainsKey(c)) rec[c] = r[c];
                return rec;
            }).ToList();
            Console.WriteLine("Projection appliquée.");
        }

        public void Preview(int count = 10)
        {
            if (!Current.Any())
            {
                Console.WriteLine("Aucun résultat à afficher.");
                return;
            }
            
            var cols = Current.First().Keys.ToList();
            var dataToShow = Current.Take(count).ToList();
            
            // Calculer la largeur optimale pour chaque colonne
            var columnWidths = new Dictionary<string, int>();
            
            foreach (var col in cols)
            {
                // Largeur minimale = nom de la colonne
                int maxWidth = col.Length;
                
                // Vérifier la largeur des valeurs dans cette colonne
                foreach (var row in dataToShow)
                {
                    var value = row.ContainsKey(col) ? row[col]?.ToString() ?? "" : "";
                    maxWidth = Math.Max(maxWidth, value.Length);
                }
                
                // Limiter la largeur maximale à 25 caractères pour éviter un affichage trop large
                columnWidths[col] = Math.Min(maxWidth, 25);
            }
            
            // Affichage header avec alignement
            var headerParts = cols.Select(col => col.PadRight(columnWidths[col])).ToArray();
            Console.WriteLine(string.Join(" | ", headerParts));
            
            // Ligne de séparation
            var separatorParts = cols.Select(col => new string('-', columnWidths[col])).ToArray();
            Console.WriteLine(string.Join("-+-", separatorParts));
            
            // Affichage des lignes avec alignement et troncature
            foreach (var row in dataToShow)
            {
                var valueParts = cols.Select(col =>
                {
                    var value = row.ContainsKey(col) ? row[col]?.ToString() ?? "" : "";
                    var width = columnWidths[col];
                    
                    // Tronquer si trop long et ajouter "..." pour indiquer la troncature
                    if (value.Length > width)
                    {
                        return width > 3 ? value.Substring(0, width - 3) + "..." : value.Substring(0, width);
                    }
                    
                    return value.PadRight(width);
                }).ToArray();
                
                Console.WriteLine(string.Join(" | ", valueParts));
            }
            
            Console.WriteLine($"\n({Current.Count} lignes au total, {count} affichées)");
        }
    }
}
