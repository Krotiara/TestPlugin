using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlugin.Models;

namespace TestPlugin.ViewModels
{
    class CategoriesViewModel
    {
        private CategoriesModel categoriesModel;

        public List<string> CategoriesNames { get; set; }
        public string SelectedCategoryName { get; set; } //To notify
        
        public CategoriesViewModel(IDocumentDataService documentDataService)
        {
            categoriesModel = new CategoriesModel(documentDataService);
            CategoriesNames = categoriesModel.GetCategories().Keys.ToList();
        }





    }
}
