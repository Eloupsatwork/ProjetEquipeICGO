using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioICGO;
using BiblioICGODAO;

namespace TestInscriptionDAO
{
    [TestClass]
    public class TestInscriptionDAO
    {
        [TestMethod]
        public void TestAjouterInscription()
        {
            Connexion.OuvrirConnexion();
            Session uneSession = SessionDAO.GetSession("BUR", 1, 1);
            Stagiaire unStagiaire = StagiaireDAO.GetStagiaire(2);
            Inscription uneInscription = new Inscription(uneSession, unStagiaire, "p");
            InscriptionDAO.AjouterUneInscription(uneInscription);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestConfirmerInscription()
        {
            Connexion.OuvrirConnexion();
            Session uneSession = SessionDAO.GetSession("BUR", 1, 1);
            Stagiaire unStagiaire = StagiaireDAO.GetStagiaire(2);
            Inscription uneInscription = new Inscription(uneSession, unStagiaire, "d");
            InscriptionDAO.ConfirmerInscription(uneInscription);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestSupprimerInscription()
        {
            Connexion.OuvrirConnexion();
            InscriptionDAO.SupprimerUneInscription("BUR", 1, 1, 2);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestVerifierPlacesDispo()
        {
            Connexion.OuvrirConnexion();
            Boolean dispo = InscriptionDAO.VerifierPlacesDisponibles("BUR", 1, 1);
            Assert.AreEqual(dispo, true);
            Connexion.FermerConnexion();
        }
    }
}
