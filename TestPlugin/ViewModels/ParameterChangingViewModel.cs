using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlugin.Models;

namespace TestPlugin.ViewModels
{
    public class ParameterChangingViewModel
    {
        public string ParameterName { get; set; }

        public string ParameterCategory { get; set; }
        public string ParameterType { get; set; }
        public string ParameterValue { get; set; }

        private CategoriesModel categoriesModel;

        public ParameterChangingViewModel(string parameterName, string parameterCategory, CategoriesModel categoriesModel)
        {
            this.categoriesModel = categoriesModel;
            ParameterName = parameterName;
            ParameterCategory = parameterCategory;
            ParameterType = categoriesModel.GetCurrentCategoryParameterType(parameterName);
            ParameterValue = categoriesModel.GetCurrentCategoryParameterValue(parameterName);
        }

        public RelayCommand ChangeParameterValueCommand
        {
            get
            {
                return new RelayCommand(parameterValue =>
                categoriesModel.SetParamValueOnCategoryElements(ParameterCategory, ParameterName, (string)parameterValue));
            }
        }


    }
}
