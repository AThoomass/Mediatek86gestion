﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

/// <summary>
/// Connexion à et interactions avec la base de données
/// </summary>
namespace Mediatek86.bdd
{
    /// <summary>
    /// Classe qui gère la connexion avec la base de données
    /// </summary>
    public class BddMySql
    {
        /// <summary>
        /// Unique instance de la classe
        /// </summary>
        private static BddMySql instance = null;
        
        /// <summary>
        /// objet de connexion à la BDD à partir d'une chaîne de connexion
        /// </summary>
        private readonly MySqlConnection connection;

        /// <summary>
        /// objet contenant le résultat d'une requête "select" (curseur)
        /// </summary>
        private MySqlDataReader reader;

        /// <summary>
        /// Constructeur privé pour créer la connexion à la BDD et l'ouvrir
        /// </summary>
        /// <param name="stringConnect">Chaine de connexion</param>
        private BddMySql(string stringConnect)
        {
            try
            {
                connection = new MySqlConnection(stringConnect);
                connection.Open();
            }
            catch (MySqlException e)
            {
                ErreurGraveBddNonAccessible(e);
            }
        }

        /// <summary>
        /// Crée une instance unique de la classe
        /// </summary>
        /// <param name="stringConnect">Chaine de connexion</param>
        /// <returns>Instance unique de la classe</returns>
        public static BddMySql GetInstance(string stringConnect)
        {
            if (instance is null)
            {
                instance = new BddMySql(stringConnect);
            }
            return instance;
        }

        /// <summary>
        /// Exécute une requête type "select" et valorise le curseur
        /// </summary>
        /// <param name="stringQuery">Requête select</param>
        public void ReqSelect(string stringQuery, Dictionary<string, object> parameters)
        {
            MySqlCommand command;
            
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                reader = command.ExecuteReader();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidOperationException e)
            {
                ErreurGraveBddNonAccessible(e);
            }
        }

        /// <summary>
        /// Tente de lire la ligne suivante du curseur
        /// </summary>
        /// <returns>False si fin de curseur atteinte</returns>
        public bool Read()
        {
            if (reader is null)
            {
                return false;
            }
            try
            {
                return reader.Read();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne le contenu d'un champ dont le nom est passé en paramètre
        /// </summary>
        /// <param name="nameField">Nom du champ</param>
        /// <returns>Valeur du champ</returns>
        public object Field(string nameField)
        {
            if (reader is null)
            {
                return null;
            }
            try
            {
                return reader[nameField];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Exécution de plusieurs requêtes autre que "select" dans une seul transaction
        /// </summary>
        /// <param name="queries">Liste de requêtes à faire</param>
        /// <param name="parameters">Dictionnire contenant les parametres</param>
        public void ReqUpdate(List<string> queries, Dictionary<string, object> parameters)
        {
            MySqlCommand command;
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (string stringQuery in queries)
                {
                    command = new MySqlCommand(stringQuery, connection, transaction);
                    if (!(parameters is null))
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                        }
                    }
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                Console.WriteLine(e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                ErreurGraveBddNonAccessible(e);
            }
        }

        /// <summary>
        /// Fermeture du curseur
        /// </summary>
        public void Close()
        {
            if (!(reader is null))
            {
                reader.Close();
            }
        }

        /// <summary>
        /// Pas d'accès à la BDD : arrêt de l'application
        /// </summary>
        /// <param name="e"></param>
        private void ErreurGraveBddNonAccessible(Exception e)
        {
            MessageBox.Show("Base de données non accessibles", "Erreur grave");
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
    }
}
