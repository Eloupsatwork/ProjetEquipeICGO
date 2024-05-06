using BiblioICGO;
using BiblioICGODAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestModuleDAO
{
    [TestClass]
    public class TestModuleDAO
    {
        [TestMethod]
        public void TestGetModule()
        {
            Connexion.OuvrirConnexion();
            Module unModule = ModuleDAO.GetModule(1);
            Assert.AreEqual(unModule.GetNumModule(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestAjouterModule()
        {
            
            Connexion.OuvrirConnexion();
            Module unModule = new Module(1, "Traitements de texte Writer", "Writer.odt", "Writer.odp", "D:\\Cours\\Texte", "D:\\Cours\\Diapo");
            ModuleDAO.AjouterUnModule(unModule);
            Module unModuleInsere = ModuleDAO.GetModule(1);
            Assert.AreEqual(unModuleInsere.GetNumModule(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestChargerLesModules()
        {
            Connexion.OuvrirConnexion();
            List<Module> lesModules = ModuleDAO.ChargerLesModules();
            Module unModule = lesModules[0];
            Assert.AreEqual(unModule.GetNumModule(), 1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestModifierModule()
        {
            Connexion.OuvrirConnexion();
            Module unModule = new Module(1, "Traitement de texte Writer", "Writer.odt", "Writer.odp", "D:\\Cours\\Texte", "D:\\Cours\\Diapo");
            ModuleDAO.ModifierUnModule(unModule, 1);
            Module unModuleModifie = ModuleDAO.GetModule(1);
            Assert.AreEqual(unModuleModifie.GetNomModule(), "Traitement de texte Writer");
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestSupprimerModule()
        {
             Connexion.OuvrirConnexion();
            ModuleDAO.SupprimerUnModule(1);
            Connexion.FermerConnexion();
        }

        [TestMethod]
        public void TestChargerLesModulesDuStage()
        {
            Connexion.OuvrirConnexion();
            Competence uneCompetence = new Competence("BUR", "Bureautique");
            StageGroupe unStage = new StageGroupe(1, "Bureautique Libre Office", 5, "850", 10, uneCompetence, "7");
            StageDAO.AjouterUnModule(unStage, ModuleDAO.GetModule(1));
            List<Module> lesModules = ModuleDAO.ChargerLesModulesDuStage("BUR", 1);
            Module unModule = lesModules[0];
            Assert.AreEqual(unModule.GetNumModule(), 1);
            Connexion.FermerConnexion();
        }
    }
}
