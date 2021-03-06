using Mediatek86.metier;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

/// <summary>
/// Les vues de l'application
/// </summary>
namespace Mediatek86.vue
{
    /// <summary>
    /// Classe partielle représentant l'onglet de commande de livres
    /// </summary>
    public partial class FrmMediatek : Form
    {
        //-----------------------------------------------------------
        // ONGLET "COMMANDE DE LIVRES"
        //-----------------------------------------------------------

        /// <summary>
        /// Boolean true si on est en train de faire une saisie de commande de livre
        /// </summary>
        private bool saisieCommandeLivre = false;

        /// <summary>
        /// Ouverture de l'onglet :
        /// Tous les booléens concernant une saisie sont mis en false (validation d'abandon a été demandé avant changement d'onglet)
        /// Récupération des livres et suivis depuis le contrôleur
        /// Désactivation de groupBox de gestion de commandes
        /// Vide les champs des infos des livres et des détails de commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeLivre_Enter(object sender, EventArgs e)
        {
            CancelAllSaisies();
            lesLivres = controle.GetAllLivres();
            lesSuivis = controle.GetAllSuivis();
            AccesGestionCommandeLivresGroupBox(false);
            txbCommandeLivreNumero.Text = "";
            VideCommandeLivresInfos();
            VideDetailsCommandeLivres();
        }

        /// <summary>
        /// Remplit le dategrid avec la collection reçue en paramètre
        /// </summary>
        /// <param name="lesCommandeDocument">La collection de CommandeDocument</param>
        private void RemplirCommandeLivresListe(List<CommandeDocument> lesCommandeDocument)
        {

            bdgCommandesLivreListe.DataSource = lesCommandeDocument;
            dgvCommandeLivreListe.DataSource = bdgCommandesLivreListe;
            dgvCommandeLivreListe.Columns["id"].Visible = false;
            dgvCommandeLivreListe.Columns["idSuivi"].Visible = false;
            dgvCommandeLivreListe.Columns["idLivreDvd"].Visible = false;
            dgvCommandeLivreListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCommandeLivreListe.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCommandeLivreListe.Columns[6].DefaultCellStyle.Format = "c2";
            dgvCommandeLivreListe.Columns[6].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-FR");
            dgvCommandeLivreListe.Columns["dateCommande"].DisplayIndex = 0;
            dgvCommandeLivreListe.Columns["montant"].DisplayIndex = 1;
            dgvCommandeLivreListe.Columns[5].HeaderCell.Value = "Date";
            dgvCommandeLivreListe.Columns[0].HeaderCell.Value = "Exemplaires";
            dgvCommandeLivreListe.Columns[2].HeaderCell.Value = "Etat";
        }

        /// <summary>
        /// Recherche d'un numéro de livre et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreRechercher_Click(object sender, EventArgs e)
        {
            if (saisieCommandeLivre && VerifAbandonSaisie())
            {
                FinSaisieCommandeLivres();
                CommandeLivresRechercher();
            }
            else if (!saisieCommandeLivre)
            {
                CommandeLivresRechercher();
            }
        }

