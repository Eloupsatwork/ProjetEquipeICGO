using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class ModuleDAO
    {
        /// <summary>
        /// Ajouter un module dans la table MODULE
        /// </summary>
        /// <param name="unModule">Un module</param>
        public static void AjouterUnModule(Module unModule)
        {

            // Exécuter la requête d'insertion
            string requete = "INSERT INTO MODULE VALUES ( " + unModule.GetNumModule() + ", '" + unModule.GetNomModule() + "', '" + unModule.GetNomSupportCours() + "', '" + unModule.GetNomPresentation() + "', '" + unModule.GetPlaceSupportCours() + "', '" + unModule.GetPlacePresentation() + "')";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les modules de la table MODULE dans une liste de modules
        /// </summary>
        /// <returns></returns>
        public static List<Module> ChargerLesModules()
        {
            List<Module> lesModules = new List<Module>();
            Module unModule;
            int NumModule;
            string NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation;

            lesModules.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT NUMMODULE, NOMMODULE, NOMSUPPORTCOURS, NOMPRESENTATION, PLACESUPPORTCOURS, PLACEPRESENTATION FROM MODULE";
            DataTable dt = Connexion.ExecuterRequete(requete);

            // Parcours du résltat de la rêquete
            foreach (DataRow uneLigne in dt.Rows)
            {
                // Récupération des caractéristiques d'un formateur à partir du résultat de la requête
                NumModule = int.Parse(uneLigne["NUMMODULE"].ToString());
                NomModule = uneLigne["NOMMODULE"].ToString();
                NomSupportCours = uneLigne["NOMSUPPORTCOURS"].ToString();
                NomPresentation = uneLigne["NOMPRESENTATION"].ToString();
                PlaceSupportCours = uneLigne["PLACESUPPORTCOURS"].ToString();
                PlacePresentation = uneLigne["PLACEPRESENTATION"].ToString();
                // Construction de l'objet unModule avec chargement des compétences attribuées à ce formateur
                unModule = new Module(NumModule, NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation);
                // Ajout du module dans la liste unModule
                lesModules.Add(unModule);
            }


            return lesModules;
        }

        /// <summary>
        /// Retourne un module identifié par son numéro dans la table MODULE
        /// </summary>
        /// <param name="idModule">Numéro module</param>
        /// <returns></returns>
        public static Module GetModule(int idModule)
        {
            Module unModule;
            string NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation;

            // Exécuter la requête de sélection
            string requete = "SELECT NOMMODULE, NOMSUPPORTCOURS, NOMPRESENTATION, PLACESUPPORTCOURS, PLACEPRESENTATION FROM MODULE WHERE NUMMODULE = " + idModule;
            DataTable dt = Connexion.ExecuterRequete(requete);

            NomModule = dt.Rows[0]["NOMMODULE"].ToString();
            NomSupportCours = dt.Rows[0]["NOMSUPPORTCOURS"].ToString();
            NomPresentation = dt.Rows[0]["NOMPRESENTATION"].ToString();
            PlaceSupportCours = dt.Rows[0]["PLACESUPPORTCOURS"].ToString();
            PlacePresentation = dt.Rows[0]["PLACEPRESENTATION"].ToString();

            // Construction de l'objet unFormateur avec chargement des compétences attribuées à ce formateur
            unModule = new Module(idModule, NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation);

            return unModule;
        }

        /// <summary>
        /// Modifier les caractéristiques d'un module identifié par son numéro dans la table MODULE
        /// </summary>
        /// <param name="unModule">Un module</param>
        /// <param name="idModule">Numéro module</param>
        public static void ModifierUnModule(Module unModule, int idModule)
        {

            // Exécuter la requête de modification
            string requete = "UPDATE MODULE SET NOMMODULE = '" + unModule.GetNomModule() + "', NOMSUPPORTCOURS = '"+ unModule.GetNomSupportCours() +"', NOMPRESENTATION = '" + unModule.GetNomPresentation() +"', PLACESUPPORTCOURS = '" + unModule.GetPlaceSupportCours() + "', PLACEPRESENTATION = '" + unModule.GetPlacePresentation() + "' WHERE NUMMODULE = " + idModule;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Supprimer un module identifié par son numéro dans la table MODULE
        /// </summary>
        /// <param name="idModule">Numéro module</param>
        public static void SupprimerUnModule(int idModule)
        {
            // Exécution de la requête de la suppression
            string requete = "DELETE FROM MODULE WHERE NUMMODULE = " + idModule;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les modules de la table COMPOSER d'un stage identifié par son code compétence et numéro stage
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <returns></returns>
        public static List<Module> ChargerLesModulesDuStage(string idCompetence, int idStage)
        {
            List<Module> lesModules = new List<Module>();
            Module unModule;
            int NumModule;
            string NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation;

            lesModules.Clear();

            // recherche des module 
            string requete = "SELECT M.NUMMODULE, NOMMODULE, NOMSUPPORTCOURS, NOMPRESENTATION, PLACESUPPORTCOURS, PLACEPRESENTATION FROM MODULE AS M INNER JOIN COMPOSER AS C ON M.NUMMODULE = C.NUMMODULE WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage;
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                // Récupération des caractéristiques d'un formateur à partir du résultat de la requête
                NumModule = int.Parse(uneLigne["NUMMODULE"].ToString());
                NomModule = uneLigne["NOMMODULE"].ToString();
                NomSupportCours = uneLigne["NOMSUPPORTCOURS"].ToString();
                NomPresentation = uneLigne["NOMPRESENTATION"].ToString();
                PlaceSupportCours = uneLigne["PLACESUPPORTCOURS"].ToString();
                PlacePresentation = uneLigne["PLACEPRESENTATION"].ToString();
                // Construction de l'objet unModule avec chargement des compétences attribuées à ce formateur
                unModule = new Module(NumModule, NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation);
                // Ajout du module dans la liste unModule
                lesModules.Add(unModule);
            }

            return lesModules;

        }
    }
}
