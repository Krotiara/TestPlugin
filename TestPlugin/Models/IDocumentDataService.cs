﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace TestPlugin.Models
{
    public interface IDocumentDataService
    {
        SortedList<string, Category> GetCategories();

        SortedList<string, Parameter> GetCategoryParameters(string categoryName);
    }
}
