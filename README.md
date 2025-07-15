
# Console DataSet ToolBox

  

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

![.NET](https://img.shields.io/badge/.NET-8.0-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

  

Une application console C# pour manipuler et analyser des jeux de données en utilisant LINQ. Prend en charge les formats CSV, JSON et XML avec des fonctionnalités avancées de filtrage, tri, groupement et export.

  

## 🚀 Fonctionnalités

  

### 📁 **Support Multi-format**

-  **CSV** : Chargement et export avec gestion des en-têtes

-  **JSON** : Parsing automatique des objets et tableaux

-  **XML** : Correction automatique des caractères non échappés

  

### 🔍 **Filtrage Intelligent**

-  **Recherche exacte** : Correspondance précise des valeurs

-  **Recherche partielle** : Contient un caractère ou une chaîne (ex: tous les champs contenant "a")

-  **Insensible à la casse** : Recherche flexible

  

### 📊 **Tri Avancé**

-  **Tri numérique** : Détection automatique des colonnes numériques

-  **Tri alphabétique** : Pour les données textuelles

-  **Ordre croissant/décroissant** : Contrôle de l'ordre

  

### 📈 **Analyse de Données**

-  **Groupement** : Par une ou plusieurs colonnes avec compteur

-  **Sélection de colonnes** : Projection de champs spécifiques

-  **Prévisualisation** : Affichage tabulaire formaté avec colonnes alignées

  

### 💾 **Export Flexible**

-  **Sauvegarde** dans tous les formats supportés

-  **Formatage** : JSON indenté, XML structuré, CSV standard

  

## 🏗️ Architecture

```

ConsoleDataSetToolBox/

├── Program.cs # Point d'entrée principal

├── Models/

│ └── DataRecord.cs # Modèle de données flexible

├── Interfaces/

│ ├── IDataLoader.cs # Contrat de chargement

│ └── IDataExporter.cs # Contrat d'export

├── Loaders/

│ ├── CsvLoader.cs # Chargement CSV

│ ├── JsonLoader.cs # Chargement JSON

│ └── XmlLoader.cs # Chargement XML avec auto-correction

├── Exporters/

│ ├── CsvExporter.cs # Export CSV

│ ├── JsonExporter.cs # Export JSON

│ └── XmlExporter.cs # Export XML

├── Services/

│ └── DataService.cs # Logique métier LINQ

└── Utils/

  └── Menu.cs # Interface utilisateur interactive

```  

## 📦 Installation

  

### 1. Cloner le projet

```bash

git  clone  https://github.com/willybeller/linq-project-ipi-m1.git

cd  linq-project-ipi-m1

```

  

### 2. Compiler l'application

```bash

dotnet  build

```

  

### 3. Exécuter l'application

```bash

dotnet  run  --project  ConsoleDataSetToolBox

```

## 📋 Fichiers de test inclus

  

Le projet inclut des fichiers de données d'exemple dans le dossier `DataSources/` :

  

-  **`movies.json`** : Base de données de films avec titre, année, rang, genre

-  **`series.csv`** : Informations sur les séries TV

-  **`musics.xml`** : Catalogue musical avec gestion des caractères spéciaux