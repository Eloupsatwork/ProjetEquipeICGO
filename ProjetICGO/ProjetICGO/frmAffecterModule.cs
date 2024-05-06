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
    public partial class frmAffecterModule : Form
    {
        // Déclaration d'un objet dynamic qui sera soit un stage étalé soit un stage groupé lors de l'exécution
        private Stage unStage;
        /// <summary>
        /// Constructeur du formulaire
        /// </summary>
        /// <param name="leStage">Stage transmis par le formulaire frmStage</param>
        public frmAffecterModule(Stage stage)
        {
            InitializeComponent();
            // Valorisation de l'objet unStage
            unStage = stage;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAffecterModule_Load(object sender, EventArgs e)
        {
            txtCodeCompetence.Text = unStage.GetLaCompetence().GetCodeCompetence();
            txtNomStage.Text = unStage.GetNomStage();
            txtNumStage.Text = unStage.GetNumStage().ToString();

            foreach (Module unModule in unStage.GetLesModules())
                dgvModule.Rows.Add(unModule.GetNumModule(), unModule.GetNomModule());

            ChargerListeModules();
        }

        /// <summary>
        /// Ajout d'un module choisi dans lstModule au stage et mise à jour du datagrid dgvCompetence et de la base de données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            int i;
            int numModule;
            Module unModule;

            for (i = 0; i <= lstModule.Items.Count - 1; i++)
            {
                // Si une compétence est sélectionnée
                if (lstModule.GetSelected(i))
                {
                    numModule = int.Parse(lstModule.Items[i].ToString());
                    unModule = ModuleDAO.GetModule(numModule);
                    txtCodeCompetence.Text = unModule.GetNumModule().ToString() + ", " + unStage.GetLaCompetence().GetCodeCompetence() + ", " + unStage.GetNumStage().ToString();
                    StageDAO.AjouterUnModule(unStage, unModule);
                    // Ajout de cette compétence dans le datagrid dgvCompetence
                    dgvModule.Rows.Add(numModule, unModule.GetNomModule());
                }
            }
            // Mise à jour de la liste lstCompetence
            ChargerListeModules();


        }

        /// <summary>
        /// Valorisation de la liste lstModule
        /// </summary>
        private void ChargerListeModules()
        {

            List<Module> lesModules = new List<Module>();
            Boolean trouve;
            int i;
            Module unModule;

            lstModule.Items.Clear();


            unStage.SetLesModules(ModuleDAO.ChargerLesModulesDuStage(unStage.GetLaCompetence().GetCodeCompetence(), unStage.GetNumStage()));
            // Chargement de l'ensemble des modules
            lesModules = ModuleDAO.ChargerLesModules();
            // Parcours de l'ensemble des modules existantes dans la base de données
            foreach (Module leModule in lesModules)
            {
                trouve = false;
                i = 0;
                // Recherche les modules qui n'ont pas été attribuées au stage
                while ((i <= unStage.GetLesModules().Count - 1) && (!trouve))
                {
                    // un module du stage
                    unModule = unStage.GetLesModules()[i];
                    if (leModule.GetNumModule().Equals(unModule.GetNumModule()))
                    {
                        trouve = true;
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
                // Si un module n'a pas été attribuée au stage, ajout de ce module à la liste lstModule
                if (!trouve)
                {
                    lstModule.Items.Add(leModule.GetNumModule());
                }
            }

        }

        /// <summary>
        /// Suppression d'un module d'un stage en cliquant sur le bouton supprimer (X)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvModule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult reponse;
            int numModule;
            Module unModule;
            int index;
            DataGridViewRow uneLigne;

            if (dgvModule.SelectedCells.Count == 1)
            {
                if (dgvModule.CurrentCell.ColumnIndex == 2)
                {
                    reponse = MessageBox.Show("Etes vous sûr de vouloir supprimer ce module ?", "Suppression d'un module", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (reponse == DialogResult.Yes)
                    {
                        index = dgvModule.CurrentCell.RowIndex;
                        uneLigne = dgvModule.Rows[index];
                        // Récupération du code compétence de la ligne sélectionnée
                        numModule = int.Parse(uneLigne.Cells["colNumModule"].Value.ToString());
                        unModule = ModuleDAO.GetModule(numModule);
                        // Supprimer la compétence de la base de données
                        StageDAO.SupprimerUnModule(unStage, unModule);
                        // Supprimer la compétence du datagrid
                        dgvModule.Rows.Remove(uneLigne);
                        // Recharger la liste des compétences lstcompetence avec les compétences non attribuées au formateur
                        ChargerListeModules();
                    }
                }
            }
        }

        /// <summary>
        /// Suppression de modules (sélection de un ou plusieurs) d'un stage par l'intermédiaire de la touche SUPPR du clavier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvModule_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult reponse;
            int numModule;
            Module unModule;

            reponse = MessageBox.Show("Etes vous sûr de vouloir supprimer ce module ?", "Suppression d'un module", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (reponse == DialogResult.Yes)
            {
                foreach (DataGridViewRow uneLigne in dgvModule.SelectedRows)
                {
                    // Récupération du code compétence de la ligne sélectionnée
                    numModule = int.Parse(uneLigne.Cells["colNumModule"].Value.ToString());
                    unModule = ModuleDAO.GetModule(numModule);
                    // Supprimer la compétence de la base de données
                    StageDAO.SupprimerUnModule(unStage, unModule);
                }
                // Recharger la liste des compétences lstcompetence avec les compétences non attribuées au formateur
                ChargerListeModules();
            }
        }
    }
}
