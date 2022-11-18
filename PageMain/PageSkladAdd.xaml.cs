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
using WPF_PR_13.AppAplicationData;

namespace WPF_PR_13.image.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageSkladAdd.xaml
    /// </summary>
    public partial class PageSkladAdd : Page
    {
        private Sklad _currentSklad = new Sklad();

        public PageSkladAdd(Sklad selectedSklad)
        {
            InitializeComponent();
            if (selectedSklad != null) 
                _currentSklad = selectedSklad;

                
            DataContext = _currentSklad;
            ComboStrana.ItemsSource = PR13MOROZOVEntities.GetContext().Strana.ToList();
        }


        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentSklad.naimenov))
                errors.AppendLine("Укажите название товара");
            if (_currentSklad.kolichestvo <= 0)
                errors.AppendLine("Количество товара не может быть меньше или равно 0");
            if (_currentSklad.chena <= 0)
                errors.AppendLine("Цена не может быть меньше или равно 0");
            if (_currentSklad.Strana == null )
                errors.AppendLine("Выберите страну");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentSklad.id == 0)
                PR13MOROZOVEntities.GetContext().Sklad.Add(_currentSklad);
            try
            {
                PR13MOROZOVEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                AppFrame.FrameMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

        }
    }
}
