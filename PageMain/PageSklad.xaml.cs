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
    /// Логика взаимодействия для PageSklad.xaml
    /// </summary>
    public partial class PageSklad : Page
    {
        public PageSklad()
        {
            InitializeComponent();
            DtGridTovar.ItemsSource = PR13MOROZOVEntities.GetContext().Sklad.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageSkladAdd((sender as Button).DataContext as Sklad));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var tovarForRemoning = DtGridTovar.SelectedItems.Cast<Sklad>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующее {tovarForRemoning.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PR13MOROZOVEntities.GetContext().Sklad.RemoveRange(tovarForRemoning);
                    PR13MOROZOVEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DtGridTovar.ItemsSource = PR13MOROZOVEntities.GetContext().Sklad.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageSkladAdd(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                PR13MOROZOVEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DtGridTovar.ItemsSource = PR13MOROZOVEntities.GetContext().Sklad.ToList();
            }
           
        }
    }
}
