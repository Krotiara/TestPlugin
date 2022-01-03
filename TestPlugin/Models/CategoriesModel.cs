using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Models
{
    public class CategoriesModel : Notifier
    {

        public CategoriesModel(IDocumentDataService documentDataService)
        {
            this.documentDataService = documentDataService;
        }

        private readonly IDocumentDataService documentDataService;
        
        private SortedList<string, Category> categories;
        private SortedList<string, Parameter> currentCategoryParameters;
       

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


        /// <summary>
        /// Метод загружает параметры переданной категории и возвращает лист имен этих параметров.
        /// </summary>
        /// <param name="categoryName">Имя категории</param>
        /// <returns></returns>
        public List<string> LoadCategoryParams(string categoryName)
        {
            currentCategoryParameters = documentDataService.GetCategoryParameters(categoryName);
            return currentCategoryParameters.Keys.ToList();
        }

        public void SetParamValueOnCategoryElements(string category, string parameterName, string parameterValue)
        {
            throw new NotImplementedException();
        }

    }
}
