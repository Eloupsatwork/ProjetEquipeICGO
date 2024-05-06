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
    public partial class frmConfirmerInscription : Form
    {
        public frmConfirmerInscription()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Confirmer une inscription (état définitif)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmer_Click(object sender, EventArgs e)
        {
            string codeCompetence;
            int numStage, numSession;

            try
            {
                Utilitaires.ExtraireIdSession(cboSession.Text, out codeCompetence, out numStage, out numSession);
                Session laSession = SessionDAO.GetSession(codeCompetence, numStage, numSession);

                int idStagiaire = Utilitaires.ExtraireNumStagiaire(cboStagiaire.Text);
                Stagiaire leStagiaire = StagiaireDAO.GetStagiaire(idStagiaire);

                Inscription uneInscription = new Inscription(laSession, leStagiaire, "p");

                InscriptionDAO.ConfirmerInscription(uneInscription);

                MessageBox.Show("Inscription Modifié", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboSession.Text = "";
            cboStagiaire.Text = "";

        }

        private void frmConfirmerInscription_Load(object sender, EventArgs e)
        {
            // Valorisation de cboStagiaire
            Manager.ChargerLesStagiaires(cboStagiaire);
        }

        /// <summary>
        /// Valorisation de cboSession : chargement des sessions du stagiaire
        /// </summary>
        private void ChargerLesSessionsDuStagiaireProvisoire()
        {
            cboSession.Items.Clear();

            int idStagiaire = Utilitaires.ExtraireNumStagiaire(cboStagiaire.Text);

            foreach (Session uneSession in SessionDAO.ChargerLesSessionsDuStagiaireProvisoire(idStagiaire))
            {
                cboSession.Items.Add(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetNumSession() + ". " + uneSession.GetLeStage().GetNomStage());
            }

        }



        private void cboStagiaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Valorisation de cboSession
            ChargerLesSessionsDuStagiaireProvisoire();
        }

        /// <summary>
        /// Suppression d'une inscription
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            string codeCompetence;
            int numStage, numSession;
            int idStagiaire = Utilitaires.ExtraireNumStagiaire(cboStagiaire.Text);
            try {
                Utilitaires.ExtraireIdSession(cboSession.Text, out codeCompetence, out numStage, out numSession);
            
                InscriptionDAO.SupprimerUneInscription(codeCompetence, numStage, numSession, idStagiaire);

                MessageBox.Show("Inscription supprimé", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
                    {
                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboSession.Text = "";
            cboStagiaire.Text = "";
        }

 
    }
}
