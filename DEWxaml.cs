using System;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Windows;
using System.Text.RegularExpressions;
using System.Linq;

namespace TRIZBD_4LR
{
    public partial class DepartmentEditWindow : Window
    {
        private readonly Department _currentDepartment;
        private readonly Hospital3Entities _db = new Hospital3Entities();

        public DepartmentEditWindow(Department selectedDepartment)
        {
            InitializeComponent();

            if (selectedDepartment == null)
            {
                MessageBox.Show("Не выбрано отделение для редактирования", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            _currentDepartment = _db.Department.Find(selectedDepartment.ID_Department);

            if (_currentDepartment == null)
            {
                MessageBox.Show("Отделение не найдено в базе данных", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            DataContext = _currentDepartment;
        }

        private void ButtonSaveEdit_Click(object sender, RoutedEventArgs e)
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

            // Проверка формата часов работы
            if (!Regex.IsMatch(_currentDepartment.Opening_hours, @"^\d{1,2}:\d{2}-\d{1,2}:\d{2}$"))
            {
                MessageBox.Show("Введите часы работы в формате ЧЧ:ММ-ЧЧ:ММ (например, 9:00-18:00)",
                               "Ошибка формата",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _db.Entry(_currentDepartment).State = EntityState.Modified;
                _db.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены!", "Успех",
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonEditBack_Click(object sender, RoutedEventArgs e)
        {
            new DepartmentsWindow().Show();
            Close();
        }

        private void TextBoxOpeningHours_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_currentDepartment.Opening_hours) &&
                !Regex.IsMatch(_currentDepartment.Opening_hours, @"^\d{1,2}:\d{2}-\d{1,2}:\d{2}$"))
            {
                MessageBox.Show("Введите часы работы в формате ЧЧ:ММ-ЧЧ:ММ (например, 9:00-18:00)",
                               "Ошибка формата",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                TextBoxOpeningHours.Focus();
            }
        }
    }
}
