using TechTalk.SpecFlow;
using Mediatek86.vue;
using Mediatek86.controleur;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecFlowMediatek86.Steps
{
    [Binding]
    public class RechercheDvdNumeroSteps
    {
        private readonly FrmMediatek frmMediatek = new FrmMediatek(new Controle());

        [Given(@"Je saisis la valeur (.*)")]
        public void GivenJeSaisisLaValeur(string valeur)
        {
            TextBox TxtValeur = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabDvd"].Controls["grpDvdRecherche"].Controls["txbDvdNumRecherche"];
            frmMediatek.Visible = true;
            TxtValeur.Text = valeur;
        }

        [When(@"Je clic sur le bouton Rechercher")]
        public void WhenJeClicSurLeBoutonRechercher()
        {
            Button BtnRechercher = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabDvd"].Controls["grpDvdRecherche"].Controls["btnDvdNumRecherche"];
            frmMediatek.Visible = true;
            BtnRechercher.PerformClick();
        }

        [Then(@"Les informations détaillées doivent afficher le titre (.*)")]
        public void ThenLesInformationsDetailleesDoiventAfficherLeTitre(string titreAttendu)
        {
            TextBox TxtTitre = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabDvd"].Controls["grpDvdInfos"].Controls["txbDvdTitre"];
            string titreObtenu = TxtTitre.Text;
            Assert.AreEqual(titreAttendu, titreObtenu);
        }

    }
}
