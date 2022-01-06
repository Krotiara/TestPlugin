using System.Windows;
using TestPlugin.Models;
using TestPlugin.ViewModels;

namespace TestPlugin.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeParameterView.xaml
    /// </summary>
    public partial class ChangeParameterView : Window
    {
        public ChangeParameterView(string parameterName, string parameterCategoryName, CategoriesModel categoriesModel)
        {
            InitializeComponent();
            DataContext = new ParameterChangingViewModel(parameterName, parameterCategoryName, categoriesModel);
            Title = string.Format("Смена значения параметра \"{0}\"", parameterName);
        }
    }
}
