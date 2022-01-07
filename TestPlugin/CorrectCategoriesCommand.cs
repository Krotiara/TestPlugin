using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlugin.Models;
using TestPlugin.Views;

namespace TestPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class CorrectCategoriesCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document; //get active Autodesk revit project
            IDocumentDataService documentDataService = new DocumentDataService(document); //Обертка-обработчик над документом
            try
            {
                //Загрузка окна плагина
                CategoriesView categoriesView = new CategoriesView(documentDataService);
                categoriesView.ShowDialog();
            }
            catch(Exception e)
            {
                return Result.Failed;
            }
            return Result.Succeeded;
        }
    }
}
