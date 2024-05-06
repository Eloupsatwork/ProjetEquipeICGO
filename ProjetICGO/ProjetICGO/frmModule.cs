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
    public partial class frmModule : Form
    {
        public frmModule()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Remise à vide de l'ensemble des zones de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            cboModule.Items.Clear();
            txtNomModule.Clear();
            txtNumModule.Clear();
            txtNomSupportCours.Clear();
            txtNomPresentation.Clear();
            txtPlaceSupportCours.Clear();
            txtPlacePresentation.Clear();
            // Valorisation de cboModule
            Manager.ChargerLesModules(cboModule);
            cboModule.Text = "";
        }

        /// <summary>
        /// Modification d'un module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            int NumModule;
            string NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation;
            Module unModule;

            // Si un module est choisi dans cboModule
            if (cboModule.SelectedIndex >= 0)
            {
                if (!int.TryParse(txtNumModule.Text, out NumModule))
                {
                    MessageBox.Show("Le numéro de formateur est incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        // Récupération du numéro de Module choisi dans cboModule
                        NumModule = Utilitaires.ExtraireNumFormateur(cboModule.Text);
                        // Récupération des informations des zones de saisie et ajout du caractère ' en double si nécessaire pour construire une requête SQL
                        NomModule = txtNomModule.Text;
                        NomSupportCours = txtNomSupportCours.Text.Replace("'", "''");
                        NomPresentation = txtNomPresentation.Text.Replace("'", "''");
                        PlaceSupportCours = txtPlaceSupportCours.Text;
                        PlacePresentation = txtPlacePresentation.Text;
                        // Création de l'objet unModule
                        unModule = new Module(NumModule, NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation);
                        // Mise à jour du Module dans la base de données
                        ModuleDAO.ModifierUnModule(unModule, NumModule);
                        // Valorisation de cboModule
                        ModuleDAO.ChargerLesModules();
                        // Valorisation de cboModule
                        Manager.ChargerLesModules(cboModule);
                        // Message
                        MessageBox.Show("Module enregistré", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun Module est choisi dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// Supprimer un module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            int NumModule;
            DialogResult reponse;

            // Si un formateur est choisi dans cboFormateur
            if (cboModule.SelectedIndex >= 0)
            {
                reponse = MessageBox.Show("Etes vous sûr de vouloir supprimer ce Module ?", "Suppression d'un Module", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (reponse == DialogResult.Yes)
                {
                    try
                    {
                        // Récupération du numéro de Module choisi dans cboModule
                        NumModule = Utilitaires.ExtraireNumModule(cboModule.Text);
                        // Supprimer le formateur identifié dans la base de données
                        ModuleDAO.SupprimerUnModule(NumModule);
                        // Valorisation de cboModule
                        Manager.ChargerLesModules(cboModule);
                        // Message
                        MessageBox.Show("Module supprimé", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun Module choisi dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// Ajout d'un module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            int NumModule;
            string NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation;
            Module unModule;

            if (!int.TryParse(txtNumModule.Text, out NumModule))
            {
                MessageBox.Show("Le numéro de Module est incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Récupération des informations saisies et ajout du caractère ' en double si nécessaire pour construire une requête SQL
                    NomModule = txtNomModule.Text;
                    NomSupportCours = txtNomSupportCours.Text.Replace("'", "''");
                    NomPresentation = txtNomPresentation.Text.Replace("'", "''");
                    PlaceSupportCours = txtPlaceSupportCours.Text;
                    PlacePresentation = txtPlacePresentation.Text;
                    // Création de l'objet unModule
                    unModule = new Module(NumModule, NomModule, NomSupportCours, NomPresentation, PlaceSupportCours, PlacePresentation);
                    // Création du formateur dans la base de données
                    ModuleDAO.AjouterUnModule(unModule);
                    // Message
                    MessageBox.Show("Module enregistré", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Remise à vide des zones : déclenchement du bouton annuler
                    btnAnnuler_Click(null, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void frmModule_Load(object sender, EventArgs e)
        {
            Manager.ChargerLesModules(cboModule);

        }


        private void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            int NumModule;
            Module unModule;

            // si sélection d'un formateur : extraction de son identifiant et recherche et affichage de ses coordonnées
            if (cboModule.SelectedIndex >= 0)
            {
                NumModule = Utilitaires.ExtraireNumModule(cboModule.Text);
                unModule = ModuleDAO.GetModule(NumModule);
                txtNumModule.Text = NumModule.ToString();
                txtNomModule.Text = unModule.GetNomModule();
                txtNomPresentation.Text = unModule.GetNomPresentation();
                txtNomSupportCours.Text = unModule.GetNomSupportCours();
                txtPlaceSupportCours.Text = unModule.GetPlaceSupportCours();
                txtPlacePresentation.Text = unModule.GetPlacePresentation();
            }
            else // remise à vide des zones et affichage de la date du jour
            {
                cboModule.Items.Clear();
                txtNomModule.Clear();
                txtNumModule.Clear();
                txtNomSupportCours.Clear();
                txtNomPresentation.Clear();
                txtPlaceSupportCours.Clear();
                txtPlacePresentation.Clear();
            }
        }


    }
}