        /// <summary>
        /// Recherche d'un numéro de livre et affiche ses informations
        /// </summary>
        private void CommandeLivresRechercher()
        {
            if (!txbCommandeLivreNumero.Text.Equals(""))
            {
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbCommandeLivreNumero.Text.Trim()));
                if (livre != null)
                {
                    AfficheCommandeLivreInfos(livre);
                }
                else
                {
                    MessageBox.Show("Numéro introuvable");
                    txbCommandeLivreNumero.Text = "";
                    txbCommandeLivreNumero.Focus();
                    VideCommandeLivresInfos();
                }
            }
            else
            {
                VideCommandeLivresInfos();
            }
        }

        /// <summary>
        /// Entrée dans champ de recherche déclenche la recherche aussi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandeLivreNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCommandeLivreRechercher_Click(sender, e);
            }
        }

        /// <summary>
        /// Si le numéro de livre est modifié, la zone de commande est vidée et inactive
        /// les informations du livre son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandeLivreNumero_TextChanged(object sender, EventArgs e)
        {
            if (!saisieCommandeLivre)
            {
                AccesGestionCommandeLivresGroupBox(false);
                VideCommandeLivresInfos();
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné et les commandes
        /// </summary>
        /// <param name="livre">Le livre sélectionné</param>
        private void AfficheCommandeLivreInfos(Livre livre)
        {
            // informations sur le livre
            txbCommandeLivreTitre.Text = livre.Titre;
            txbCommandeLivreAuteur.Text = livre.Auteur;
            txbCommandeLivreCollection.Text = livre.Collection;
            txbCommandeLivreGenre.Text = livre.Genre;
            txbCommandeLivrePublic.Text = livre.Public;
            txbCommandeLivreRayon.Text = livre.Rayon;
            txbCommandeLivreImage.Text = livre.Image;
            txbCommandeLivreISBN.Text = livre.Isbn;
            string image = livre.Image;
            try
            {
                pcbCommandeLivreImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCommandeLivreImage.Image = null;
            }
            // affiche la liste des commandes du livre
            AfficheCommandeDocumentLivre();

            // accès à la zone d'ajout d'une commande
            AccesGestionCommandeLivresGroupBox(true);
        }

        /// <summary>
        /// Affichage des détails d'une commande
        /// </summary>
        /// <param name="commandeDocument">Le CommandeDocument concernée</param>
        private void AfficheCommandeLivresCommande(CommandeDocument commandeDocument)
        {
            txbCommandeLivreNumeroCommande.Text = commandeDocument.Id;
            dtpCommandeLivreDate.Value = commandeDocument.DateCommande;
            nudCommandeLivreNombreExemplaire.Value = commandeDocument.NbExemplaires;
            txbCommandeLivreMontant.Text = commandeDocument.Montant.ToString("C2",
                  CultureInfo.CreateSpecificCulture("fr-FR"));
        }

        /// <summary>
        /// Récupération de la liste de commandes d'un livre puis affichage dans la liste
        /// </summary>
        private void AfficheCommandeDocumentLivre()
        {
            string idDocument = txbCommandeLivreNumero.Text.Trim();
            lesCommandeDocument = controle.GetCommandeDocument(idDocument);
            RemplirCommandeLivresListe(lesCommandeDocument);
        }

        /// <summary>
        /// Vide les zones d'affchage des informations du livre
        /// </summary>
        private void VideCommandeLivresInfos()
        {
            txbCommandeLivreTitre.Text = "";
            txbCommandeLivreAuteur.Text = "";
            txbCommandeLivreCollection.Text = "";
            txbCommandeLivreGenre.Text = "";
            txbCommandeLivrePublic.Text = "";
            txbCommandeLivreRayon.Text = "";
            txbCommandeLivreImage.Text = "";
            txbCommandeLivreISBN.Text = "";
            pcbCommandeLivreImage.Image = null;
            lesCommandeDocument = new List<CommandeDocument>();
            RemplirCommandeLivresListe(lesCommandeDocument);
            AccesGestionCommandeLivresGroupBox(false);
        }

        /// <summary>
        /// Vide les zones d'affichage des détails de commande.
        /// </summary>
        private void VideDetailsCommandeLivres()
        {
            txbCommandeLivreNumeroCommande.Text = "";
            dtpCommandeLivreDate.Value = DateTime.Now;
            nudCommandeLivreNombreExemplaire.Value = 1;
            txbCommandeLivreMontant.Text = "";
        }

        /// <summary>
        /// (Dés)active la zone de gestion de commandes
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">'True' autorise l'accès</param>
        private void AccesGestionCommandeLivresGroupBox(bool acces)
        {
            grpGestionCommandeLivre.Enabled = acces;
            btnCommandeLivreAjouter.Enabled = acces;
        }


        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandeLivreListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandeLivreListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = SortCommandeDocumentList(titreColonne);
            RemplirCommandeLivresListe(sortedList);
        }

        /// <summary>
        /// Evénement sélection d'une ligne dans la liste des commandes
        /// Vérifie si une saisie est en cours avant de procéder
        /// Demande validation d'abandon si une saisie est en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandeLivreListe_SelectionChanged(object sender, EventArgs e)
        {
            if (saisieCommandeLivre)
            {
                if (VerifAbandonSaisie())
                {
                    FinSaisieCommandeLivres();
                    CommandeLivresListeSelection();
                }
            }
            else
            {
                CommandeLivresListeSelection();
            }
        }

        /// <summary>
        /// Affichage des infos de la commande sélectionnée dans la liste
        /// </summary>
        private void CommandeLivresListeSelection()
        {
            if (dgvCommandeLivreListe.CurrentCell != null)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivreListe.List[bdgCommandesLivreListe.Position];
                AfficheCommandeLivresCommande(commandeDocument);
                ActivationModificationCommandeLivres(commandeDocument);
            }
            else
            {
                DesactivationModificationCommandeLivres();
                VideDetailsCommandeLivres();
            }
        }

        /// <summary>
        /// Activation des boutons de gestion de commande en fonction de l'état de suivi
        /// </summary>
        /// <param name="commandeDocument">La CommandeDocument concernée</param>
        private void ActivationModificationCommandeLivres(CommandeDocument commandeDocument)
        {
            string etatSuivi = commandeDocument.LibelleSuivi;
            switch (etatSuivi)
            {
                case "En cours":
                case "Relancée":
                    btnCommandeLivreRelancer.Enabled = true;
                    btnCommandeLivreConfirmerLivraison.Enabled = true;
                    btnCommandeLivreRegler.Enabled = false;
                    btnCommandeLivreSupprimer.Enabled = true;
                    break;
                case "Livrée":
                    btnCommandeLivreRelancer.Enabled = false;
                    btnCommandeLivreConfirmerLivraison.Enabled = false;
                    btnCommandeLivreRegler.Enabled = true;
                    btnCommandeLivreSupprimer.Enabled = false;
                    break;
                case "Réglée":
                    DesactivationModificationCommandeLivres();
                    break;
            }
        }

        /// <summary>
        /// Désactivation des boutons de gestion de commande (sauf ajout)
        /// </summary>
        private void DesactivationModificationCommandeLivres()
        {
            btnCommandeLivreRelancer.Enabled = false;
            btnCommandeLivreConfirmerLivraison.Enabled = false;
            btnCommandeLivreRegler.Enabled = false;
            btnCommandeLivreSupprimer.Enabled = false;
        }

        /// <summary>
        /// Début de saisie de commande de livre 
        /// </summary>
        private void DebutSaisieCommandeLivres()
        {
            AccesSaisieCommandeLivre(true);
        }

        /// <summary>
        /// Fin de saisie de commande de livre
        /// Affiche les informations de la commande sélectionnée dans la liste
        /// </summary>
        private void FinSaisieCommandeLivres()
        {
            AccesSaisieCommandeLivre(false);
            CommandeLivresListeSelection();
        }

        /// <summary>
        /// Actionne le booleen saisieCommandeLivres
        /// Vide les champs de détails d'une commande
        /// (Dés)active la protection readonly des champs de détails de commande
        /// (Dés)active les boutons concernant l'ajout, validation et annulation de saisie de commande
        /// </summary>
        /// <param name="acces">'True' active les boutons 'Valider' et 'Annuler', désactive le bouton 'Ajouter', déverrouille les champs des détails de commande</param>
        private void AccesSaisieCommandeLivre(bool acces)
        {
            saisieCommandeLivre = acces;
            VideDetailsCommandeLivres();
            btnCommandeLivreValider.Enabled = acces;
            btnCommandeLivreAnnuler.Enabled = acces;
            btnCommandeLivreAjouter.Enabled = !acces;
            txbCommandeLivreNumeroCommande.Enabled = acces;
            dtpCommandeLivreDate.Enabled = acces;
            nudCommandeLivreNombreExemplaire.Enabled = acces;
            txbCommandeLivreMontant.Enabled = acces;
            grpCommandeLivre.Enabled = acces;
        }

        /// <summary>
        /// Evénement clic sur le bouton d'ajout de commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreAjouter_Click(object sender, EventArgs e)
        {
            DesactivationModificationCommandeLivres();
            DebutSaisieCommandeLivres();
        }

        /// <summary>
        /// Evénement clic sur le bouton d'annulation d'une saisie de commande
        /// Demande validation de l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreAnnuler_Click(object sender, EventArgs e)
        {
            if (VerifAbandonSaisie())
            {
                FinSaisieCommandeLivres();
            }
        }

        /// <summary>
        /// Evénement clic sur le bouton de validation d'une commande
        /// Vérifie si tous les champs sont remplis et la validité du champ 'montant'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreValider_Click(object sender, EventArgs e)
        {
            if (txbCommandeLivreNumeroCommande.Text == "" || txbCommandeLivreMontant.Text == "")
            {
                MessageBox.Show("Tous les champs sont obligatoires.", "Information");
                return;
            }

            String id = txbCommandeLivreNumeroCommande.Text;
            DateTime dateCommande = dtpCommandeLivreDate.Value;
            int nbExemplaires = (int)nudCommandeLivreNombreExemplaire.Value;
            string idLivreDvd = txbCommandeLivreNumero.Text.Trim();
            int idSuivi = lesSuivis[0].Id;
            string libelleSuivi = lesSuivis[0].Libelle;

            String montantSaisie = txbCommandeLivreMontant.Text.Replace(',', '.');
            bool success = Double.TryParse(montantSaisie, out double montant);
            if (!success)
            {
                MessageBox.Show("La valeur saisie pour le montant doit être numérique.", "Erreur");
                txbCommandeLivreMontant.Text = "";
                txbCommandeLivreMontant.Focus();
                return;
            }

            CommandeDocument laCommandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaires, idSuivi, libelleSuivi, idLivreDvd);

            String message = controle.CreerCommandeDocument(laCommandeDocument);
            if (message.Substring(0, 2) == "OK")
            {
                MessageBox.Show("Commande validée!", "Information");
            }
            else if (message.Substring(0, 9) == "Duplicate")
            {
                MessageBox.Show("Ce numéro de commande existe déjà.", "Erreur");
                txbCommandeLivreNumeroCommande.Text = "";
                txbCommandeLivreNumeroCommande.Focus();
                return;
            }
            else
            {
                MessageBox.Show(message, "Erreur");
                return;
            }
            FinSaisieCommandeLivres();
            AfficheCommandeDocumentLivre();
        }

        /// <summary>
        /// Evénement clic sur le bouton de suppression d'une commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreSupprimer_Click(object sender, EventArgs e)
        {
            if (ValidationSuppressionCommande())
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivreListe.List[bdgCommandesLivreListe.Position];
                if (controle.SupprCommandeDocument(commandeDocument.Id))
                {
                    AfficheCommandeDocumentLivre();
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite.", "Erreur");
                }
            }
        }

        /// <summary>
        /// Modification d'état de suivi d'une commande de livre : étape 1 "relancée"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreRelancer_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivreListe.List[bdgCommandesLivreListe.Position];
            Suivi nouveauSuivi = lesSuivis.Find(suivi => suivi.Libelle == "Relancée");
            ModifEtatSuiviCommandeDocumentLivre(commandeDocument.Id, nouveauSuivi);
        }

        /// <summary>
        /// Modification d'état de suivi d'une commande de livre : étape 2 "livrée"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreConfirmerLivraison_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivreListe.List[bdgCommandesLivreListe.Position];
            Suivi nouveauSuivi = lesSuivis.Find(suivi => suivi.Libelle == "Livrée");
            if (ModifEtatSuiviCommandeDocumentLivre(commandeDocument.Id, nouveauSuivi))
            {
                MessageBox.Show("Les exemplaires ont été ajoutés dans la base de données.", "Information");
            }
        }

        /// <summary>
        /// Modification d'état de suivi d'une commande de livre : étape 3 "réglée"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreRegler_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivreListe.List[bdgCommandesLivreListe.Position];
            Suivi nouveauSuivi = lesSuivis.Find(suivi => suivi.Libelle == "Réglée");
            ModifEtatSuiviCommandeDocumentLivre(commandeDocument.Id, nouveauSuivi);
        }

        /// <summary>
        /// Demande de modification de l'état de suivi au contrôleur après validation utilisateur
        /// </summary>
        /// <param name="idCommandeDocument">Identifiant du document concerné</param>
        /// <param name="nouveauSuivi">Nouvel état de suivi</param>
        /// <returns>True si modification a réussi</returns>
        private bool ModifEtatSuiviCommandeDocumentLivre(string idCommandeDocument, Suivi nouveauSuivi)
        {
            if (ValidationModifEtatSuivi(nouveauSuivi.Libelle))
            {
                if (controle.ModifSuiviCommandeDocument(idCommandeDocument, nouveauSuivi.Id))
                {
                    AfficheCommandeDocumentLivre();
                    return true;
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite.", "Erreur");
                    return false;
                }
            }
            return false;
        }
    }
}
