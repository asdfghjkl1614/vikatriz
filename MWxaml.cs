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

namespace TRIZBD_4LR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnPosts_Click(object sender, RoutedEventArgs e)
        {
            var postsWindow = new PostsWindow();
            postsWindow.Show();
            this.Close();
        }

        private void BtnWorkers_Click(object sender, RoutedEventArgs e)
        {
            var workersWindow = new WorkersWindow();
            workersWindow.Show();
            this.Close();
        }

        private void BtnQualifications_Click(object sender, RoutedEventArgs e)
        {
            var qualificationsWindow = new QualificationsWindow();
            qualificationsWindow.Show();
            this.Close();
        }

        private void BtnDepartments_Click(object sender, RoutedEventArgs e)
        {
            var departmentsWindow = new DepartmentsWindow();
            departmentsWindow.Show();
            this.Close();
        }

        private void BtnWards_Click(object sender, RoutedEventArgs e)
        {
            var wardsWindow = new WardsWindow();
            wardsWindow.Show();
            this.Close();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
