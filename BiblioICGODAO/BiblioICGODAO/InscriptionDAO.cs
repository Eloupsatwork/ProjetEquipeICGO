using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BiblioICGO;
using System.Runtime.Remoting.Channels;


namespace BiblioICGODAO
{
    public class InscriptionDAO
    {
        /// <summary>
        /// Ajouter une inscription dans la table INSCRIRE
        /// </summary>
        /// <param name="uneInscription">Une inscription</param>
        public static void AjouterUneInscription(Inscription uneInscription)
        {

            // Exécuter la requête d'insertion
            string requete = "INSERT INTO INSCRIRE VALUES ( " + uneInscription.GetLeStagiaire().GetNumStagiaire() + ", '" + uneInscription.GetLaSession().GetLeStage().GetLaCompetence().GetCodeCompetence() + "', " + uneInscription.GetLaSession().GetLeStage().GetNumStage() + ", " + uneInscription.GetLaSession().GetNumSession() + ", '" + uneInscription.GetEtatInscription() + "')";
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Confirmer une inscription : état définitif 
        /// </summary>
        /// <param name="uneInscription">Une inscription</param>
        public static void ConfirmerInscription(Inscription uneInscription)
        {

            // Exécuter la requête de mise a jour
            string requete = "UPDATE INSCRIRE SET ETATINSCRIPTION = 'D' WHERE NUMSTAGIAIRE = " + uneInscription.GetLeStagiaire().GetNumStagiaire();
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Supprimer une inscription identifiée par une session et un stagiaire
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <param name="idSession">Numéro session</param>
        /// <param name="idStagiaire">Numéro stagiaire</param>
        public static void SupprimerUneInscription(string idCompetence, int idStage, int idSession, int idStagiaire)
        {

            // Exécuter la requête de suppression
            string requete = "DELETE FROM INSCRIRE WHERE NUMSTAGIAIRE = " + idStagiaire + " AND CODECOMPETENCE = '" + idCompetence + "' AND NUMSTAGE = " + idStage + " AND NUMSESSION = " + idSession;
            Connexion.ExecuterRequeteMaj(requete);
        }

        /// <summary>
        /// Vérifier si l'inscription d'un stagiaire à une session est possible
        /// </summary>
        /// <param name="idCompetence">Code compétence</param>
        /// <param name="idStage">Numéro stage</param>
        /// <param name="idSession">Numéro session</param>
        /// <returns></returns>
        public static bool VerifierPlacesDisponibles(string idCompetence, int idStage, int idSession)
        {
            bool disponible=false;

            string requete = "DECLARE	@return_value int EXEC	@return_value = [dbo].[verifierPlaces] @codeCompetence = N'" + idCompetence + "', @noStage = " + idStage + ", @noSession = " + idSession + " SELECT	'Return Value' = @return_value";
            DataTable resultat = Connexion.ExecuterRequete(requete);
            if (resultat.Rows[0][0].ToString() == "1")
            {
                disponible = true;
            }
            return disponible;
        }
    }
}
