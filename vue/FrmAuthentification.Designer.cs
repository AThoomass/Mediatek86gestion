namespace Mediatek86.vue
{
    partial class FrmAuthentification
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
            this.btnAuthAnnuler = new System.Windows.Forms.Button();
            this.btnAuthValider = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbAuthMdp = new System.Windows.Forms.TextBox();
            this.txbAuthUtilisateur = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAuthAnnuler
            // 
            this.btnAuthAnnuler.Location = new System.Drawing.Point(238, 67);
            this.btnAuthAnnuler.Name = "btnAuthAnnuler";
            this.btnAuthAnnuler.Size = new System.Drawing.Size(122, 23);
            this.btnAuthAnnuler.TabIndex = 11;
            this.btnAuthAnnuler.Text = "Annuler";
            this.btnAuthAnnuler.UseVisualStyleBackColor = true;
            this.btnAuthAnnuler.Click += new System.EventHandler(this.btnAuthAnnuler_Click);
            // 
            // btnAuthValider
            // 
            this.btnAuthValider.Location = new System.Drawing.Point(109, 67);
            this.btnAuthValider.Name = "btnAuthValider";
            this.btnAuthValider.Size = new System.Drawing.Size(122, 23);
            this.btnAuthValider.TabIndex = 10;
            this.btnAuthValider.Text = "Valider";
            this.btnAuthValider.UseVisualStyleBackColor = true;
            this.btnAuthValider.Click += new System.EventHandler(this.btnAuthValider_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Mot de passe :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nom d\'utilisateur :";
            // 
            // txbAuthMdp
            // 
            this.txbAuthMdp.Location = new System.Drawing.Point(109, 41);
            this.txbAuthMdp.Name = "txbAuthMdp";
            this.txbAuthMdp.Size = new System.Drawing.Size(251, 20);
            this.txbAuthMdp.TabIndex = 7;
            this.txbAuthMdp.UseSystemPasswordChar = true;
            this.txbAuthMdp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbAuthMdp_KeyDown);
            // 
            // txbAuthUtilisateur
            // 
            this.txbAuthUtilisateur.Location = new System.Drawing.Point(109, 15);
            this.txbAuthUtilisateur.Name = "txbAuthUtilisateur";
            this.txbAuthUtilisateur.Size = new System.Drawing.Size(251, 20);
            this.txbAuthUtilisateur.TabIndex = 6;
            // 
            // FrmAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 105);
            this.Controls.Add(this.btnAuthAnnuler);
            this.Controls.Add(this.btnAuthValider);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbAuthMdp);
            this.Controls.Add(this.txbAuthUtilisateur);
            this.Name = "FrmAuthentification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAuthentification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAuthAnnuler;
        private System.Windows.Forms.Button btnAuthValider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbAuthMdp;
        private System.Windows.Forms.TextBox txbAuthUtilisateur;
    }
}