using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Models
{
    class CategoriesModel: Notifier
    {

        public CategoriesModel(IDocumentDataService documentDataService)
        {
            this.documentDataService = documentDataService;
        }

        private readonly IDocumentDataService documentDataService;
        private SortedList<string, Category> categories;
        private string currentCategoryName;

        public string CurrentCategoryName
        {
            get => currentCategoryName;
            set
            {
                currentCategoryName = value;
                NotifyPropertyChanged(CurrentCategoryName);
            }
        }


        public int CurrentCategoryId { get; private set; }

        public int CurrentParameterId { get; private set; }

        public SortedList<string, Category> GetCategories()
        {
            categories = documentDataService.GetCategories();
            return categories;
        }

    }
}
