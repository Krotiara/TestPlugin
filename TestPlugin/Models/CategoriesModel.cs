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
        
        private SortedList<int, Category> categories;
        private SortedList<string, Parameter> currentCategoryParameters;
        private Dictionary<string, Category> categoriesNamesDict;



        public StorageType GetCurrentCategoryParameterType(string parameterName)
        {
            return currentCategoryParameters[parameterName].StorageType;
        }


        public string GetCurrentCategoryParameterValue(string parameterName)
        {
            return currentCategoryParameters[parameterName].AsValueString();
        }


        /// <summary>
        /// Метод загружает категории и возвращает список имет этих категорий.
        /// </summary>
        public List<string> LoadCategories()
        {
            categories = documentDataService.GetCategories();
            categoriesNamesDict = new Dictionary<string, Category>();
            foreach (Category category in categories.Values)
                categoriesNamesDict[category.Name] = category;
            return categories.Values.Select(x => x.Name).ToList();
        }


        /// <summary>
        /// Метод загружает параметры переданной категории и возвращает лист имен этих параметров.
        /// </summary>
        /// <param name="categoryName">Имя категории</param>
        /// <returns></returns>
        public List<string> LoadCategoryParams(string categoryName)
        {
            Category category = GetCategoryByName(categoryName);
            currentCategoryParameters = documentDataService.GetCategoryParameters(category.Id.IntegerValue);
            return currentCategoryParameters.Keys.ToList();
        }

        /// <summary>
        /// Метод устанавливает значение parameterValue  параметра с именем parameterName для всех элементов категории с именем categoryName.
        /// </summary>
        /// <param name="categoryName">Имя категории</param>
        /// <param name="parameterName">Имя параметра</param>
        /// <param name="parameterValue">Значение параметра</param>
        public void SetParamValueOnCategoryElements(string categoryName, string parameterName, string parameterValue)
        {
            Category category = GetCategoryByName(categoryName);
            ElementId activeViewId = documentDataService.Document.ActiveView.Id;
            BuiltInCategory builtInCategory = (BuiltInCategory)category.Id.IntegerValue;
            Definition settingParameterDefinition = GetParameterByName(parameterName).Definition;
            FilteredElementCollector activeCategoryElements = new FilteredElementCollector(documentDataService.Document, activeViewId)
                .OfCategory(builtInCategory);
            using (Transaction transaction = new Transaction(documentDataService.Document, "Change category parameters"))
            {
                transaction.Start();
                try
                {
                    foreach (Element element in activeCategoryElements)
                    {
                        Parameter parameter = element.get_Parameter(settingParameterDefinition);
                        bool isSuccess = parameter.SetValueString(parameterValue);
                    }
                }
                catch(Exception e)
                {
                    transaction.RollBack();
                    throw; //Обработка ошибок todo
                }
                transaction.Commit();
            }
        }

        private Category GetCategoryByName(string categoryName)
        {
            //Add validation todo
            return categoriesNamesDict[categoryName];
        }


        private Parameter GetParameterByName(string parameterName)
        {
            //Add validation todo
            return currentCategoryParameters[parameterName];
        }

    }
}
