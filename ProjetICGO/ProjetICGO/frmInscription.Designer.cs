namespace ProjetICGO
{
    partial class frmInscription
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFermer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboStagiaire = new System.Windows.Forms.ComboBox();
            this.btnStagiaire = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSession = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboEtat = new System.Windows.Forms.ComboBox();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(1499, 584);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(227, 93);
            this.btnFermer.TabIndex = 0;
            this.btnFermer.Text = "&Fermer";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(179, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(309, 39);
            this.label3.TabIndex = 59;
            this.label3.Text = "Choisir un stagiaire";
            // 
            // cboStagiaire
            // 
            this.cboStagiaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStagiaire.FormattingEnabled = true;
            this.cboStagiaire.Location = new System.Drawing.Point(539, 95);
            this.cboStagiaire.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cboStagiaire.Name = "cboStagiaire";
            this.cboStagiaire.Size = new System.Drawing.Size(593, 46);
            this.cboStagiaire.TabIndex = 58;
            this.cboStagiaire.SelectedIndexChanged += new System.EventHandler(this.cboStagiaire_SelectedIndexChanged);
            this.cboStagiaire.Click += new System.EventHandler(this.cboStagiaire_Click);
            // 
            // btnStagiaire
            // 
            this.btnStagiaire.Location = new System.Drawing.Point(1499, 79);
            this.btnStagiaire.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnStagiaire.Name = "btnStagiaire";
            this.btnStagiaire.Size = new System.Drawing.Size(373, 86);
            this.btnStagiaire.TabIndex = 60;
            this.btnStagiaire.Text = "Créer stagiaire";
            this.btnStagiaire.UseVisualStyleBackColor = true;
            this.btnStagiaire.Click += new System.EventHandler(this.btnStagiaire_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 272);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 39);
            this.label1.TabIndex = 64;
            this.label1.Text = "Choisir une session";
            // 
            // cboSession
            // 
            this.cboSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSession.FormattingEnabled = true;
            this.cboSession.Location = new System.Drawing.Point(539, 265);
            this.cboSession.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cboSession.Name = "cboSession";
            this.cboSession.Size = new System.Drawing.Size(841, 46);
            this.cboSession.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(176, 422);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 39);
            this.label4.TabIndex = 66;
            this.label4.Text = "Choisir un état";
            // 
            // cboEtat
            // 
            this.cboEtat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEtat.FormattingEnabled = true;
            this.cboEtat.Location = new System.Drawing.Point(539, 415);
            this.cboEtat.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cboEtat.Name = "cboEtat";
            this.cboEtat.Size = new System.Drawing.Size(593, 46);
            this.cboEtat.TabIndex = 65;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(965, 584);
            this.btnAnnuler.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(237, 91);
            this.btnAnnuler.TabIndex = 70;
            this.btnAnnuler.Text = "A&nnuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(347, 591);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(237, 86);
            this.btnAjouter.TabIndex = 67;
            this.btnAjouter.Text = "&Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // frmInscription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1923, 808);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboEtat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboSession);
            this.Controls.Add(this.btnStagiaire);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboStagiaire);
            this.Controls.Add(this.btnFermer);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "frmInscription";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inscription d\'un stagiaire";
            this.Load += new System.EventHandler(this.frmInscription_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboStagiaire;
        private System.Windows.Forms.Button btnStagiaire;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSession;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboEtat;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnAjouter;
    }
}