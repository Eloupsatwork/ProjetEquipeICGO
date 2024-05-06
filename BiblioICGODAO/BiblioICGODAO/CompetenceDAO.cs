using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class CompetenceDAO
    {
        /// <summary>
        /// Ajouter une compétence dans la table COMPETENCE
        /// </summary>
        /// <param name="uneCompetence">Une compétence</param>
        public static void AjouterUneCompetence(Competence uneCompetence)
        {
            // Exécuter la requête d'insertion
            string requete = "INSERT INTO COMPETENCE VALUES ( '" + uneCompetence.GetCodeCompetence() + "', '" + uneCompetence.GetNomCompetence() + "')";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les compétences de la table COMPETENCE dans une liste de compétences
        /// </summary>
        /// <returns></returns>
        public static List<Competence> ChargerLesCompetences()
        {

            List<Competence> lesCompetences = new List<Competence>();
            Competence uneCompetence;
            string codeCompetence;
            string nomCompetence;

            lesCompetences.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT CODECOMPETENCE, LIBELLECOMPETENCE FROM COMPETENCE";
            DataTable dt = Connexion.ExecuterRequete(requete);

            // Parcours du résultat de la requête
            foreach (DataRow uneLigne in dt.Rows)
            {
                // Récupération des caractéristiques d'une compétence à partir du résultat de la requête
                codeCompetence = uneLigne["CODECOMPETENCE"].ToString();
                nomCompetence = uneLigne["LIBELLECOMPETENCE"].ToString();
                // Construction de l'objet uneCompetence avec chargement des formateurs ayant cette compétence
                uneCompetence = new Competence(codeCompetence, nomCompetence, FormateurDAO.ChargerLesFormateursCompetents(codeCompetence));
                // Ajout de la compétence dans la liste lesCompetences
                lesCompetences.Add(uneCompetence);
            }

            // Retour de la liste lesCompetences
            return lesCompetences;
        }

        /// <summary>
        /// Retourne une compétence identifiée par son code dans la table COMPETENCE
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <returns></returns>
        public static Competence GetCompetence(string idCompetence)
        {
            Competence uneCompetence;
            string nomCompetence;

            // Exécuter la requête de sélection
            string requete = "SELECT LIBELLECOMPETENCE FROM COMPETENCE WHERE CODECOMPETENCE = '" + idCompetence + "'";
            DataTable dt = Connexion.ExecuterRequete(requete);

            if (dt.Rows.Count == 1)
            {
                nomCompetence = dt.Rows[0]["LIBELLECOMPETENCE"].ToString();
                // Création de l'objet uneCompetence avec chargement des formateurs ayant cette compétence
                uneCompetence = new Competence(idCompetence, nomCompetence, FormateurDAO.ChargerLesFormateursCompetents(idCompetence));
            }
            else
            {
                uneCompetence = new Competence();
            }

            // Retour de la compétence uneCompetence
            return uneCompetence;
        }

        /// <summary>
        /// Modifier les caractéristiques d'une compétence identifiée par son code dans la table COMPETENCE 
        /// </summary>
        /// <param name="uneCompetence">Une Competence</param>
        /// <param name="idCompetence">Code compétence</param>
        public static void ModifierUneCompetence(Competence uneCompetence, string idCompetence)
        {
            // Exécuter la requête de modification
            string requete = "UPDATE COMPETENCE SET CODECOMPETENCE = '" + uneCompetence.GetCodeCompetence() + "', LIBELLECOMPETENCE = '" + uneCompetence.GetNomCompetence() + "' WHERE CODECOMPETENCE = '" + idCompetence + "'";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Supprimer une compétence identifiée par son code dans la table COMPETENCE
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        public static void SupprimerUneCompetence(string idCompetence)
        {
            // Exécuter la requête de suppression
            string requete = "DELETE FROM COMPETENCE WHERE CODECOMPETENCE = '" + idCompetence + "'";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les compétences de la table ETRE_COMPETENT d'un formateur identifié par son numéro
        /// </summary>
        /// <param name="idFormateur">Numéro formateur</param>
        /// <returns></returns>
        public static List<Competence> ChargerLesCompetencesDuFormateur(int idFormateur)
        {
            List<Competence> lesCompetences = new List<Competence>();
            Competence uneCompetence;
            string codeCompetence;
            string nomCompetence;

            // Recherche des compétences du formateur
            lesCompetences.Clear();

            string requete = "SELECT C.CODECOMPETENCE, LIBELLECOMPETENCE FROM COMPETENCE AS C INNER JOIN ETRE_COMPETENT AS E ON C.CODECOMPETENCE = E.CODECOMPETENCE WHERE NUMFORMATEUR = " + idFormateur;
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                codeCompetence = uneLigne["CODECOMPETENCE"].ToString();
                nomCompetence = uneLigne["LIBELLECOMPETENCE"].ToString();
                // Création de l'objet uneCompetence et ajout dans la liste des compétences 
                uneCompetence = new Competence(codeCompetence, nomCompetence);
                lesCompetences.Add(uneCompetence);
            }

            // Retour de la liste lesCompetences
            return lesCompetences;
        }

    }
}
