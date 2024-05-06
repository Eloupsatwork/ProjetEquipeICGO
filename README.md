# Procédure d'installation
## Prérequis
- Microsoft Visual Studio installé sur votre poste
- SQL Server Management Studio
## Installation de la base de donnée
Connectez-vous sur SQL Server Management Studio, puis exécutez le script SQL suivant :  
```
scriptDB_ICGO_ETTBS.sql  
```
Ensuite, vous devez lier votre base de donnée à l'application en modifiant la ligne suivante dans le fichier connexion.cs du dossier BiblioICGODAO:  
```
35. connexion = new SqlConnection(@"Data Source={votre_serveur};Initial Catalog={nom_DB};Integrated Security=True");
```
## Installation de l'application
Vous pouvez simplement exécuter le programme C# et commencer à utiliser l'application
