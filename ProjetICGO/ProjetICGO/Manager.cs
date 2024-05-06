using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BiblioICGO;
using BiblioICGODAO;

namespace ProjetICGO
{
    public class Manager
    {

        #region Agence

        /// <summary>
        /// Valorisation de cboAgence
        /// </summary>
        /// <param name="cboAgence">Combo cboAgence</param>
        static public void ChargerLesAgences(ComboBox cboAgence)
        {
            List<Agence> lesAgences = new List<Agence>();

            // Remise à vide de cboAgence
            cboAgence.SelectedIndex = -1;
            cboAgence.Items.Clear();
            // Recherche des agences dans la base de données
            lesAgences = AgenceDAO.ChargerLesAgences();

            // Ajout de chaque nom d'agence dans cboAgence
            foreach (Agence uneAgence in lesAgences)
            {
                cboAgence.Items.Add(uneAgence.GetNomAgence());
            }
        }

        #endregion

        #region Compétence

        /// <summary>
        /// Valorisation de cboCompetence
        /// </summary>
        /// <param name="cboCompetence">Combo cboCompetence</param>
        static public void ChargerLesCompetences(ComboBox cboCompetence)
        {
            List<Competence> lesCompetences = new List<Competence>();

            // Remise à vide de cboCompetence
            cboCompetence.SelectedIndex = -1;
            cboCompetence.Items.Clear();
            // Recherche des compétences dans la base de données
            lesCompetences = CompetenceDAO.ChargerLesCompetences();
            // Ajout de chaque code compétence dans cboCompetence
            foreach (Competence uneCompetence in lesCompetences)
            {
                cboCompetence.Items.Add(uneCompetence.GetCodeCompetence());
            }
        }

        #endregion

        #region Formateur

        /// <summary>
        /// Valorisation de cboFormateur
        /// </summary>
        /// <param name="cboFormateur">Combo cboFormateur</param>
        static public void ChargerLesFormateurs(ComboBox cboFormateur)
        {
            List<Formateur> lesFormateurs = new List<Formateur>();

            // Recherche des formateurs dans la base de données
            lesFormateurs = FormateurDAO.ChargerLesFormateurs();
            // Remise à vide de cboFormateur
            cboFormateur.SelectedIndex = -1;
            cboFormateur.Items.Clear();
            // Création d'un libellé "numéro. nom prénom" et ajout dans cboFormateur pour chaque formateur
            foreach (Formateur unFormateur in lesFormateurs)
            {
                cboFormateur.Items.Add(unFormateur.GetNumFormateur() + ". " + unFormateur.GetNomFormateur() + ". " + unFormateur.GetPrenom());
            }
        }

        #endregion

        #region Stagiaire

        /// <summary>
        /// Valorisation de cboStagiaire
        /// </summary>
        /// <param name="cboStagiaire">Combo cboStagiaire</param>
        static public void ChargerLesStagiaires(ComboBox cboStagiaire)
        {
            List<Stagiaire> lesStagiaires = new List<Stagiaire>();

            // Recherche des stagiaires dans la base de données
            lesStagiaires = StagiaireDAO.ChargerLesStagiaires();
            // Remise à vide de cboStagaire
            cboStagiaire.SelectedIndex = -1;
            cboStagiaire.Items.Clear();
            // Création d'un libellé "numéro. nom prénom" et ajout dans cboStagiaire pour chaque stagiaire
            foreach (Stagiaire unStagiaire in lesStagiaires)
            {
                cboStagiaire.Items.Add(unStagiaire.GetNumStagiaire() + ". " + unStagiaire.GetNomStagiaire() + ". " + unStagiaire.GetPrenom());
            }
        }


        #endregion

        #region Stage

        /// <summary>
        /// Valorisation de cboStage
        /// </summary>
        /// <param name="cboStage">Combo cboStage</param>
        static public void ChargerLesStages(ComboBox cboStage)
        {
            List<Stage> lesStages = new List<Stage>();

            // Recherche des stages dans la base de données
            lesStages = StageDAO.ChargerLesStages();
            // Remise à vide de cboStage
            cboStage.SelectedIndex = -1;
            cboStage.Items.Clear();
            // Création d'un libellé "numéro. nom" et ajout dans cboStage pour chaque stage (étalé ou groupé)
            foreach (dynamic unStage in lesStages)
            {
                cboStage.Items.Add(unStage.GetLaCompetence().GetCodeCompetence() + ". " + unStage.GetNumStage() + ". " + unStage.GetNomStage());
            }
        }

        #endregion

        #region Session stage

        static public void ChargerLesSessions(ComboBox cboSession)
        {
            List<Session> lesSessions = new List<Session>();

            lesSessions = SessionDAO.ChargerLesSessions();

            cboSession.SelectedIndex = -1;
            cboSession.Items.Clear();

            foreach (dynamic uneSession in lesSessions)
            {
                cboSession.Items.Add(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetNumSession() + ". " + uneSession.GetLeStage().GetNomStage());
            }
        }

        #endregion

        #region Module

        /// <summary>
        /// Valorisation de cboFormateur
        /// </summary>
        /// <param name="cboFormateur">Combo cboFormateur</param>
        static public void ChargerLesModules(ComboBox cboModule)
        {
            List<Module> lesModules = new List<Module>();

            // Recherche des formateurs dans la base de données
            lesModules = ModuleDAO.ChargerLesModules();
            // Remise à vide de cboFormateur
            cboModule.SelectedIndex = -1;
            cboModule.Items.Clear();
            // Création d'un libellé "numéro. nom prénom" et ajout dans cboFormateur pour chaque formateur
            foreach (Module unModule in lesModules)
            {
                cboModule.Items.Add(unModule.GetNumModule() + ". " + unModule.GetNomModule());
            }
        }

        #endregion
    }
}
