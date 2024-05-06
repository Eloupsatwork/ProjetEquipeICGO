using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BiblioICGO;
using BiblioICGODAO;
using System.Collections.Generic;

namespace TestSessionDAO
{
    [TestClass]
    public class TestSessionDAO
    {
        [TestMethod]
        public void TestGetSession()
        {
            Connexion.OuvrirConnexion();
            Session uneSession = SessionDAO.GetSession("BUR", 1, 1);
            Assert.AreEqual(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence(), "BUR");
            Assert.AreEqual(uneSession.GetNumSession(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestAjouterSession()
        {
            Connexion.OuvrirConnexion();
            Competence uneCompetence = new Competence("BUR", "Bureautique");
            StageGroupe unStage = new StageGroupe(1, "Bureautique Libre Office", 5, "850", 10, uneCompetence, "7");
            Agence uneAgence = new Agence("ICGO AUXERRE");
            Formateur unFormateur = FormateurDAO.GetFormateur(1);
            DateTime uneDate = new DateTime(2019, 10, 20);
            Session uneSession = new Session(1, uneDate, unStage, unFormateur, uneAgence);
            SessionDAO.AjouterUneSession(uneSession);
            Session uneSessionInseree = SessionDAO.GetSession("BUR", 1, 1);
            Assert.AreEqual(uneSessionInseree.GetNumSession(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestChargerLesSessions()
        {
            Connexion.OuvrirConnexion();
            List<Session> lesSessions = SessionDAO.ChargerLesSessions();
            Session uneSession = lesSessions[0];
            Assert.AreEqual(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence(), "BUR");
            Assert.AreEqual(uneSession.GetNumSession(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestModifierSession()
        {
            Connexion.OuvrirConnexion();
            Competence uneCompetence = new Competence("BUR", "Bureautique");
            StageGroupe unStage = new StageGroupe(1, "Bureautique Libre Office", 5, "850", 10, uneCompetence, "7");
            Agence uneAgence = new Agence("ICGO AUXERRE");
            Formateur unFormateur = FormateurDAO.GetFormateur(1);
            DateTime uneDate = new DateTime(2019, 10, 22);
            Session uneSession = new Session(1, uneDate, unStage, unFormateur, uneAgence);
            SessionDAO.ModifierUneSession(uneSession, "BUR", 1, 1);
            Session uneSessionModifiee = SessionDAO.GetSession("BUR", 1, 1);
            Assert.AreEqual(uneSessionModifiee.GetDateSession(), uneDate);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestSupprimerSession()
        {
            Connexion.OuvrirConnexion();
            SessionDAO.SupprimerUneSession("BUR", 1, 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestChargerLesSessionsDuStagiaire()
        {
            Connexion.OuvrirConnexion();
            List<Session> lesSessions = SessionDAO.ChargerLesSessionsDuStagiaire(2);
            Session uneSession = lesSessions[0];
            Assert.AreEqual(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence(), "BUR");
            Assert.AreEqual(uneSession.GetNumSession(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestChargerLesSessionsNonChoisiesDuStagiaire()
        {
            Connexion.OuvrirConnexion();
            List<Session> lesSessions = SessionDAO.ChargerLesSessionsNonChoisiesDuStagiaire(2);
            Session uneSession = lesSessions[0];
            Assert.AreEqual(uneSession.GetLeStage().GetLaCompetence().GetCodeCompetence(), "BDD");
            Assert.AreEqual(uneSession.GetNumSession(), 1);
            Connexion.FermerConnexion();
        }
    }
}
