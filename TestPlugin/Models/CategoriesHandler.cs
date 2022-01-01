using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Models
{
    class CategoriesHandler
    {

        public CategoriesHandler(List<Category> categories)
        {
            Categories = categories;
        }

        public List<Category> Categories { get; private set; }
        public List<Parameter> CurrentCategoryParameters { get; private set; }

        public int CurrentCategoryId { get; private set; }

        public int CurrentParameterId { get; private set; }

    }
}
