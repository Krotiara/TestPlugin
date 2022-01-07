using Autodesk.Revit.DB;
using System;
using TestPlugin.Models;

namespace TestPlugin.ViewModels
{
    public class ParameterChangingViewModel
    {
        public ParameterChangingViewModel(string parameterName, string parameterCategoryName, CategoriesModel categoriesModel)
        {
            this.categoriesModel = categoriesModel;
            ParameterName = parameterName;
            ParameterCategoryName = parameterCategoryName;
            storageParameterType = categoriesModel.GetCurrentCategoryParameterType(parameterName);
            ParameterType = storageParameterType.ToString();
            //ParameterValue = categoriesModel.GetCurrentCategoryParameterValue(parameterName);
        }

        private readonly CategoriesModel categoriesModel;


        #region Binding properties
        public string ParameterName { get; set; }

        public string ParameterCategoryName { get; set; }
        public string ParameterType { get; set; }
        private StorageType storageParameterType;
        public string ParameterValue { get; set; }

        #endregion


        #region Commands   
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
                    case StorageType.ElementId:  //Пока не придумал, как проверить ElementId
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
        #endregion
    }
}
