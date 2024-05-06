using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class StagiaireDAO
    {
        /// <summary>
        /// Ajouter un stagiaire dans la table STAGIAIRE
        /// </summary>
        /// <param name="unStagiaire">Un stagiaire</param>
        public static void AjouterUnStagiaire(Stagiaire unStagiaire)
        {
            // Exécution de la requête d'insertion
            string requete = "INSERT INTO Stagiaire VALUES ( " + unStagiaire.GetNumStagiaire() + ", '" + unStagiaire.GetNomStagiaire() + "', '" + unStagiaire.GetPrenom() + "', '" + unStagiaire.GetRue() + "', '" + unStagiaire.GetCodePostal() + "', '" + unStagiaire.GetVille() + "', '" + unStagiaire.GetTelephone() + "')";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les stagiaires de la table STAGIAIRE dans une liste de stagiaires
        /// </summary>
        /// <returns></returns>
        public static List<Stagiaire> ChargerLesStagiaires()
        {

            List<Stagiaire> lesStagiaires = new List<Stagiaire>();
            Stagiaire unStagiaire;
            int numStagiaire;
            string nomStagiaire, prenomStagiaire;
            string rue, codePostal, ville;
            string telephone;

            // Exécuter la requête de sélection
            string requete = "SELECT NUMSTAGIAIRE, NOMSTAGIAIRE, PRENOMSTAGIAIRE, RUESTAGIAIRE, CODEPOSTALSTAGIAIRE, VILLESTAGIAIRE, TELEPHONESTAGIAIRE FROM STAGIAIRE";
            DataTable dt = Connexion.ExecuterRequete(requete);

            // Parcours du résultat de la requête
            foreach (DataRow uneLigne in dt.Rows)
            {
                // Récupération des caractéristiques d'un stagiaire à partir du résultat de la requête
                numStagiaire = int.Parse(uneLigne["NUMSTAGIAIRE"].ToString());
                nomStagiaire = uneLigne["NOMSTAGIAIRE"].ToString();
                prenomStagiaire = uneLigne["PRENOMSTAGIAIRE"].ToString();
                rue = uneLigne["RUESTAGIAIRE"].ToString();
                codePostal = uneLigne["CODEPOSTALSTAGIAIRE"].ToString();
                ville = uneLigne["VILLESTAGIAIRE"].ToString();
                telephone = uneLigne["TELEPHONESTAGIAIRE"].ToString();
                // Construction de l'objet unStagiaire avec chargement des sessions auxquelles ce stagiaire est inscrit
                unStagiaire = new Stagiaire(numStagiaire, nomStagiaire, prenomStagiaire, rue, codePostal, ville, telephone, SessionDAO.ChargerLesSessionsDuStagiaire(numStagiaire));
                // Ajout du stagiaire dans la liste lesStagiaires
                lesStagiaires.Add(unStagiaire);
            }

            return lesStagiaires;
        }

        /// <summary>
        /// Retourne un stagiaire identifié par son numéro dans la table STAGIAIRE
        /// </summary>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        /// <returns></returns>
        public static Stagiaire GetStagiaire(int idStagiaire)
        {
            Stagiaire unStagiaire;
            string nomStagiaire, prenomStagiaire;
            string rue, codePostal, ville;
            string telephone;

            // Recherche du stagiaire dans la table STAGIAIRE
            string requete = "SELECT NOMSTAGIAIRE, PRENOMSTAGIAIRE, RUESTAGIAIRE, CODEPOSTALSTAGIAIRE, VILLESTAGIAIRE, TELEPHONESTAGIAIRE FROM STAGIAIRE WHERE NUMSTAGIAIRE = " + idStagiaire;
            DataTable dt = Connexion.ExecuterRequete(requete);

            if (dt.Rows.Count == 1)
            {
                nomStagiaire = dt.Rows[0]["NOMSTAGIAIRE"].ToString();
                prenomStagiaire = dt.Rows[0]["PRENOMSTAGIAIRE"].ToString();
                rue = dt.Rows[0]["RUESTAGIAIRE"].ToString();
                codePostal = dt.Rows[0]["CODEPOSTALSTAGIAIRE"].ToString();
                ville = dt.Rows[0]["VILLESTAGIAIRE"].ToString();
                telephone = dt.Rows[0]["TELEPHONESTAGIAIRE"].ToString();
                // Construction de l'objet unStagiaire avec chargement des sessions auxquelles ce stagiaire est inscrit
                unStagiaire = new Stagiaire(idStagiaire, nomStagiaire, prenomStagiaire, rue, codePostal, ville, telephone, SessionDAO.ChargerLesSessionsDuStagiaire(idStagiaire));
            }
            else
            {
                unStagiaire = new Stagiaire();
            }

            return unStagiaire;
        }

        /// <summary>
        /// Modifier les caractéristiques d'un stagiaire identifié par son numéro dans la table STAGIAIRE
        /// </summary>
        /// <param name="unStagiaire">Un stagiaire</param>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        public static void ModifierUnStagiaire(Stagiaire unStagiaire, int idStagiaire)
        {
            // Exécution de la requête de modification
            string requete = "UPDATE STAGIAIRE SET NUMSTAGIAIRE = " + unStagiaire.GetNumStagiaire() + ", NOMSTAGIAIRE = '" + unStagiaire.GetNomStagiaire() + "', PRENOMSTAGIAIRE = '" + unStagiaire.GetPrenom() + "', RUESTAGIAIRE = '" + unStagiaire.GetRue() + "', CODEPOSTALSTAGIAIRE = '" + unStagiaire.GetCodePostal() + "', VILLESTAGIAIRE = '" + unStagiaire.GetVille() + "', TELEPHONESTAGIAIRE = '" + unStagiaire.GetTelephone() + "' WHERE NUMSTAGIAIRE = " + idStagiaire;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Supprimer un stagiaire identifié par son numéro dans la table STAGIAIRE
        /// </summary>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        public static void SupprimerUnStagiaire(int idStagiaire)
        {
            // Exécution de la requête de suppression
            string requete = "DELETE FROM STAGIAIRE WHERE NUMSTAGIAIRE = " + idStagiaire;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les stagiaires de la table INSCRIRE d'une session identifiée
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <param name="idSession">Numéro session</param>
        /// <returns></returns>
        public static List<Stagiaire> ChargerLesStagiairesDeLaSession(string idCompetence, int idStage, int idSession)
        {

            List<Stagiaire> lesStagiaires = new List<Stagiaire>();
            Stagiaire unStagiaire;
            int numStagiaire;
            string nomStagiaire, prenomStagiaire;
            string rue, codePostal, ville;
            string telephone;

            lesStagiaires.Clear();

            // Recherche des stagiaires de la session
            string requete = "SELECT S.NUMSTAGIAIRE, NOMSTAGIAIRE, PRENOMSTAGIAIRE, RUESTAGIAIRE, CODEPOSTALSTAGIAIRE, VILLESTAGIAIRE, TELEPHONESTAGIAIRE FROM STAGIAIRE AS S INNER JOIN INSCRIRE AS I ON S.NUMSTAGIAIRE = I.NUMSTAGIAIRE WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage + " AND NUMSESSION = " + idSession;
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                numStagiaire = int.Parse(uneLigne["NUMSTAGIAIRE"].ToString());
                nomStagiaire = uneLigne["NOMSTAGIAIRE"].ToString();
                prenomStagiaire = uneLigne["PRENOMSTAGIAIRE"].ToString();
                rue = uneLigne["RUESTAGIAIRE"].ToString();
                codePostal = uneLigne["CODEPOSTALSTAGIAIRE"].ToString();
                ville = uneLigne["VILLESTAGIAIRE"].ToString();
                telephone = uneLigne["TELEPHONESTAGIAIRE"].ToString();
                // Création de l'objet unStagiaire et ajout dans la liste des stagiaires
                unStagiaire = new Stagiaire(numStagiaire, nomStagiaire, prenomStagiaire, rue, codePostal, ville, telephone);
                lesStagiaires.Add(unStagiaire);
            }

            return lesStagiaires;
        }
    }
}
