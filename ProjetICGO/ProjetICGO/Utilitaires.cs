using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetICGO
{
    public class Utilitaires
    {
        #region Formateur

        /// <summary>
        /// Extraction du numéro formateur à partir du libellé choisi dans un comboBox
        /// </summary>
        /// <param name="unLibelleFormateur"></param>
        /// <returns></returns>
        static public int ExtraireNumFormateur(string unLibelleFormateur)
        {
            int numFormateur;
            string[] strFormateur;
            string idFormateur;

            // Récupération dans un tableau strFormateur des éléments du libellé séparé par le caractère "."
            strFormateur = unLibelleFormateur.Split(new String[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            // Récupération du premier élément du tableau strFormateur
            idFormateur = strFormateur[0].ToString();
            // Conversion de l'élément en valeur de type int
            numFormateur = int.Parse(idFormateur);
            // Retour du résultat
            return numFormateur;
        }

        #endregion

        #region Stagiaire

        /// <summary>
        /// Extraction du numéro stagiaire à partir du libellé choisi dans dans un comboBox
        /// </summary>
        /// <param name="unLibelleStagiaire"></param>
        /// <returns></returns>
        static public int ExtraireNumStagiaire(string unLibelleStagiaire)
        {
            int numStagiaire;
            string[] strStagiaire;
            string idStagiaire;

            // Récupération dans un tableau strStagiaire des éléments du libellé séparé par le caractère "."
            strStagiaire = unLibelleStagiaire.Split(new String[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            // Récupération du premier élément du tableau strStagiaire
            idStagiaire = strStagiaire[0].ToString();
            // Conversion de l'élément en valeur de type int
            numStagiaire = int.Parse(idStagiaire);
            // Retour du résultat
            return numStagiaire;
        }

        #endregion

        #region Stage

        /// <summary>
        /// Extraction de l'identifiant stage (code compétence, numéro stage) à partir du libellé choisi dans un comboBox
        /// </summary>
        /// <param name="unLibelleStage">Libellé</param>
        /// <param name="codeCompetence">Code compétence (en sortie)</param>
        /// <param name="numStage">Numéro stage (en sortie)</param>
        static public void ExtraireIdStage(string unLibelleStage, out string codeCompetence, out int numStage)
        {
            string[] strStage;
            string idStage;

            // Récupération dans un tableau strStage des éléments du libellé séparé par le caractère "."
            strStage = unLibelleStage.Split(new String[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            // Récupération du premier élément compétence du tableau strStage
            codeCompetence = strStage[0].ToString();
            // Récupération du deuxième élément numéro stage du tableau strStage
            idStage = strStage[1].ToString();
            // Conversion en int
            numStage = int.Parse(idStage);
        }

        #endregion

        #region Session stage
        static public void ExtraireIdSession(string unlibelleSession, out string codeCompetence, out int numStage, out int numSession)
        {
            string[] strSession;
            string idSession;

            //Récupération dans un tableau strSession des éléments du libellé séparé par le caractère "."
            strSession = unlibelleSession.Split(new String[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            // Récupération du premier élément compétence du tableau strSession
            codeCompetence = strSession[0].ToString();
            // Récupération du deuxième élément numéro stage du tableau strSession
            idSession = strSession[1].ToString();
            // Conversion en int
            numStage = int.Parse(idSession);
            numSession = int.Parse(idSession);
        }
        #endregion

        #region Module
        static public int ExtraireNumModule(string libelleModule)
        {
            int numModule;
            string[] strModule;
            string idModule;

            // Récupération dans un tableau strModule des éléments du libellé séparé par le caractère "."
            strModule = libelleModule.Split(new String[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            // Récupération du premier élément compétence du tableau strMdule
            idModule = strModule[0].ToString();
            // Conversion en int
            numModule = int.Parse(idModule);
            // Retour du résultat
            return numModule;

        }


        #endregion
    }
}
