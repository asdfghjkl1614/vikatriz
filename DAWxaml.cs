using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Shapes;

namespace TRIZBD_4LR
{
    /// <summary>
    /// Логика взаимодействия для DepartmentAddWindow.xaml
    /// </summary>
    public partial class DepartmentAddWindow : Window
    {
        private readonly Department _currentDepartment = new Department();
        private readonly Hospital3Entities _db = new Hospital3Entities();

        public DepartmentAddWindow()
        {
            InitializeComponent();
            DataContext = _currentDepartment;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка названия отделения
            if (string.IsNullOrWhiteSpace(_currentDepartment.Title))
            {
                MessageBox.Show("Название отделения - обязательное поле!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка часов работы
            if (string.IsNullOrWhiteSpace(_currentDepartment.Opening_hours))
            {
                MessageBox.Show("Часы работы - обязательное поле!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Дополнительная проверка формата часов работы (например, "9:00-18:00")
            if (!System.Text.RegularExpressions.Regex.IsMatch(_currentDepartment.Opening_hours,
                @"^\d{1,2}:\d{2}-\d{1,2}:\d{2}$"))
            {
                MessageBox.Show("Введите часы работы в формате ЧЧ:ММ-ЧЧ:ММ (например, 9:00-18:00)",
                               "Ошибка формата",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _db.Department.Add(_currentDepartment);
                _db.SaveChanges();
                MessageBox.Show("Отделение успешно добавлено!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                new DepartmentsWindow().Show();
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                MessageBox.Show($"Ошибки валидации:\n{string.Join("\n", errorMessages)}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddBack_Click(object sender, RoutedEventArgs e)
        {
            new DepartmentsWindow().Show();
            Close();
        }
    }
}
