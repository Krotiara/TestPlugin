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
        private SortedDictionary<string, Category> categoriesNamesDict;


        public StorageType GetCurrentCategoryParameterType(string parameterName) => currentCategoryParameters[parameterName].StorageType;


        public string GetCurrentCategoryParameterValue(string parameterName) => currentCategoryParameters[parameterName].AsValueString();


        private Category GetCategoryByName(string categoryName) => categoriesNamesDict[categoryName];


        private Parameter GetParameterByName(string parameterName) => currentCategoryParameters[parameterName];


        /// <summary>
        /// Метод загружает категории и возвращает список имен этих категорий.
        /// </summary>
        public List<string> LoadCategories()
        {
            categories = documentDataService.GetCategories();
            categoriesNamesDict = new SortedDictionary<string, Category>();
            foreach (Category category in categories.Values)
                categoriesNamesDict[category.Name] = category;
            return categoriesNamesDict.Keys.ToList();
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
                bool isSettingSuccess = false;
                foreach (Element element in activeCategoryElements)
                {
                    try
                    {
                        Parameter parameter = element.get_Parameter(settingParameterDefinition);
                        isSettingSuccess = SetParameterValue(parameter, parameterValue);
                        if (!isSettingSuccess)
                        {
                            //Сюда логично добавить log.
                        }
                    }
                    catch (Exception e)
                    {
                        // Сюда логично добавить log. Пока оставлена просто заглужка на пропуск ошибки.
                        continue;
                    }
                }
                transaction.Commit();
            }
        }


        private bool SetParameterValue(Parameter parameter, string valueString)
        {
            bool isSuccess = true;
            StorageType storageType = parameter.StorageType;
            switch(storageType)
            {
                // SetValueString не хочет устанавливать yesno parameter type и strings, поэтому через Set.
                case StorageType.Integer:
                    isSuccess = parameter.Set(int.Parse(valueString));
                    break;
                case StorageType.ElementId:
                    int id = int.Parse(valueString);
                    isSuccess = parameter.Set(new ElementId(id));
                    break;
                case StorageType.String:
                    isSuccess = parameter.Set(valueString);
                    break;
                case StorageType.Double:
                    isSuccess = parameter.Set(double.Parse(valueString));
                    break;
                default:
                    isSuccess = false;
                    break;
            }
            return isSuccess;
        }


       
    }
}
