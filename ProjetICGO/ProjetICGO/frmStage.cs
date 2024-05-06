﻿using System;
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
    public partial class frmStage : Form
    {
        public frmStage()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Supprimer un stage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            int idStage;
            string codeCompetence;
            DialogResult reponse;

            // Si un stage est choisi dans cboStage
            if (cboStage.SelectedIndex >= 0)
            {
                reponse = MessageBox.Show("Etes vous sûr de vouloir supprimer ce stage ?", "Suppression d'un stage", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (reponse == DialogResult.Yes)
                {
                    try
                    {
                        // Extraction de l'identifiant du stage (code compétence et numéro stage)
                        Utilitaires.ExtraireIdStage(cboStage.Text, out codeCompetence, out idStage);
                        // Supprimer un stage identifié dans la base de données
                        StageDAO.SupprimerUnStage(codeCompetence, idStage);
                        // Valorisation de cboStage
                        Manager.ChargerLesStages(cboStage);
                        // Remise à vide des zones : déclenchement du bouton annuler
                        btnAnnuler_Click(null, EventArgs.Empty);
                        // Message
                        MessageBox.Show("Stage supprimé", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun stage choisi dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Remise à vide de l'ensemble des zones de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            txtNumStage.Clear();
            txtNomStage.Clear();
            txtDuree.Clear();
            txtPrix.Clear();
            txtNbPlaces.Clear();
            lblJour.Visible = false;
            cboJour.SelectedIndex = -1;
            cboJour.Visible = false;
            lblNbHeures.Visible = false;
            txtNbHeures.Clear();
            txtNbHeures.Visible = false;
            cboCompetence.SelectedIndex = -1;
            cboStage.SelectedIndex = -1;
        }

        /// <summary>
        /// Modification d'un stage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            dynamic unStage;    // stage étalé ou stage groupé lors de l'exécution
            int numStage, idStage;
            int duree;
            string nbHeures;
            int nbPlaces;
            string prix;
            string nomStage;
            string jour;
            Competence uneCompetence;
            string codeCompetence, idCompetence;

            // si sélection d'un stage : extraction de son identifiant et recherche et affichage de ses caractéristiques
            if (cboStage.SelectedIndex >= 0)
            {
                if (!int.TryParse(txtNumStage.Text, out numStage))
                {
                    MessageBox.Show("Le numéro de stage est incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!int.TryParse(txtDuree.Text, out duree))
                    {
                        MessageBox.Show("La durée est incorrecte", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (!int.TryParse(txtNbPlaces.Text, out nbPlaces))
                        {
                            MessageBox.Show("Le nombre de places est incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            try
                            {
                                Utilitaires.ExtraireIdStage(cboStage.Text, out idCompetence, out idStage);
                                codeCompetence = cboCompetence.Text;
                                uneCompetence = CompetenceDAO.GetCompetence(codeCompetence);
                                nomStage = txtNomStage.Text.Replace("'", "''");
                                prix = txtPrix.Text;
                                // Selon le type de stage
                                if (optEtale.Checked)
                                {
                                    // Création d'un objet stage étalé
                                    jour = cboJour.Text;
                                    unStage = new StageEtale(numStage, nomStage, duree, prix, nbPlaces, uneCompetence, jour);
                                }
                                else
                                {
                                    // Création d'un objet stage groupé
                                    nbHeures = txtNbHeures.Text;
                                    unStage = new StageGroupe(numStage, nomStage, duree, prix, nbPlaces, uneCompetence, nbHeures);
                                }

                                // Mise à jour du stage dans la base de données
                                StageDAO.ModifierUnStage(unStage, idCompetence, idStage);
                                // Valorisation de cboStage
                                Manager.ChargerLesStages(cboStage);
                                // Message
                                MessageBox.Show("Stage enregistré", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun stage choisi dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Ajout d'un stage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            dynamic unStage;
            int numStage;
            int duree;
            string nbHeures;
            int nbPlaces;
            string prix;
            string nomStage;
            string jour;
            Competence uneCompetence;
            string codeCompetence;

            if ((cboCompetence.SelectedIndex >= 0) && (!txtNumStage.Text.Equals("")))
            {
                if (!int.TryParse(txtNumStage.Text, out numStage))
                {
                    MessageBox.Show("Le numéro de stage est incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!int.TryParse(txtDuree.Text, out duree))
                    {
                        MessageBox.Show("La durée est incorrecte", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (!int.TryParse(txtNbPlaces.Text, out nbPlaces))
                        {
                            MessageBox.Show("Le nombre de places est incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            try
                            {
                                // Récupération des informations saisies et ajout du caractère ' en double si nécessaire pour construire une requête SQL
                                codeCompetence = cboCompetence.Text;
                                uneCompetence = CompetenceDAO.GetCompetence(codeCompetence);
                                nomStage = txtNomStage.Text.Replace("'", "''");
                                prix = txtPrix.Text;
                                // Selon le type de stage
                                if (optEtale.Checked)
                                {
                                    // Création d'un objet stage étalé
                                    jour = cboJour.Text;
                                    unStage = new StageEtale(numStage, nomStage, duree, prix, nbPlaces, uneCompetence, jour);
                                }
                                else
                                {
                                    // Création d'un objet stage groupé
                                    nbHeures = txtNbHeures.Text;
                                    unStage = new StageGroupe(numStage, nomStage, duree, prix, nbPlaces, uneCompetence, nbHeures);
                                }
                                // Création du stage dans la base de données
                                StageDAO.AjouterUnStage(unStage);
                                // Message
                                MessageBox.Show("Stage enregistré", "Mise à jour réussie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Déclencher l'ouverture du formulaire AffecterModule
                                btnAffecterModule_Click(null, EventArgs.Empty);
                                // Valorisation de cboStage
                                Manager.ChargerLesStages(cboStage);
                                // Remise à vide des zones : déclenchement du bouton annuler
                                btnAnnuler_Click(null, EventArgs.Empty);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Mise à jour échouée !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Choisir une compétence et saisir un numéro de stage", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void frmStage_Load(object sender, EventArgs e)
        {
            // Valorisation de cboJour
            cboJour.Items.Add("Lundi");
            cboJour.Items.Add("Mardi");
            cboJour.Items.Add("Mercredi");
            cboJour.Items.Add("Jeudi");
            cboJour.Items.Add("Vendredi");
            // Rendre les informations de jour invisibles
            lblJour.Visible = false;
            cboJour.Visible = false;
            // Rendre les informations de nombre d'heures invisibles
            lblNbHeures.Visible = false;
            txtNbHeures.Visible = false;
            // Valorisation de cboStage
            Manager.ChargerLesStages(cboStage);
            // Valorisation de cboCompetence
            Manager.ChargerLesCompetences(cboCompetence);
        }

        /// <summary>
        /// Rendre visible le jour et invisible le nombre d'heures si stage étalé 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optEtale_CheckedChanged(object sender, EventArgs e)
        {
            lblJour.Visible = true;
            cboJour.Visible = true;
            lblNbHeures.Visible = false;
            txtNbHeures.Visible = false;

        }

        /// <summary>
        /// Rendre invisible le jour et visible le nombre d'heures si stage groupé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optGroupe_CheckedChanged(object sender, EventArgs e)
        {
            lblNbHeures.Visible = true;
            txtNbHeures.Visible = true;
            lblJour.Visible = false;
            cboJour.Visible = false;
        }

        /// <summary>
        /// Valorisation des zones de saisie à la sélection d'un stage choisi dans cboStage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idStage;
            string codeCompetence;
            dynamic unStage;  // stage étalé ou groupé lors de l'exécution

            // si sélection d'un stage : extraction de son identifiant et recherche et affichage de ses caractéristiques
            if (cboStage.SelectedIndex >= 0)
            {
                Utilitaires.ExtraireIdStage(cboStage.Text, out codeCompetence, out idStage);
                unStage = StageDAO.GetStage(codeCompetence, idStage);
                cboCompetence.Text = codeCompetence;
                txtNumStage.Text = idStage.ToString();
                txtNomStage.Text = unStage.GetNomStage();
                txtDuree.Text = unStage.GetDuree().ToString();
                // Affichage du prix sous la forme d'une chaine de caractères avec un point décimal au lieu d'une virgule
                txtPrix.Text = unStage.GetPrix().Replace(",", ".");
                txtNbPlaces.Text = unStage.GetNbPlaces().ToString();
                // Selon le type de stage
                if (unStage is StageEtale)
                {
                    optEtale.Checked = true;
                    optGroupe.Checked = false;
                    cboJour.Text = unStage.GetJour();
                    lblJour.Visible = true;
                    cboJour.Visible = true;
                    txtNbHeures.Clear();
                    lblNbHeures.Visible = false;
                    txtNbHeures.Visible = false;
                }
                else
                {
                    optGroupe.Checked = true;
                    optEtale.Checked = false;
                    // Affichage du nombre d'heures sous la forme d'une chaine de caractères avec un point décimal au lieu d'une virgule
                    txtNbHeures.Text = unStage.GetNbHeures().Replace(",", ".");
                    lblNbHeures.Visible = true;
                    txtNbHeures.Visible = true;
                    cboJour.SelectedIndex = -1;
                    lblJour.Visible = false;
                    cboJour.Visible = false;
                }
            }
            else // remise à vide des zones 
            {
                txtNumStage.Clear();
                txtNomStage.Clear();
                txtDuree.Clear();
                txtPrix.Clear();
                txtNbPlaces.Clear();
                optEtale.Checked = false;
                lblJour.Visible = false;
                cboJour.SelectedIndex = -1;
                cboJour.Visible = false;
                optGroupe.Checked = false;
                lblNbHeures.Visible = false;
                txtNbHeures.Clear();
                txtNbHeures.Visible = false;
                cboCompetence.SelectedIndex = -1;
                cboStage.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Ouverture du formulaire frmAffecterModule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAffecterModule_Click(object sender, EventArgs e)
        {
            int idStage;
            string idCompetence;
            Stage unStage;

            if (cboStage.SelectedIndex >= 0)
            {
                idStage = int.Parse(txtNumStage.Text);
                idCompetence = cboCompetence.Text;

                unStage = StageDAO.GetStage(idCompetence, idStage);

                frmAffecterModule mod = new frmAffecterModule(unStage);
                mod.Show();
            }
            else
            {

                if (!txtNumStage.Text.Equals(""))
                {
                    unStage = StageDAO.GetStage(cboCompetence.Text, int.Parse(txtNumStage.Text));

                    frmAffecterModule mod = new frmAffecterModule(unStage);
                    mod.Show();
                }
                else
                {
                    MessageBox.Show("Aucun stage choisi dans la liste", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }
     }
}
