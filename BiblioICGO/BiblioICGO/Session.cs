using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioICGO
{
    public class Session
    {
        #region Attributs privés

        private int numSession;
        private DateTime dateSession;
        private Stage leStage;
        private Formateur leFormateur;
        private Agence lAgence;
        private List<Stagiaire> lesStagiaires;
        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur
        /// </summary>
        public Session()
        {
            lesStagiaires = new List<Stagiaire>();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numero_session">Numéro de la session</param>
        /// <param name="date">La date</param>
        /// <param name="le_stage">Le stage</param>
        /// <param name="le_formateur">Le formateur</param>
        /// <param name="lagence">L'agence</param>
        public Session(int numero_session, DateTime date, Stage le_stage, Formateur le_formateur, Agence lagence)
        {
            numSession = numero_session;
            dateSession = date;
            leStage = le_stage;
            leFormateur = le_formateur;
            lAgence = lagence;
            lesStagiaires = new List<Stagiaire>();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numero_session">Numéro de la session</param>
        /// <param name="date">La date</param>
        /// <param name="le_stage">Le stage</param>
        /// <param name="le_formateur">Le formateur</param>
        /// <param name="lagence">L'agence</param>
        /// <param name="les_stagiaires">Les stagiaires</param>
        public Session(int numero_session, DateTime date, Stage le_stage, Formateur le_formateur, Agence lagence, List<Stagiaire> les_stagiaires)
        {
            numSession = numero_session;
            dateSession = date;
            leStage = le_stage;
            leFormateur = le_formateur;
            lAgence = lagence;
            lesStagiaires = les_stagiaires;
        }

        #endregion

        #region Accesseurs

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>Le numéro de session</returns>
        public int GetNumSession()
        {
            return numSession;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="numero_session">Le numéro de la session</param>
        public void SetNumSession(int numero_session)
        {
            numSession = numero_session;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>La date</returns>
        public DateTime GetDateSession()
        {
            return dateSession;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="date_Session">La date</param>
        public void SetDateSession(DateTime date_Session)
        {
            dateSession = date_Session;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>Le stage</returns>
        public Stage GetLeStage()
        {
            return leStage;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="le_Stage">Le stage</param>
        public void SetLeStage(Stage le_Stage)
        {
            leStage = le_Stage;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>L'agence</returns>
        public Agence GetLAgence()
        {
            return lAgence;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="l_agence">L'agence</param>
        public void SetLAgence(Agence l_agence)
        {
            lAgence = l_agence;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>Le formateur</returns>
        public Formateur GetLeFormateur()
        {
            return leFormateur;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="le_Formateur">Le formateur</param>
        public void SetLeFormateur(Formateur le_Formateur)
        {
            leFormateur= le_Formateur;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>Les stagiaires</returns>
        public List<Stagiaire> GetLesStagiaires()
        {
            return lesStagiaires;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="les_Stagiaires">Les stagiaires</param>
        public void SetLesStagiaires(List<Stagiaire> les_Stagiaires)
        {
            lesStagiaires = les_Stagiaires;
        }

        #endregion

    }
}
