using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlugin.Models;
using TestPlugin.Views;

namespace TestPlugin.ViewModels
{
    public class CategoriesViewModel : Notifier
    {
        private CategoriesModel categoriesModel;

        public List<string> CategoriesNames { get; set; }

        public List<string> ParametersNames { get; set; }

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


        #region Binding properties
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
       

        private string selectedParameterName;
        public string SelectedParameterName
        {
            get => selectedParameterName;
            set
            {
                selectedParameterName = value;
                NotifyPropertyChanged("SelectedParameterName");
            }
        }
        
        public string SelectedParameterType => categoriesModel.GetCurrentCategoryParameterType(SelectedParameterName);
        
        public string SelectedParameterValue => categoriesModel.GetCurrentCategoryParameterValue(SelectedParameterName);
        #endregion


        #region commands       
        public RelayCommand ChangeParameterValueCommand
        {
            get
            {
                return new RelayCommand(parameterValue =>
                categoriesModel.SetParamValueOnCurrentCategoryElements(SelectedParameterName, (string)parameterValue));
            }
        }

        public RelayCommand OpenChangeParameterViewCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ChangeParameterView view = new ChangeParameterView(this);
                    view.ShowDialog();
                });
            }
        }      
        #endregion
    }
}
