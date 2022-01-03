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
    /// Логика взаимодействия для ChangeParameterView.xaml
    /// </summary>
    public partial class ChangeParameterView : Window
    {
        public ChangeParameterView(string parameterName, string parameterCategory, CategoriesModel categoriesModel)
        {
            InitializeComponent();
            DataContext = new ParameterChangingViewModel(parameterName, parameterCategory, categoriesModel);
        }
    }
}
