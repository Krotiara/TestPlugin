using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Models
{
    public class DocumentDataService : IDocumentDataService
    {
        private readonly FilteredElementCollector activeViewElementsCollector;
        private readonly SortedList<int, Category> categories;

        public Document Document { get; }

        public DocumentDataService(Document document)
        {
            Document = document;
            activeViewElementsCollector = new FilteredElementCollector(document, document.ActiveView.Id);
            categories = GetCategories();
        }

        public SortedList<int, Category> GetCategories()
        {
            SortedList<int, Category> categories = new SortedList<int, Category>();
            foreach(Element element in activeViewElementsCollector)
            {
                Category category = element.Category;
                if (category == null) continue; //TODO 02.01.2022 Разобраться почему выскакивают с null
                // Выбираем только категории элементов модели модели
                if (category.CategoryType == CategoryType.Model && !categories.ContainsKey(category.Id.IntegerValue))
                {
                    categories[category.Id.IntegerValue] = category;
                }
            }
            return categories;
        }

        public SortedList<string, Parameter> GetCategoryParameters(int categoryId)
        {
            if (!categories.ContainsKey(categoryId))
                return null;

            SortedList<string, Parameter> parameters = new SortedList<string, Parameter>();
            Category category = categories[categoryId];
            BuiltInCategory builtInCategory = (BuiltInCategory)category.Id.IntegerValue;
 
            //заполнение имен всех параметров элементов выбранной категории
            foreach (Element e in activeViewElementsCollector.OfCategory(builtInCategory))
                foreach (Parameter parameter in e.Parameters)
                    if (!parameters.ContainsKey(parameter.Definition.Name))
                        parameters[parameter.Definition.Name] = parameter;
            return parameters;
        }
    }
}
