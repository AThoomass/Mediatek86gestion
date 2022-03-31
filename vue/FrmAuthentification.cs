using System;
using System.Windows.Forms;
using Mediatek86.controleur;
using Mediatek86.metier;

/// <summary>
/// Les vues de l'application
/// </summary>
namespace Mediatek86.vue
{
    /// <summary>
    /// Vue pour l'authentification de l'utilisateur
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        /// <summary>
        /// Instance du contrôleur
        /// </summary>
        private readonly Controle controle;

        /// <summary>
        /// Booléen, true si l'authentification a réussi
        /// </summary>
        public bool AuthentificationSuccess { get; private set; }

        /// <summary>
        /// Constructeur, initialise le composant et valorise la propriété contrôleur avec le contrôleur reçu en paramètre
        /// </summary>
        /// <param name="controle">Instance du contrôleur</param>
        public FrmAuthentification(Controle controle)
        {
            InitializeComponent();
            this.controle = controle;
        }

        /// <summary>
        /// Clic sur le bouton valider vérifie si l'utilisateur et mdp sont correctes avant d'actionner le booléen authentification succes et de fermer la fenêtre
        /// L'accès à l'application n'est pas authorisé pour le service 'Culture'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuthValider_Click(object sender, EventArgs e)
        {
            string utilisateur = txbAuthUtilisateur.Text.Trim();
            string mdp = txbAuthMdp.Text.Trim();
            Service leService = controle.Authentification(utilisateur, mdp);
            if (leService != null)
            {
                if (leService.Libelle == "culture")
                {
                    MessageBox.Show("Votre service n'a pas accès à cette application.", "Information");
                    ViderChamps();
                }
                else
                {
                    AuthentificationSuccess = true;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrecte(s)", "Erreur");
                ViderChamps();
            }
        }

        /// <summary>
        /// Clic sur le bouton annuler ferme l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuthAnnuler_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Vide les champs et met le focus sur le champ nom d'utilisateur
        /// </summary>
        private void ViderChamps()
        {
            txbAuthUtilisateur.Text = "";
            txbAuthMdp.Text = "";
            txbAuthUtilisateur.Focus();
        }

        /// <summary>
        /// Entrée dans le champ déclenche la validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbAuthMdp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAuthValider_Click(sender, e);
            }
        }
    }
}
