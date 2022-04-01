using TechTalk.SpecFlow;
using Mediatek86.vue;
using Mediatek86.controleur;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecFlowMediatek86.Steps
{
    [Binding]
    public class ChercherLivrePuisAjouterCommandeSteps
    {
        private readonly FrmMediatek frmMediatek = new FrmMediatek(new Controle());

        [Given(@"Je saisie la valeur (.*)")]
        public void GivenJeSaisieLaValeur(string numeroLivre)
        {
            TextBox TxbNumeroLivre = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommmandeLivreRecherche"].Controls["txbCommandeLivreNumero"];
            frmMediatek.Visible = true;
            TxbNumeroLivre.Text = numeroLivre;
        }
        
        [Given(@"Je clique sur le bouton Rechercher")]
        public void GivenJeCliqueSurLeBoutonRechercher()
        {
            Button BtnRechercher = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommmandeLivreRecherche"].Controls["btnCommandeLivreRechercher"];
            frmMediatek.Visible = true;
            BtnRechercher.PerformClick();
        }
        
        [Given(@"Je clique sur le bouton Ajouter")]
        public void GivenJeCliqueSurLeBoutonAjouter()
        {
            Button BtnAjouter = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpGestionCommandeLivre"].Controls["btnCommandeLivreAjouter"];
            frmMediatek.Visible = true;
            BtnAjouter.PerformClick();
        }
        
        [Given(@"Je saisie le numéro de commande (.*)")]
        public void GivenJeSaisieLeNumeroDeCommande(string numeroCommande)
        {
            TextBox TxbNumeroCommande = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommandeLivre"].Controls["txbCommandeLivreNumeroCommande"];
            frmMediatek.Visible = true;
            TxbNumeroCommande.Text = numeroCommande;
        }
        
        [Given(@"Je saisie le nombre d'exemplaire à (.*)")]
        public void GivenJeSaisieLeNombreDExemplaireA(int nombreExemplaire)
        {
            NumericUpDown NudNombreExemplaire = (NumericUpDown)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommandeLivre"].Controls["nudCommandeLivreNombreExemplaire"];
            frmMediatek.Visible = true;
            NudNombreExemplaire.Value = nombreExemplaire;
        }
        
        [Given(@"Je saisie le montant à (.*)")]
        public void GivenJeSaisieLeMontantA(string montant)
        {
            TextBox TxbMontant = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommandeLivre"].Controls["txbCommandeLivreMontant"];
            frmMediatek.Visible = true;
            TxbMontant.Text = montant;
        }
        
        [When(@"Je clique sur le bouton Valider")]
        public void WhenJeCliqueSurLeBoutonValider()
        {
            Button BtnValider = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommandeLivre"].Controls["btnCommandeLivreValider"];
            frmMediatek.Visible = true;
            BtnValider.PerformClick();
        }
        
        [Then(@"Le détail de la commande doit afficher le numéro de commande (.*)")]
        public void ThenLeDetailDeLaCommandeDoitAfficherLeNumeroDeCommande(string numeroCommandeAttentdu)
        {
            TextBox TxbNumeroCommande = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabCommandeLivre"].Controls["grpCommandeLivre"].Controls["txbCommandeLivreNumeroCommande"];
            string numeroCommandeObtenu = TxbNumeroCommande.Text;
            Assert.AreEqual(numeroCommandeAttentdu, numeroCommandeObtenu);
        }
    }
}
