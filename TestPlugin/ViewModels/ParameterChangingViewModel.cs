using Autodesk.Revit.DB;
using System;
using TestPlugin.Models;

namespace TestPlugin.ViewModels
{
    public class ParameterChangingViewModel
    {
        public string ParameterName { get; set; }

        public string ParameterCategoryName { get; set; }
        public string ParameterType { get; set; }
        private StorageType storageParameterType;
        public string ParameterValue { get; set; }

        private CategoriesModel categoriesModel;

        public ParameterChangingViewModel(string parameterName, string parameterCategoryName, CategoriesModel categoriesModel)
        {
            this.categoriesModel = categoriesModel;
            ParameterName = parameterName;
            ParameterCategoryName = parameterCategoryName;
            storageParameterType = categoriesModel.GetCurrentCategoryParameterType(parameterName);
            ParameterType = storageParameterType.ToString();
            ParameterValue = categoriesModel.GetCurrentCategoryParameterValue(parameterName);
        }

        public RelayCommand ChangeParameterValueCommand
        {
            get
            {
                return new RelayCommand(parameterValue =>
                categoriesModel.SetParamValueOnCategoryElements(ParameterCategoryName, ParameterName, (string)parameterValue),
                parameterValue => IsAbleToChangeParameter((string)parameterValue));
            }
        }

        private bool IsAbleToChangeParameter(string parameterValue)
        {
            try
            {
                switch (storageParameterType)
                {
                    case StorageType.Double:
                        double.Parse(parameterValue);
                        break;
                    case StorageType.Integer:
                    case StorageType.ElementId:
                        int.Parse(parameterValue);
                        break;
                    case StorageType.None:
                        return false;
                    case StorageType.String:
                        return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
