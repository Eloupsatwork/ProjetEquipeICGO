using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BiblioICGO
{
    public class Module
    {

        #region Attributs privés
        /// <summary>
        /// Attributs privés
        /// </summary>
        int numModule;
        string nomModule;
        string nomSupportCours;
        string nomPresentation;
        string placeSupportCours;
        string placePresentation;
        List<Stage> lesStages;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur
        /// </summary>
        public Module(int NumModule, string NomModule, string NomSupportCours, string NomPresentation, string PlaceSupportCours, string PlacePresentation)
        {
            this.numModule = NumModule;
            this.nomModule = NomModule;
            this.nomSupportCours = NomSupportCours;
            this.nomPresentation = NomPresentation;
            this.placeSupportCours = PlaceSupportCours;
            this.placePresentation = PlacePresentation;
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        public Module()
        {
            this.lesStages = new List<Stage>();
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        public Module(int NumModule, string NomModule, string NomSupportCours, string NomPresentation, string PlaceSupportCours, string PlacePresentation, List<Stage> DesStages)
        {
            this.numModule = NumModule;
            this.nomModule = NomModule;
            this.nomSupportCours = NomSupportCours;
            this.nomPresentation = NomPresentation;
            this.placeSupportCours = PlaceSupportCours;
            this.placePresentation = PlacePresentation;
            this.lesStages = new List<Stage>();
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">numModule</param>
        public int GetNumModule()
        {
            return numModule;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">numModule</param>
        public void SetNumModule(int value)
        {
            numModule = value;
        }
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">nomModule</param>
        public string GetNomModule()
        {
            return nomModule;
        }

        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">nomModule</param>
        public void SetNomModule(string value)
        {
            nomModule = value;
        }
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">NomSupportCours</param>
        public string GetNomSupportCours()
        {
            return nomSupportCours;
        }
        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">NomSupportCours</param>
        public void SetNomSupportCours(string value)
        {
            nomSupportCours= value;
        }
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">NomPresentation</param>
        public string GetNomPresentation()
        {
            return nomPresentation;
        }
        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">NomPresentation</param>
        public void SetNomPresentation(string value)
        {
            nomPresentation = value;
        }
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">PlaceSupportCours</param>
        public string GetPlaceSupportCours()
        {
            return placeSupportCours;
        }
        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">PlaceSupportCours</param>
        public void SetPlaceSupportCours(string value)
        {
            placeSupportCours = value;
        }
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">PlacePresentation</param>
        public string GetPlacePresentation()
        {
            return placePresentation;
        }
        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">PlacePresentation</param>
        public void SetPlacePresentation(string value)
        {
            placePresentation = value;
        }
        /// <summary>
        /// Accesseur Get
        /// </summary>
        /// <param name="value">lesStages</param>
        public List<Stage> GetLesStages()
        {
            return lesStages;
        }
        /// <summary>
        /// Accesseur Set
        /// </summary>
        /// <param name="value">lesStages</param>
        public void SetLesStages(List<Stage> value)
        {
            lesStages = value;
        }
        #endregion

    }
}
