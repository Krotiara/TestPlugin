using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Models
{
    class CategoriesModel : Notifier
    {

        public CategoriesModel(IDocumentDataService documentDataService)
        {
            this.documentDataService = documentDataService;
        }

        private readonly IDocumentDataService documentDataService;
        private SortedList<string, Category> categories;
        private SortedList<string, Parameter> currentCategoryParameters;

        private string currentCategoryName;
        public void SetCurrentCategoryName(string categoryName)
        {
            currentCategoryName = categoryName;
        }

        public string GetCurrentCategoryParameterType(string parameterName)
        {
            return currentCategoryParameters[parameterName].StorageType.ToString();
        }

        public string GetCurrentCategoryParameterValue(string parameterName)
        {
            return currentCategoryParameters[parameterName].AsValueString();
        }

        public List<string> GetCategoriesNames()
        {
            categories = documentDataService.GetCategories();         
            return categories.Keys.ToList();
        }

        public List<string> GetCurrentCategoryParamsNames()
        {
            currentCategoryParameters = documentDataService.GetCategoryParameters(currentCategoryName);
            return currentCategoryParameters.Keys.ToList();
        }

        public void SetParamValueOnCurrentCategoryElements(string parameterName, string parameterValue)
        {
            throw new NotImplementedException();
        }

    }
}
