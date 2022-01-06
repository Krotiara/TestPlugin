using TestPlugin.Models;

namespace TestPlugin.ViewModels
{
    public class ParameterChangingViewModel
    {
        public string ParameterName { get; set; }

        public string ParameterCategoryName { get; set; }
        public string ParameterType { get; set; }
        public string ParameterValue { get; set; }

        private CategoriesModel categoriesModel;

        public ParameterChangingViewModel(string parameterName, string parameterCategoryName, CategoriesModel categoriesModel)
        {
            this.categoriesModel = categoriesModel;
            ParameterName = parameterName;
            ParameterCategoryName = parameterCategoryName;
            ParameterType = categoriesModel.GetCurrentCategoryParameterType(parameterName);
            ParameterValue = categoriesModel.GetCurrentCategoryParameterValue(parameterName);
        }

        public RelayCommand ChangeParameterValueCommand
        {
            get
            {
                return new RelayCommand(parameterValue =>
                categoriesModel.SetParamValueOnCategoryElements(ParameterCategoryName, ParameterName, (string)parameterValue));
            }
        }


    }
}
