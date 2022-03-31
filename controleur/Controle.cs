using System.Collections.Generic;
using Mediatek86.modele;
using Mediatek86.metier;
using Mediatek86.vue;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Contrôleur de l'application
/// </summary>
namespace Mediatek86.controleur
{
    /// <summary>
    /// Contrôleur de l'application
    /// </summary>
    public class Controle
    {
        /// <summary>
        /// Collection d'objets Livre
        /// </summary>
        private readonly List<Livre> lesLivres;

        /// <summary>
        /// Collection d'objets Dvd
        /// </summary>
        private readonly List<Dvd> lesDvd;

        /// <summary>
        /// Collection d'objets Revue
        /// </summary>
        private readonly List<Revue> lesRevues;

        /// <summary>
        /// Collection d'objets Rayon dont la classe mère est Categorie
        /// </summary>
        private readonly List<Categorie> lesRayons;

        /// <summary>
        /// Collection d'objets Public dont la classe mère est Categorie
        /// </summary>
        private readonly List<Categorie> lesPublics;

        /// <summary>
        /// Collection d'objets Genre dont la classe mère est Categorie
        /// </summary>
        private readonly List<Categorie> lesGenres;

        /// <summary>
        /// Collection d'objets Suivi
        /// </summary>
        private readonly List<Suivi> lesSuivis;

        /// <summary>
        /// Le service dont dépent l'utilisateur connecté
        /// </summary>
        public Service LeService { get; private set; }

        /// <summary>
        /// Ouverture de la fenêtre d'authentification
        /// Si l'authentification réussi alors overture de l'application
        /// </summary>
        public Controle()
        {
            lesLivres = Dao.GetAllLivres();
            lesDvd = Dao.GetAllDvd();
            lesRevues = Dao.GetAllRevues();
            lesGenres = Dao.GetAllGenres();
            lesRayons = Dao.GetAllRayons();
            lesPublics = Dao.GetAllPublics();
            lesSuivis = Dao.GetAllSuivis();
            FrmAuthentification frmAuthentification = new FrmAuthentification(this);
            Application.Run(frmAuthentification);
            if (frmAuthentification.AuthentificationSuccess)
            {
                FrmMediatek frmMediatek = new FrmMediatek(this);
                Application.Run(frmMediatek);
            }
        }

        /// <summary>
        /// Getter sur la liste des genres
        /// </summary>
        /// <returns>Collection d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return lesGenres;
        }

