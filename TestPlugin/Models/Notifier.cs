using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Models
{
    /// <summary>
    /// Класс-уведомитель об изменениях. При наследовании от него добавляет поддержку интерфейса INotifyPropertyChanged без необходимости явной реализации
    /// </summary>
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
