﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BiblioICGODAO
{
    public class Connexion
    {

        private static SqlConnection connexion;

        #region Accesseur

        /// <summary>
        /// Retourne la connexion
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnexion()
        {
            return connexion;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Ouverture de la connexion à la base de données
        /// </summary>
        public static void OuvrirConnexion()
        {
            connexion = new SqlConnection(@"Data Source={votre_serveur};Initial Catalog={nom_DB};Integrated Security=True");
            connexion.Open();
        }

        /// <summary>
        /// Fermeture de la connexion
        /// </summary>
        public static void FermerConnexion()
        {
            connexion.Close();
        }

        /// <summary>
        /// Exécution d'une requête de consultation
        /// </summary>
        /// <param name="requete">Requête</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuterRequete(string requete)
        {
            // Utilisation d'un DataAdapter
            SqlDataAdapter unDataAdapter = new SqlDataAdapter(requete, connexion);
            // Remplissage du DataSet avec le resultat du DataAdapter
            DataSet unDataSet = new DataSet();
            unDataAdapter.Fill(unDataSet);
            //Selection du 1er DataTable
            DataTable unDataTable = unDataSet.Tables[0];
            return unDataTable;
        }

        /// <summary>
        /// Exécution d'une requête de mise à jour
        /// </summary>
        /// <param name="requete">Requête</param>
        public static void ExecuterRequeteMaj(string requete)
        {   // type de maj : insertion, suppression, modification
            SqlCommand cmdMaj = new SqlCommand(requete, connexion);
            cmdMaj.ExecuteNonQuery();
        }

        #endregion
    }
}
