using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlugin.Models;

namespace TestPlugin.ViewModels
{
    class CategoriesViewModel: Notifier
    {
        private CategoriesModel categoriesModel;

        public List<string> CategoriesNames { get; set; }
        private string selectedCategoryName;
        public string SelectedCategoryName
        {
            get => selectedCategoryName; 
            set
            {
                selectedCategoryName = value;
                SelectCategory();
                NotifyPropertyChanged("SelectedCategoryName");
                NotifyPropertyChanged("ParametersNames");
            }
        }

        public List<string> ParametersNames { get; set; }

        public string SelectedParameterName { get; set; }
        
        public CategoriesViewModel(IDocumentDataService documentDataService)
        {
            categoriesModel = new CategoriesModel(documentDataService);
            CategoriesNames = categoriesModel.GetCategoriesNames();
        }

        public void SelectCategory()
        {
            categoriesModel.SetCurrentCategoryName(SelectedCategoryName);
            ParametersNames = categoriesModel.GetCurrentCategoryParamsNames();
        }





    }
}
