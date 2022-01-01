using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class CorrectCategoriesCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document document = commandData.Application.ActiveUIDocument.Document; //get active Autodesk revit project
            //List<Element> a = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_Walls).ToList(); //Get elements of catefory
            //// element.Category.Id 
            ////Returns the built -in category enum.
            FilteredElementCollector allElementsInView = new FilteredElementCollector(document, document.ActiveView.Id);
            List<Element> viewElements = allElementsInView.ToElements().ToList();
            return Result.Succeeded;
            //throw new NotImplementedException();
        }
    }
}
