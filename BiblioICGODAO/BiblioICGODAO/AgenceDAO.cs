using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class AgenceDAO
    {
        /// <summary>
        /// Ajouter une agence dans la table AGENCE
        /// </summary>
        /// <param name="uneAgence">Une agence</param>
        public static void AjouterUneAgence(Agence uneAgence)
        {
            // Exécuter la requête d'insertion
            string requete = "INSERT INTO AGENCE VALUES ( '" + uneAgence.GetNomAgence() + "')";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Charger les agences de la table AGENCE dans une liste d'agences
        /// </summary>
        /// <returns></returns>
        public static List<Agence> ChargerLesAgences()
        {
            List<Agence> lesAgences = new List<Agence>();
            Agence uneAgence;
            string nomAgence;

            // Exécuter la requête de sélection
            string requete = "SELECT NOMAGENCE FROM AGENCE";
            DataTable dt = Connexion.ExecuterRequete(requete);

            // Parcours du résultat de la requête
            foreach (DataRow uneLigne in dt.Rows)
            {
                // Récupération du nom de l'agence de la ligne 
                nomAgence = uneLigne["NOMAGENCE"].ToString();
                // Construction de l'objet uneAgence
                uneAgence = new Agence(nomAgence);
                // Ajout de l'agence dans la liste lesAgences
                lesAgences.Add(uneAgence);
            }

            // Retour de la liste les Agences
            return lesAgences;
        }

        /// <summary>
        /// Supprimer une agence identifiée dans la table AGENCE
        /// </summary>
        /// <param name="idAgence">Nom agence</param>
        public static void SupprimerUneAgence(string idAgence)
        {
            // Exécuter la requête de suppression
            string requete = "DELETE FROM AGENCE WHERE NOMAGENCE = '" + idAgence + "'";
            Connexion.ExecuterRequeteMaj(requete);
        }
    }
}
