using Mediatek86.metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediatek86.vue
{
    public partial class FrmMediatek : Form
    {

        //-----------------------------------------------------------
        // ONGLET "ABONNEMENTS REVUE"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet : blocage en saisie des champs de saisie des infos de l'abonnement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabAbonnementRevue_Enter(object sender, EventArgs e)
        {
            CancelAllSaisies();
            lesDvd = controle.GetAllDvd();
            txbAbonnementRevueNumeroRevue.Text = "";
            VideAbonnementRevueInfos();
            // accesCommandeDvdGroupBox(false);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirAbonnementRevueListe(List<Abonnement> lesAbonnements)
        {

            bdgAbonnementRevueListe.DataSource = lesAbonnements;
            dgvAbonnementRevueListe.DataSource = bdgAbonnementRevueListe;
            dgvAbonnementRevueListe.Columns["id"].Visible = false;
            dgvAbonnementRevueListe.Columns["idRevue"].Visible = false;
            dgvAbonnementRevueListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAbonnementRevueListe.Columns["dateCommande"].DisplayIndex = 0;
            dgvAbonnementRevueListe.Columns["montant"].DisplayIndex = 1;
            dgvAbonnementRevueListe.Columns[3].HeaderCell.Value = "Date commande";
            dgvAbonnementRevueListe.Columns[0].HeaderCell.Value = "Date fin abonnement";
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbonnementRevueRechercher_Click(object sender, EventArgs e)
        {
            if (!txbAbonnementRevueNumeroRevue.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbAbonnementRevueNumeroRevue.Text.Trim()));
                if (revue != null)
                {
                    AfficheAbonnementRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    VideAbonnementRevueInfos();
                }
            }
            else
            {
                VideAbonnementRevueInfos();
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone d'abonnements est vidée et inactive
        /// les informations de la revue sont aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbAbonnementRevueNumeroRevue_TextChanged(object sender, EventArgs e)
        {
            // accesCommandeDvdGroupBox(false);
            VideAbonnementRevueInfos();
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheAbonnementRevueInfos(Revue revue)
        {
            // informations sur l'abonnement
            txbAbonnementRevueTitre.Text = revue.Titre;
            txbAbonnementRevuePeriodicite.Text = revue.Periodicite;
            txbAbonnementRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbAbonnementRevueGenre.Text = revue.Genre;
            txbAbonnementRevuePublic.Text = revue.Public;
            txbAbonnementRevueRayon.Text = revue.Rayon;
            txbAbonnementRevueImage.Text = revue.Image;
            chkAbonnementRevueEmpruntable.Checked = revue.Empruntable;
            string image = revue.Image;
            try
            {
                pcbAbonnementRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbAbonnementRevueImage.Image = null;
            }
            // affiche la liste des commandes du DVD
            AfficheAbonnementsRevue();

            // accès à la zone d'ajout d'un exemplaire
            // accesAbonnementRevueGroupBox(true);
        }

        /// <summary>
        /// Récupération de la liste des abonnement à une revue puis affichage dans la liste
        /// </summary>
        private void AfficheAbonnementsRevue()
        {
            string idDocument = txbAbonnementRevueNumeroRevue.Text.Trim();
            lesAbonnements = controle.GetAbonnement(idDocument);
            RemplirAbonnementRevueListe(lesAbonnements);
        }

        /// <summary>
        /// Vide les zones d'affchage des informations de la revue
        /// </summary>
        private void VideAbonnementRevueInfos()
        {
            txbAbonnementRevueTitre.Text = "";
            txbAbonnementRevuePeriodicite.Text = "";
            txbAbonnementRevueDelaiMiseADispo.Text = "";
            txbAbonnementRevueGenre.Text = "";
            txbAbonnementRevuePublic.Text = "";
            txbAbonnementRevueRayon.Text = "";
            txbAbonnementRevueImage.Text = "";
            chkAbonnementRevueEmpruntable.Checked = false;
            pcbAbonnementRevueImage.Image = null;
            lesAbonnements = new List<Abonnement>();
            RemplirAbonnementRevueListe(lesAbonnements);
            // accesAbonnementRevueGroupBox(false);
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void dgvAbonnementRevueListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* string titreColonne = dgvCommandeLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Commande> sortedList = new List<Commande>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirCommandeLivresListe(sortedList);*/
        }
    }
}