        /// <summary>
        /// Getter sur la liste des livres
        /// </summary>
        /// <returns>Collection d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return lesLivres;
        }

        /// <summary>
        /// Getter sur la liste des Dvd
        /// </summary>
        /// <returns>Collection d'objets DVD</returns>
        public List<Dvd> GetAllDvd()
        {
            return lesDvd;
        }

        /// <summary>
        /// Getter sur la liste des revues
        /// </summary>
        /// <returns>Collection d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return lesRevues;
        }

        /// <summary>
        /// Getter sur les rayons
        /// </summary>
        /// <returns>Collection d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return lesRayons;
        }

        /// <summary>
        /// Getter sur les publics
        /// </summary>
        /// <returns>Collection d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return lesPublics;
        }

        /// <summary>
        /// Getter sur les suivis
        /// </summary>
        /// <returns>Collection d'objets Suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            return lesSuivis;
        }

        /// <summary>
        /// Récupère les commandes d'un livre ou d'un DVD depuis la bdd
        /// </summary>
        /// <param name="idDocument">Identifiant du livre ou DVD concerné</param>
        /// <returns>Collection d'objets de type CommandeDocument</returns>
        public List<CommandeDocument> GetCommandeDocument(string idDocument)
        {
            return Dao.GetCommandeDocument(idDocument);
        }

        /// <summary>
        /// Récupère les abonnements d'une revue depuis la bdd
        /// </summary>
        /// <param name="idDocument">Identifiant de la revue concerné</param>
        /// <returns>Collection d'objets de type Abonnement</returns>
        public List<Abonnement> GetAbonnement(string idDocument)
        {
            return Dao.GetAbonnement(idDocument);
        }

        /// <summary>
        /// Récupère les abonnements avec une date d'expiration
        /// à moins de 30 jours deplus la bdd
        /// </summary>
        /// <returns>Collection d'objets de type Abonnement</returns>
        public List<FinAbonnement> GetFinAbonnement()
        {
            return Dao.GetFinAbonnement();
        }

        /// <summary>
        /// Récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">Identifiant de la revue concerné</param>
        /// <returns>Collection d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return Dao.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return Dao.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Crée une CommandeDocument dans la bdd
        /// </summary>
        /// <param name="commandeDocument">L'objet CommandeDocument concerné</param>
        /// <returns>Le message de confirmation ou d'erreur</returns>
        public string CreerCommandeDocument(CommandeDocument commandeDocument)
        {
            return Dao.CreerCommandeDocument(commandeDocument);
        }

        /// <summary>
        /// Supprime une CommandeDocument de la bdd
        /// </summary>
        /// <param name="id">Identifiant de la CommandeDocument à supprimer</param>
        /// <returns>True si la suppression a réussi</returns>
        public bool SupprCommandeDocument(string id)
        {
            return Dao.SupprCommandeDocument(id);
        }

        /// <summary>
        /// Modification d'état de suivi d'une CommandeDocument
        /// </summary>
        /// <param name="idCommandeDocument">identifiant de la CommandeDocument à modifier</param>
        /// <param name="idSuivi">identifiant du nouveau état de suivi</param>
        /// <returns>True si la modification a réussi</returns>
        public bool ModifSuiviCommandeDocument(string idCommandeDocument, int idSuivi)
        {
            return Dao.ModifSuiviCommandeDocument(idCommandeDocument, idSuivi);
        }

        /// <summary>
        /// Crée un abonnement dans la bdd
        /// </summary>
        /// <param name="abonnement">L'objet Abonnement concerné</param>
        /// <returns>Le message de confirmation ou d'erreur</returns>
        public string CreerAbonnement(Abonnement abonnement)
        {
            return Dao.CreerAbonnement(abonnement);
        }

        /// <summary>
        /// Récupère les exemplaires rattachés à la revue concerné par un abonnement
        /// puis demande vérification s'ils font partie de l'abonnement
        /// </summary>
        /// <param name="abonnement">Abonnement concerné</param>
        /// <returns>True si un exemplaire est rattaché à l'abonnement</returns>
        public bool VerifSuppressionAbonnement(Abonnement abonnement)
        {
            List<Exemplaire> lesExemplaires = GetExemplairesRevue(abonnement.IdRevue);
            bool parution = false;
            foreach (Exemplaire exemplaire in lesExemplaires.Where(ex => ParutionDansAbonnement(abonnement.DateCommande, abonnement.DateFinAbonnement, ex.DateAchat)))
            {
                parution = true;
            }
            return parution;
        }

        /// <summary>
        /// Vérifie si la dateParution est comprise entre dateCommande et dateFinAbonnement
        /// </summary>
        /// <param name="dateCommande">Date de commande d'un abonnement</param>
        /// <param name="dateFinAbonnement">Date d'expiration d'un abonnement</param>
        /// <param name="dateParution">Date de parution d'un abonnement</param>
        /// <returns>True si la date est comprise</returns>
        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            return (DateTime.Compare(dateCommande, dateParution) < 0 && DateTime.Compare(dateParution, dateFinAbonnement) < 0);
        }

        /// <summary>
        /// Demande la suppression d'un abonnement de la bdd
        /// </summary>
        /// <param name="idAbonnement">Identifiant de l'abonnement concerné</param>
        /// <returns>True si l'opréation réussi</returns>
        public bool SupprAbonnement(string idAbonnement)
        {
            return Dao.SupprAbonnement(idAbonnement);
        }

        /// <summary>
        /// Calcul du hash MD5 d'une chaîne de caractères
        /// </summary>
        /// <param name="mdp">la chaîne d'entrée</param>
        /// <returns>Le hash calculé</returns>
        public string CreateMD5(string mdp)
        {
            // Utilisation du string mdp pour calculer MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(mdp);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Conversion du vecteur vers un string hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Récupère le service de l'utilisateur qui essaye de se connecter depuis la bdd
        /// Valorise la propriété 'service'
        /// </summary>
        /// <param name="utilisateur">L'identifiant de l'utilisateur</param>
        /// <param name="mdp">Le mot de passe de l'utilisateur</param>
        /// <returns>Le service de l'utilisateur s'il est trouvé dans la bdd, et le mdp est correct. Sinon retourne null</returns>
        public Service Authentification(string utilisateur, string mdp)
        {
            Service service = Dao.Authentification(utilisateur, CreateMD5(mdp));
            LeService = service;
            return service;
        }

    }

}

