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
    public partial class frmConnexion : Form
    {
        private frmMenu fm;

        public frmConnexion()
        {
            InitializeComponent();
            // Connexion à la base de données 
            try
            {
                Connexion.OuvrirConnexion();
            }
            catch
            {
                MessageBox.Show("Impossible de se connecter à la base de données.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {   
            // Fermer la connexion à la base de données
            Connexion.FermerConnexion();
            this.Close();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            // Vérifier si l'utilisateur existe
            if (UtilisateurDAO.VerifierUtilisateur(txtLogin.Text, txtMotPasse.Text))
            {
                // Afficher le menu de l'application
                fm = new frmMenu(this);
                fm.Show();
                // Cacher le formulaire de connexion
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login et/ou mot de passe incorrects", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Clear();
                txtMotPasse.Clear();
            }

        }
    }
}
