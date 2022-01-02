using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestPlugin.Models;
using TestPlugin.ViewModels;

namespace TestPlugin.Views
{
    /// <summary>
    /// Логика взаимодействия для CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : Window
    {
        public CategoriesView(IDocumentDataService documentDataService)
        {
            InitializeComponent();
            DataContext = new CategoriesViewModel(documentDataService);
        }
    }
}
