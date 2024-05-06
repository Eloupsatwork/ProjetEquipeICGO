using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class SessionDAO
    {
        /// <summary>
        /// Ajouter une session dans la table SESSION
        /// </summary>
        /// <param name="uneSession"></param>
        public static void AjouterUneSession(Session uneSession)
        {

            // Exécuter la requête d'insertion
            string requete = "INSERT INTO SESSION VALUES ( '" + uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + "' , " + uneSession.GetLeStage().GetNumStage() + " , " + uneSession.GetNumSession() + " , '" + uneSession.GetLAgence().GetNomAgence() + "' , " + uneSession.GetLeFormateur().GetNumFormateur() + " , '" + uneSession.GetDateSession() + "')";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        ///  Charger les sessions de la table SESSION dans une liste de sessions
        /// </summary>
        /// <returns></returns>
        public static List<Session> ChargerLesSessions()
        {
            List<Session> lesSessions = new List<Session>();
            Session uneSession;
            int numero_session;
            DateTime date;
            Stage le_stage;
            Formateur le_formateur;
            Agence lagence;

            lesSessions.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT CODECOMPETENCE, NUMSTAGE, NUMSESSION, NOMAGENCE, NUMFORMATEUR, DATEDEBUTSESSION FROM SESSION ";
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach(DataRow uneLigne in dt.Rows)
            {
                numero_session = int.Parse(uneLigne["NUMSESSION"].ToString());
                date = DateTime.Parse(uneLigne["DATEDEBUTSESSION"].ToString());
                le_stage = StageDAO.GetStage(uneLigne["CODECOMPETENCE"].ToString(), int.Parse(uneLigne["NUMSTAGE"].ToString()));
                le_formateur = FormateurDAO.GetFormateur(int.Parse(uneLigne["NUMFORMATEUR"].ToString()));
                lagence = new Agence(uneLigne["NOMAGENCE"].ToString());
                uneSession = new Session(numero_session, date, le_stage, le_formateur, lagence);
                lesSessions.Add(uneSession);
            }

            return lesSessions;
        }

        /// <summary>
        /// Retourne une session identifiée dans la table SESSION
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <param name="idSession">Numéro session</param>
        /// <returns></returns>
        public static Session GetSession(string idCompetence, int idStage, int idSession)
        {
            Session uneSession;
            Agence lagence;

            string requete = "SELECT NOMAGENCE, NUMFORMATEUR, DATEDEBUTSESSION FROM SESSION WHERE CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage + " AND NUMSESSION = " + idSession + " ";
            DataTable dt = Connexion.ExecuterRequete(requete);

            lagence = new Agence(dt.Rows[0]["NOMAGENCE"].ToString());

            uneSession = new Session(idSession, DateTime.Parse(dt.Rows[0]["DATEDEBUTSESSION"].ToString()), StageDAO.GetStage(idCompetence, idStage), FormateurDAO.GetFormateur(int.Parse(dt.Rows[0]["NUMFORMATEUR"].ToString())), lagence);

            return uneSession;
        }

        /// <summary>
        /// Modifier les caractéristiques d'une session identifiée dans la table SESSION
        /// </summary>
        /// <param name="uneSession">Une session</param>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <param name="idSession">Numéro session</param>
        public static void ModifierUneSession(Session uneSession, string idCompetence, int idStage, int idSession)
        {
            string requete = "UPDATE SESSION SET NOMAGENCE = '" + uneSession.GetLAgence().GetNomAgence() + "', NUMFORMATEUR = " + uneSession.GetLeFormateur().GetNumFormateur() + ", DATEDEBUTSESSION = '" + uneSession.GetDateSession() + "' WHERE NUMSESSION = " + idSession + " AND CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Supprimer une session identifiée dans la table SESSION
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <param name="idSession">Numéro session</param>
        public static void SupprimerUneSession(string idCompetence, int idStage, int idSession)
        {

            string requete = "DELETE FROM SESSION WHERE CODECOMPETENCE = '" + idCompetence + "'AND NUMSTAGE = " + idStage + "AND NUMSESSION = " + idSession;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les sessions de la table INSCRIRE d'un stagiaire identifié par son numéro
        /// </summary>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        /// <returns></returns>
        public static List<Session> ChargerLesSessionsDuStagiaire(int idStagiaire)
        {
            List<Session> lesSessions = new List<Session>();
            Session uneSession;
            int numero_session;
            DateTime date;
            Stage le_stage;
            Formateur le_formateur;
            Agence lagence;

            lesSessions.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT S.CODECOMPETENCE, S.NUMSTAGE, S.NUMSESSION, NOMAGENCE, NUMFORMATEUR, DATEDEBUTSESSION FROM SESSION AS S INNER JOIN INSCRIRE AS I ON S.CODECOMPETENCE = I.CODECOMPETENCE WHERE I.NUMSTAGIAIRE = " + idStagiaire;
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                numero_session = int.Parse(uneLigne["NUMSESSION"].ToString());
                date = DateTime.Parse(uneLigne["DATEDEBUTSESSION"].ToString());
                le_stage = StageDAO.GetStage(uneLigne["CODECOMPETENCE"].ToString(), int.Parse(uneLigne["NUMSTAGE"].ToString()));
                le_formateur = FormateurDAO.GetFormateur(int.Parse(uneLigne["NUMFORMATEUR"].ToString()));
                lagence = new Agence(uneLigne["NOMAGENCE"].ToString());
                uneSession = new Session(numero_session, date, le_stage, le_formateur, lagence);
                lesSessions.Add(uneSession);
            }

            return lesSessions;
        }

        /// <summary>
        /// Charger les sessions de la table INSCRIRE d'un stagiaire identifié par son numéro
        /// </summary>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        /// <returns></returns>
        public static List<Session> ChargerLesSessionsDuStagiaireProvisoire(int idStagiaire)
        {
            List<Session> lesSessions = new List<Session>();
            Session uneSession;
            int numero_session;
            DateTime date;
            Stage le_stage;
            Formateur le_formateur;
            Agence lagence;

            lesSessions.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT S.CODECOMPETENCE, S.NUMSTAGE, S.NUMSESSION, NOMAGENCE, NUMFORMATEUR, DATEDEBUTSESSION FROM SESSION AS S INNER JOIN INSCRIRE AS I ON S.CODECOMPETENCE = I.CODECOMPETENCE WHERE I.NUMSTAGIAIRE = " + idStagiaire + " AND ETATINSCRIPTION = 'P'";
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                numero_session = int.Parse(uneLigne["NUMSESSION"].ToString());
                date = DateTime.Parse(uneLigne["DATEDEBUTSESSION"].ToString());
                le_stage = StageDAO.GetStage(uneLigne["CODECOMPETENCE"].ToString(), int.Parse(uneLigne["NUMSTAGE"].ToString()));
                le_formateur = FormateurDAO.GetFormateur(int.Parse(uneLigne["NUMFORMATEUR"].ToString()));
                lagence = new Agence(uneLigne["NOMAGENCE"].ToString());
                uneSession = new Session(numero_session, date, le_stage, le_formateur, lagence);
                lesSessions.Add(uneSession);
            }

            return lesSessions;
        }

        /// <summary>
        /// Charger les sessions n'appartenant pas à la table INSCRIRE d'un stagiaire identifié par son numéro
        /// </summary>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        /// <returns></returns>
        public static List<Session> ChargerLesSessionsNonChoisiesDuStagiaire(int idStagiaire)
        {
            List<Session> lesSessions = new List<Session>();
            Session uneSession;
            int numero_session;
            DateTime date;
            Stage le_stage;
            Formateur le_formateur;
            Agence lagence;

            lesSessions.Clear();

            // Exécuter la requête de sélection
            string requete = "SELECT codecompetence, numstage, numsession, nomagence, numformateur, datedebutsession FROM session as s where not exists (SELECT codecompetence, numstage, numsession, numstagiaire FROM INSCRIRE as i WHERE s.numsession = i.numsession AND s.numstage = i.numstage AND s.CODECOMPETENCE = i.CODECOMPETENCE AND NUMSTAGIAIRE = " + idStagiaire + ")";
            DataTable dt = Connexion.ExecuterRequete(requete);

            foreach (DataRow uneLigne in dt.Rows)
            {
                numero_session = int.Parse(uneLigne["NUMSESSION"].ToString());
                date = DateTime.Parse(uneLigne["DATEDEBUTSESSION"].ToString());
                le_stage = StageDAO.GetStage(uneLigne["CODECOMPETENCE"].ToString(), int.Parse(uneLigne["NUMSTAGE"].ToString()));
                le_formateur = FormateurDAO.GetFormateur(int.Parse(uneLigne["NUMFORMATEUR"].ToString()));
                lagence = new Agence(uneLigne["NOMAGENCE"].ToString());
                uneSession = new Session(numero_session, date, le_stage, le_formateur, lagence);
                lesSessions.Add(uneSession);
            }

            return lesSessions;
        }
    }
}
