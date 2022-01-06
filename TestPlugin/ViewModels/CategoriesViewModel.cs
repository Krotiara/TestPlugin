using System.Collections.Generic;
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
            CategoriesNames = categoriesModel.LoadCategories();
        }


        #region Binding properties
        private string selectedCategoryName;
        public string SelectedCategoryName
        {
            get => selectedCategoryName;
            set
            {
                selectedCategoryName = value;
                NotifyPropertyChanged("SelectedCategoryName");
                SetCategoryParams();
            }
        }

        public void SetCategoryParams()
        {
            ParametersNames = categoriesModel.LoadCategoryParams(SelectedCategoryName);
            NotifyPropertyChanged("ParametersNames");
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
        #endregion


        #region commands       
        public RelayCommand OpenChangeParameterViewCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ChangeParameterView view = new ChangeParameterView(SelectedParameterName, SelectedCategoryName, categoriesModel);
                    view.ShowDialog();
                });
            }
        }
        #endregion
    }
}
