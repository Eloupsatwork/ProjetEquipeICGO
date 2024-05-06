using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class StageDAO
    {
        /// <summary>
        /// Ajouter un stage dans la table STAGE
        /// </summary>
        /// <param name="unStage">Un stage</param>
        public static void AjouterUnStage(dynamic unStage)
        {
            // Détermination du type de stage (étalé ou groupé) et exécution de la requête d'insertion
            string requete;
            if (unStage is StageEtale)
            {
                requete = "INSERT INTO STAGE VALUES ( '" + unStage.GetLaCompetence().GetCodeCompetence() + "', " + unStage.GetNumStage() + ", '" + unStage.GetNomStage() + "', " + unStage.GetDuree() + ", CAST(" + unStage.GetPrix() + " AS float), " + unStage.GetNbPlaces() + ", 'E', '" + unStage.GetJour() + "', NULL )";
                Connexion.ExecuterRequeteMaj(requete);
            }
            else
            {
                requete = "INSERT INTO STAGE VALUES ( '" + unStage.GetLaCompetence().GetCodeCompetence() + "', " + unStage.GetNumStage() + ", '" + unStage.GetNomStage() + "', " + unStage.GetDuree() + ", CAST(" + unStage.GetPrix() + " AS float), " + unStage.GetNbPlaces() + ", 'G', NULL , CAST(" + unStage.GetNbHeures() + " AS float))";
                Connexion.ExecuterRequeteMaj(requete);
            }
        }

        /// <summary>
        /// Charger les stages de la table STAGE dans une liste de stages
        /// </summary>
        /// <returns></returns>
        public static List<Stage> ChargerLesStages()
        {
            List<Stage> lesStages = new List<Stage>();

            Stage unStage;
            int numStage;
            int duree;
            string nbHeures;
            int nbPlaces;
            string prix;
            string nomStage;
            string jour;
            Competence uneCompetence;
            string codeCompetence;
            string typeStage;

            lesStages.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT CODECOMPETENCE, NUMSTAGE, INTITULESTAGE, DUREESTAGE, PRIXSTAGE, NOMBREPLACESSTAGE, TYPESTAGE, JOUR, NOMBREHEURESPARJOUR FROM STAGE";
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                // Récupération des caractéristiques d'une session à partir du résultat de la requête
                codeCompetence = uneLigne["CODECOMPETENCE"].ToString();
                numStage = int.Parse(uneLigne["NUMSTAGE"].ToString());
                nomStage = uneLigne["INTITULESTAGE"].ToString();
                duree = int.Parse(uneLigne["DUREESTAGE"].ToString());
                prix = uneLigne["PRIXSTAGE"].ToString();
                nbPlaces = int.Parse(uneLigne["NOMBREPLACESSTAGE"].ToString());
                typeStage = uneLigne["TYPESTAGE"].ToString();
                uneCompetence = CompetenceDAO.GetCompetence(codeCompetence);
                // Création de l'objet unStage en fonction de son type (étalé ou groupé) et ajout dans la liste des stages 
                if (typeStage.Equals("E"))
                {
                    jour = uneLigne["JOUR"].ToString();
                    unStage = new StageEtale(numStage, nomStage, duree, prix, nbPlaces, uneCompetence, ModuleDAO.ChargerLesModulesDuStage(codeCompetence, numStage), jour);
                }
                else
                {
                    nbHeures = uneLigne["NOMBREHEURESPARJOUR"].ToString();
                    unStage = new StageGroupe(numStage, nomStage, duree, prix, nbPlaces, uneCompetence, ModuleDAO.ChargerLesModulesDuStage(codeCompetence, numStage), nbHeures);
                }
                // Ajout du stage dans la liste lesStages
                lesStages.Add(unStage);
            }

            return lesStages;
        }

        /// <summary>
        /// Retourne un stage (étalé ou groupé) identifié dans la table STAGE
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <returns></returns>
        public static Stage GetStage(string idCompetence, int idStage)
        {
            Stage unStage;
            int duree;
            string nbHeures;
            int nbPlaces;
            string prix;
            string nomStage;
            string jour;
            Competence uneCompetence;
            String typeStage;

            // Recherche du stage dans la table STAGE
            string requete = "SELECT INTITULESTAGE, DUREESTAGE, PRIXSTAGE, NOMBREPLACESSTAGE, TYPESTAGE, JOUR, NOMBREHEURESPARJOUR FROM STAGE WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage;
            DataTable dt = Connexion.ExecuterRequete(requete);

            if (dt.Rows.Count == 1)
            {
                nomStage = dt.Rows[0]["INTITULESTAGE"].ToString();
                duree = int.Parse(dt.Rows[0]["DUREESTAGE"].ToString());
                prix = dt.Rows[0]["PRIXSTAGE"].ToString();
                nbPlaces = int.Parse(dt.Rows[0]["NOMBREPLACESSTAGE"].ToString());
                typeStage = dt.Rows[0]["TYPESTAGE"].ToString();
                uneCompetence = CompetenceDAO.GetCompetence(idCompetence);
                // Création de l'objet unStage en fonction de son type (étalé ou groupé) et ajout dans la liste des stages 
                if (typeStage.Equals("E"))
                {
                    jour = dt.Rows[0]["JOUR"].ToString();
                    unStage = new StageEtale(idStage, nomStage, duree, prix, nbPlaces, uneCompetence, ModuleDAO.ChargerLesModulesDuStage(idCompetence, idStage), jour);
                }
                else
                {
                    nbHeures = dt.Rows[0]["NOMBREHEURESPARJOUR"].ToString();
                    unStage = new StageGroupe(idStage, nomStage, duree, prix, nbPlaces, uneCompetence, ModuleDAO.ChargerLesModulesDuStage(idCompetence, idStage), nbHeures);
                }

            }
            else
            {
                unStage = new Stage();
            }

            return unStage;
        }

        /// <summary>
        /// Modifier les caractéristiques d'un stage identifié dans la table STAGE
        /// </summary>
        /// <param name="unStage">Un stage</param>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        public static void ModifierUnStage(dynamic unStage, string idCompetence, int idStage)
        {
            // Exécution de la requête de modification en fonction du type de stage (étalé ou groupé)
            string requete;
            if (unStage is StageEtale)
            {
                requete = "UPDATE STAGE SET CODECOMPETENCE = '" + unStage.GetLaCompetence().GetCodeCompetence() + "', NUMSTAGE = " + unStage.GetNumStage() + ", INTITULESTAGE = '" + unStage.GetNomStage() + "', DUREESTAGE = " + unStage.GetDuree() + ", PRIXSTAGE = CAST(" + unStage.GetPrix() + " AS float) , NOMBREPLACESSTAGE = " + unStage.GetNbPlaces() + ", TYPESTAGE = 'E', JOUR = '" + unStage.GetJour() + "', NOMBREHEURESPARJOUR = NULL WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage;
                Connexion.ExecuterRequeteMaj(requete);
            }
            else
            {
                requete = "UPDATE STAGE SET CODECOMPETENCE = '" + unStage.GetLaCompetence().GetCodeCompetence() + "', NUMSTAGE = " + unStage.GetNumStage() + ", INTITULESTAGE = '" + unStage.GetNomStage() + "', DUREESTAGE = " + unStage.GetDuree() + ", PRIXSTAGE = CAST(" + unStage.GetPrix() + " AS float) , NOMBREPLACESSTAGE = " + unStage.GetNbPlaces() + ", TYPESTAGE = 'G', JOUR = NULL , NOMBREHEURESPARJOUR = CAST(" + unStage.GetNbHeures() + " AS float) WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage;
                Connexion.ExecuterRequeteMaj(requete);
            }
        }

        /// <summary>
        /// Supprimer un stage identifié dans la table STAGE
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        public static void SupprimerUnStage(string idCompetence, int idStage)
        {
            // Exécution de la requête de suppression
            string requete = "DELETE FROM STAGE WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Ajouter un module à un stage dans la table COMPOSER
        /// </summary>
        /// <param name="unStage">Un stage</param>
        /// <param name="unModule">Un module</param>
        public static void AjouterUnModule(Stage unStage, Module unModule)
        {

            string requete = "INSERT INTO COMPOSER VALUES ('" + unStage.GetLaCompetence().GetCodeCompetence() + "', " + unStage.GetNumStage() + ", " + unModule.GetNumModule() + ")";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Supprimer un module à un stage dans la table COMPOSER
        /// </summary>
        /// <param name="unStage">Un stage</param>
        /// <param name="unModule">Un module</param>
        public static void SupprimerUnModule(Stage unStage, Module unModule)
        {

            // Exécution de la requête de suppression
            string requete = "DELETE FROM COMPOSER WHERE NUMMODULE = " + unModule.GetNumModule() + " AND NUMSTAGE = " + unStage.GetNumStage();
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les stages de la table COMPOSER d'un module identifié par son numéro
        /// </summary>
        /// <param name="idModule">Numéro module</param>
        /// <returns></returns>
        public static List<Stage> ChargerLesStagesDuModule(int idModule)
        {
            List<Stage> lesStages = new List<Stage>();
            bool ajouté;

            foreach (Stage unStage in ChargerLesStages())
            {
                ajouté = false;
                foreach(Module unModule in unStage.GetLesModules())
                {
                    if(unModule.GetNumModule() == idModule && ajouté == false)
                    {
                        lesStages.Add(unStage);
                        ajouté = true;
                    }
                }
            }

            return lesStages;
        }
    }
}
