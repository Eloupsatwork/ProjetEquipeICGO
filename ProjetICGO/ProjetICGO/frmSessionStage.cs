using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BiblioICGO;
using BiblioICGODAO;

namespace ProjetICGO
{
    public partial class frmSessionStage : Form
    {
        // Booléen indiquant si une session a été choisie dans cboSession
        private bool choixSession;

        public frmSessionStage()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSessionStage_Load(object sender, EventArgs e)
        {
            // Valorisation de cboAgence
            Manager.ChargerLesAgences(cboAgence);
            // Valorisation de cboStage
            Manager.ChargerLesStages(cboStage);
            // Valorisation de cboSession
            Manager.ChargerLesSessions(cboSession);
            // Valorisation de cboFormateur
            Manager.ChargerLesFormateurs(cboFormateur);
        }

        /// <summary>
        /// Valorisation de cboFormateur avec les formateurs compétents pour la session de stage 
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        private void ChargerLesFormateursCompetents(string idCompetence)
        {

            List<Formateur> resultat = new List<Formateur>();
            bool competant;

            resultat.Clear();

            foreach (Formateur unFormateur in FormateurDAO.ChargerLesFormateurs())
            {
                competant = false;

                foreach(Competence uneCompetence in unFormateur.GetLesCompetences())
                {
                    if(uneCompetence.GetCodeCompetence() == idCompetence)
                    {
                        competant = true;
                    }
                }

                if(competant == true)
                {
                    cboFormateur.Items.Add(unFormateur.GetNumFormateur() + ". " + unFormateur.GetNomFormateur() + ". " + unFormateur.GetPrenom());
                }
            }

        }

        /// <summary>
        /// Ajout d'une session de stage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {

            Session laSession;
            int numero_session;
            DateTime date;
            Stage le_stage = new Stage();
            Formateur le_formateur = new Formateur();
            Agence lagence = new Agence();

