using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioICGO
{
    public class Inscription
    {
        #region Attributs privés

        private Session laSession;
        private Stagiaire leStagiaire;
        private string etatInscription;
        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur
        /// </summary>
        public Inscription()
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="la_Session">La session</param>
        /// <param name="le_Stagiaire">Le stagiaire</param>
        /// <param name="etat_Inscription">Etat inscription</param>
        public Inscription(Session la_Session, Stagiaire le_Stagiaire, string etat_Inscription)
        {
            laSession = la_Session;
            leStagiaire = le_Stagiaire;
            etatInscription = etat_Inscription;
        }

        #endregion

        #region Accesseurs

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>La session</returns>
        public Session GetLaSession()
        {
            return laSession;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="uneSession">La session</param>
        public void SetLaSession(Session uneSession)
        {
            laSession = uneSession;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>Le stagiaire</returns>
        public Stagiaire GetLeStagiaire()
        {
            return leStagiaire;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="unStagiaire">Le stagiaire</param>
        public void SetLeStagiaire(Stagiaire unStagiaire)
        {
            leStagiaire = unStagiaire;
        }

        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <returns>L'état inscription</returns>
        public string GetEtatInscription()
        {
            return etatInscription;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="etat_Inscription">L'état inscription</param>
        public void SetEtatInscription(string etat_Inscription)
        {
            etatInscription= etat_Inscription;
        }

        #endregion
    }
}
