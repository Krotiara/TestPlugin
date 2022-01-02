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
        private readonly Document document;
        private readonly List<Element> documentElements;
        private readonly SortedList<string, Category> categories;

        public DocumentDataService(Document document)
        {
            this.document = document;
            documentElements = new FilteredElementCollector(document, document.ActiveView.Id)
                .ToElements()
                .ToList();
            categories = GetCategories();
        }

        public SortedList<string, Category> GetCategories()
        {
            SortedList<string, Category> categories = new SortedList<string, Category>();
            foreach(Element element in documentElements)
            {
                Category category = element.Category;
                if (category == null) continue; //TODO 02.01.2022 Разобраться почему выскакивают с null
                if (category.CategoryType == CategoryType.Model && !categories.ContainsKey(category.Name))
                // Выбираем только категории модели ( TODO - Это то же самое, что только те, которые имеют представление в 3д модели?)
                {
                    categories[category.Name] = category;
                }
            }
            return categories;
        }

        public SortedList<string, Parameter> GetCategoryParameters(string categoryName)
        {
            if (!categories.ContainsKey(categoryName))
                return null;
            SortedList<string, Parameter> parameters = new SortedList<string, Parameter>();
            Category category = categories[categoryName];
            BuiltInCategory builtInCategory = (BuiltInCategory)category.Id.IntegerValue;
            Element element = new FilteredElementCollector(document).OfCategory(builtInCategory).First();
            foreach(Parameter parameter in element.Parameters)
            {
                parameters[parameter.Definition.Name] = parameter;
            }
            return parameters;     
        }
    }
}
