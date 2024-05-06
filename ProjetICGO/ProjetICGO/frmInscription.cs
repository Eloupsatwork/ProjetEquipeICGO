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
    public partial class frmInscription : Form
    {
        public frmInscription()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ouverture du formulaire frmStagiaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStagiaire_Click(object sender, EventArgs e)
        {
            frmStagiaire fs = new frmStagiaire();
            cboStagiaire.SelectedIndex = -1;
            fs.Show();
        }

        /// <summary>
        /// Valorisation de cboSession : chargement des sessions non choisies du stagiaire
        /// </summary>
        private void ChargerLesSessionsNonChoisiesDuStagiaire()
        {
            List<Session> lesSessions = new List<Session>();
            int idStagiaire;
            idStagiaire = Utilitaires.ExtraireNumStagiaire(cboStagiaire.Text);
            lesSessions = SessionDAO.ChargerLesSessionsNonChoisiesDuStagiaire(idStagiaire);

            cboSession.SelectedIndex = -1;
            cboSession.Items.Clear();

            foreach (Session uneSession in lesSessions)
            {
                cboSession.Items.Add(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence() + ". " + uneSession.GetLeStage().GetNumStage() + ". " + uneSession.GetNumSession() + ". " + uneSession.GetLeStage().GetNomStage());
            }

        }

        /// <summary>
        /// Ajout d'une inscription
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            int numStagiaire;
            string codeCompetence;
            int numStage;
            int numSession;
            Stagiaire unStagiaire;
            Session uneSession;
            string unEtat;
            Inscription uneInscription;

            try
            {
                if (cboStagiaire.SelectedIndex >= 0 && cboSession.SelectedIndex >= 0)
                {
                    numStagiaire = Utilitaires.ExtraireNumStagiaire(cboStagiaire.Text);
                    unStagiaire = StagiaireDAO.GetStagiaire(numStagiaire);

                    Utilitaires.ExtraireIdSession(cboSession.Text, out codeCompetence, out numStage, out numSession);

                    uneSession = SessionDAO.GetSession(codeCompetence, numStage, numSession);

                    unEtat = cboEtat.Text;

                    if (unEtat == "Définitif")
                    {
                        uneInscription = new Inscription(uneSession, unStagiaire, "D");
                    }
                    else
                    {
                        uneInscription = new Inscription(uneSession, unStagiaire, "P");
                    }

                    if (InscriptionDAO.VerifierPlacesDisponibles(codeCompetence, numStage, numStagiaire))
                    {
                        InscriptionDAO.AjouterUneInscription(uneInscription);
                        MessageBox.Show("La session à bien été enregistré", "Information !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Il n'y a plus de place disponible", "Information !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Remise à vide des zones : déclenchement du bouton annuler
                    btnAnnuler_Click(null, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Aucune session ou aucun stagiaire n'a été choisit !", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ajout échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Remise à vide de l'ensemble des zones de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            cboStagiaire.SelectedIndex = -1;
            cboSession.SelectedIndex = -1;
            cboEtat.SelectedIndex = -1;
        }

        private void frmInscription_Load(object sender, EventArgs e)
        {
            // Valorisation de cboEtat
            cboEtat.Items.Add("Définitif");
            cboEtat.Items.Add("Provisoire");
            // Valorisation de cboStagiaire

            // Valorisation de cboSession

        }

        /// <summary>
        /// Valorisation de cboSession après la sélection d'un stagiaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStagiaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStagiaire.SelectedIndex >= 0)
            {
                // Charger les sessions auxquelles le stagiaire n'est pas encore inscrit
                ChargerLesSessionsNonChoisiesDuStagiaire();
            }
            else
            {
                cboStagiaire.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Valorisation de cboStagiaire sur le click de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStagiaire_Click(object sender, EventArgs e)
        {
            // Valorisation de cboStagiaire
            Manager.ChargerLesStagiaires(cboStagiaire);
        }
    }
}