using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;

namespace BiblioICGODAO
{
    public class UtilisateurDAO
    {
        /// <summary>
        /// Vérifier qu'un utilisateur existe
        /// </summary>
        /// <param name="unLogin">Login</param>
        /// <param name="unMotPasse">Mot de passe</param>
        /// <returns></returns>
        public static Boolean VerifierUtilisateur(string unLogin, string unMotPasse)
        {
            Boolean existe = false;
            int nb;

            // Exécuter la requête de sélection
            string requete = "SELECT COUNT(*) AS NB FROM UTILISATEUR WHERE LOGIN = '" + unLogin + "' AND MOTPASSE = '" + unMotPasse + "'";
            DataTable dt = Connexion.ExecuterRequete(requete);

            // Parcours du résultat de la requête
            if (dt.Rows.Count == 1)
            {
                nb = int.Parse(dt.Rows[0]["NB"].ToString());
                if (nb == 1)
                {
                    existe = true;
                }
            }
            return existe;
        }
    }
}
