using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace TestPlugin.Models
{
    public interface IDocumentDataService
    {
        Document Document { get; }

        SortedList<int, Category> GetCategories();

        SortedList<string, Parameter> GetCategoryParameters(int categoryId);
    }
}
