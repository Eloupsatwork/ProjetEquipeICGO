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
    public partial class frmCompetence : Form
    {
        public frmCompetence()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ajout d'une compétence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            string codeCompetence;
            string nomCompetence;
            Competence uneCompetence;

            try
            {
                if (!txtCodeCompetence.Text.Equals(""))
                {
                    // Récupération des informations saisies et ajout du caractère ' en double si nécessaire pour construire une requête SQL
                    codeCompetence = txtCodeCompetence.Text.Replace("'", "''");
                    nomCompetence = txtNomCompetence.Text.Replace("'", "''");
                    // Création de l'objet uneCompetence
                    uneCompetence = new Competence(codeCompetence, nomCompetence);
                    // Création de la compétence dans la base de données
                    CompetenceDAO.AjouterUneCompetence(uneCompetence);
                    // Valorisation de cboCompetence
                    Manager.ChargerLesCompetences(cboCompetence);
                    // Remise à vide des zones : déclenchement du bouton annuler
                    btnAnnuler_Click(null, EventArgs.Empty);
                    // Message
                    MessageBox.Show("Compétence enregistrée", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Saisir un code compétence", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void frmCompetence_Load(object sender, EventArgs e)
        {
            Manager.ChargerLesCompetences(cboCompetence);
        }

        /// <summary>
        /// Modification d'une compétence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            string idCompetence;
            string codeCompetence;
            string nomCompetence;
            Competence uneCompetence;

            // Si une compétence est choisie dans cboCompetence
            if (cboCompetence.SelectedIndex >= 0)
            {
                try
                {
                    // Récupération des informations saisies et ajout du caractère ' en double si nécessaire pour construire une requête SQL
                    idCompetence = cboCompetence.Text;
                    codeCompetence = txtCodeCompetence.Text.Replace("'", "''");
                    nomCompetence = txtNomCompetence.Text.Replace("'", "''");
                    // Création de l'objet uneCompetence
                    uneCompetence = new Competence(codeCompetence, nomCompetence);

                    // Mise à jour de la compétence dans la base de données
                    CompetenceDAO.ModifierUneCompetence(uneCompetence, idCompetence);
                    // Valorisation de cboCompetence
                    Manager.ChargerLesCompetences(cboCompetence);
                    // Message
                    MessageBox.Show("Compétence enregistrée", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Aucune compétence choisie dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Supprimer une compétence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            string idCompetence;
            DialogResult reponse;

            // Si une compétence est choisie dans cboCompetence
            if (cboCompetence.SelectedIndex >= 0)
            {
                reponse = MessageBox.Show("Etes vous sûr de vouloir supprimer cette compétence ?", "Suppression d'une compétence", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (reponse == DialogResult.Yes)
                {
                    try
                    {
                        // Récupération du code compétence choisi dans cboCompetence
                        idCompetence = cboCompetence.Text;
                        // Supprimer la compétence identifiée dans la base de données
                        CompetenceDAO.SupprimerUneCompetence(idCompetence);
                        Manager.ChargerLesCompetences(cboCompetence);
                        // Message
                        MessageBox.Show("Compétence supprimée", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucune compétence choisie dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Valorisation des zones de saisie à la sélection d'une compétence choisie dans cboCompetence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCompetence_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idCompetence;
            Competence uneCompetence;

            // Récupération du code compétence choisi dans cboCompetence 
            idCompetence = cboCompetence.Text;
            // Recherche de la compétence identifiée dans la base de données
            uneCompetence = CompetenceDAO.GetCompetence(idCompetence);
            // Valorisation des zones de saisie
            txtCodeCompetence.Text = idCompetence;
            txtNomCompetence.Text = uneCompetence.GetNomCompetence();
        }

        /// <summary>
        /// Remise à vide de l'ensemble des zones de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            txtCodeCompetence.Clear();
            txtNomCompetence.Clear();
            cboCompetence.SelectedIndex = -1;
        }
    }
}