            try
            {
                numero_session = int.Parse(txtNumSession.Text);
                date = dtpDateSession.Value;

                foreach (Stage unStage in StageDAO.ChargerLesStages())
                {
                    if(cboStage.Text == unStage.GetLaCompetence().GetCodeCompetence() + ". " + unStage.GetNumStage() + ". " + unStage.GetNomStage())
                    {
                        le_stage = unStage;
                    }
                }

                foreach (Formateur unFormateur in FormateurDAO.ChargerLesFormateurs())
                {
                    if(cboFormateur.Text == unFormateur.GetNumFormateur() + ". " + unFormateur.GetNomFormateur() + ". " + unFormateur.GetPrenom())
                    {
                        le_formateur = unFormateur;
                    }
                }

                foreach (Agence uneAgence in AgenceDAO.ChargerLesAgences())
                {
                    if(cboAgence.Text == uneAgence.GetNomAgence())
                    {
                        lagence = uneAgence;
                    }
                }

                laSession = new Session(numero_session, date, le_stage, le_formateur, lagence);
                
                SessionDAO.AjouterUneSession(laSession);

                btnAnnuler_Click(null, EventArgs.Empty);

                MessageBox.Show("Session enregistré", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Remise à vide de l'ensemble des zones de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            choixSession = false;
            txtNumSession.Clear();
            dtpDateSession.Value = DateTime.Now;
            cboAgence.SelectedIndex = -1;
            cboFormateur.SelectedIndex = -1;
            cboFormateur.Text = "";
            cboSession.SelectedIndex = -1;
            cboStage.SelectedIndex = -1;
            // Valorisation de cboSession
            Manager.ChargerLesSessions(cboSession);
            // Valorisation de cboFormateur
            Manager.ChargerLesFormateurs(cboFormateur);
            // Valorisation de cboStage
            Manager.ChargerLesStages(cboStage);
        }

        /// <summary>
        /// Modification de la session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifier_Click(object sender, EventArgs e)
        {

            Session laSession;
            int numero_session;
            DateTime date;
            Stage le_stage = new Stage();
            Formateur le_formateur = new Formateur();
            Agence lagence = new Agence();
            Boolean existe = false;

            try
            {
                numero_session = int.Parse(txtNumSession.Text);
                date = dtpDateSession.Value;

                foreach (Stage unStage in StageDAO.ChargerLesStages())
                {
                    if (cboStage.Text == unStage.GetLaCompetence().GetCodeCompetence() + ". " + unStage.GetNumStage() + ". " + unStage.GetNomStage())
                    {
                        le_stage = unStage;
                    }
                }

                foreach (Formateur unFormateur in FormateurDAO.ChargerLesFormateurs())
                {
                    if (cboFormateur.Text == unFormateur.GetNumFormateur() + ". " + unFormateur.GetNomFormateur() + ". " + unFormateur.GetPrenom())
                    {
                        le_formateur = unFormateur;
                    }
                }

                foreach (Agence uneAgence in AgenceDAO.ChargerLesAgences())
                {
                    if (cboAgence.Text == uneAgence.GetNomAgence())
                    {
                        lagence = uneAgence;
                    }
                }

                laSession = new Session(numero_session, date, le_stage, le_formateur, lagence);

                foreach (Session uneSession in SessionDAO.ChargerLesSessions())
                {
                    //Si la session existe
                    if(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() == laSession.GetLeStage().GetLaCompetence().GetCodeCompetence() && uneSession.GetLeStage().GetNumStage() == laSession.GetLeStage().GetNumStage() && uneSession.GetNumSession() == laSession.GetNumSession()){
                        existe = true;
                    }
                }

                if (existe)
                {
                    SessionDAO.ModifierUneSession(laSession, laSession.GetLeStage().GetLaCompetence().GetCodeCompetence(), le_stage.GetNumStage(), numero_session);

                    btnAnnuler_Click(null, EventArgs.Empty);

                    MessageBox.Show("Session modifiée", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La session n'existe pas !", "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Supprimer une session de stage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {

            Session laSession = new Session();

            try
            {

                foreach (Session uneSession in SessionDAO.ChargerLesSessions())
                {
                    if (cboSession.Text == uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetNumSession() + ". " + uneSession.GetLeStage().GetNomStage())
                    {
                        laSession = uneSession;
                    }
                }

                SessionDAO.SupprimerUneSession(laSession.GetLeStage().GetLaCompetence().GetCodeCompetence(), laSession.GetLeStage().GetNumStage(), laSession.GetNumSession());

                btnAnnuler_Click(null, EventArgs.Empty);

                MessageBox.Show("Session supprimée", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Valorisation des zones de saisie à la sélection d'une session choisie dans cboSession
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboSession.SelectedIndex != -1)
            {

            Session laSession = new Session();
            
            foreach (Session uneSession in SessionDAO.ChargerLesSessions())
            {
                if (cboSession.Text == uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetNumSession() + ". " + uneSession.GetLeStage().GetNomStage())
                {
                    laSession = uneSession;
                }
            }

            cboFormateur.Items.Clear();
            cboStage.Items.Clear();

            ChargerLesFormateursCompetents(laSession.GetLeStage().GetLaCompetence().GetCodeCompetence());
            ChargerLesStagesSession(laSession);
            txtNumSession.Text = laSession.GetNumSession().ToString();
            cboAgence.Text = laSession.GetLAgence().GetNomAgence();
            cboFormateur.Text = laSession.GetLeFormateur().GetNumFormateur() + ". " + laSession.GetLeFormateur().GetNomFormateur() + ". " + laSession.GetLeFormateur().GetPrenom();

            }

        }

        private void ChargerLesStagesSession(Session laSession)
        {
            foreach (Session uneSession in SessionDAO.ChargerLesSessions())
            {
                if(uneSession.GetLeStage().GetNumStage() == laSession.GetLeStage().GetNumStage() && uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() == laSession.GetLeStage().GetLaCompetence().GetCodeCompetence())
                {
                    cboStage.Items.Add(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetLeStage().GetNomStage());
                    cboStage.Text = uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetLeStage().GetNomStage();
                }
            }
        }

        /// <summary>
        /// Valorisation des zones de saisie à la sélection d'un stage choisi dans cboStage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboStage.SelectedIndex != -1)
            {

            cboFormateur.Items.Clear();

            Utilitaires.ExtraireIdStage(cboStage.Text, out string codeCompetence, out int numStage);

            Stage leStage = new Stage();

            foreach (Stage unStage in StageDAO.ChargerLesStages())
            {
                if (unStage.GetLaCompetence().GetCodeCompetence() == codeCompetence && unStage.GetNumStage() == numStage)
                {
                    leStage = unStage;
                }
            }

            ChargerLesFormateursCompetents(leStage.GetLaCompetence().GetCodeCompetence());

            }
        }
     }
}
