using System;
using System.Windows.Forms;
using Npgsql;

namespace HP_gun
{
    public partial class PartnerEditForm : Form
    {
        // 🔹 ПОЛЯ: используем префикс "_" для ясности
        private readonly string _connectionString;
        private readonly int? _partnerId;

        // Конструктор для ДОБАВЛЕНИЯ нового партнёра
        public PartnerEditForm(string connectionString)
        {
            _connectionString = connectionString; // ← присваиваем ПОЛЕ
            InitializeComponent(); // ← инициализируем компоненты ДО загрузки данных
        }

        // Конструктор для РЕДАКТИРОВАНИЯ существующего партнёра
        public PartnerEditForm(int partnerId, string connectionString) : this(connectionString)
        {
            _partnerId = partnerId; // ← присваиваем ПОЛЕ
            LoadPartnerData();      // ← теперь можно грузить данные
        }

        private void LoadPartnerData()
        {
            if (!_partnerId.HasValue) return;

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                const string query = "SELECT * FROM partners WHERE partner_id = @id";
                using var cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", _partnerId.Value);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // 🔧 Используем ПРАВИЛЬНЫЕ имена колонок из вашей БД
                    txtName.Text = reader["name"]?.ToString() ?? string.Empty;
                    cmbType.Text = reader["partner_type"]?.ToString() ?? string.Empty;
                    numRating.Value = reader["rating"] as int? ?? 0;
                    txtAddress.Text = reader["address"]?.ToString() ?? string.Empty;
                    txtDirector.Text = reader["director"]?.ToString() ?? string.Empty;
                    txtEmail.Text = reader["email"]?.ToString() ?? string.Empty;

                    // 🔹 maskedTxtINN и maskedTextBoxPhone — это MaskedTextBox (см. Designer.cs)
                    maskedTxtINN.Text = reader["inn"]?.ToString() ?? string.Empty;
                    maskedTextBoxPhone.Text = reader["phone"]?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при загрузке данных партнёра: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Поле 'Наименование партнера' обязательно для заполнения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbType.Text))
            {
                MessageBox.Show("Выберите тип партнера из списка.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }

            if (!maskedTxtINN.MaskCompleted)
            {
                MessageBox.Show("ИНН должен содержать ровно 10 цифр.\nФормат: 1234567890",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maskedTxtINN.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле 'Юридический адрес' обязательно для заполнения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDirector.Text))
            {
                MessageBox.Show("Поле 'Директор' обязательно для заполнения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDirector.Focus();
                return false;
            }

            if (!maskedTextBoxPhone.MaskCompleted)
            {
                MessageBox.Show("Введите корректный номер телефона.\nФормат: (XXX) XXX-XX-XX",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maskedTextBoxPhone.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email) &&
                !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email адрес.\nФормат: example@domain.com",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void AddParameters(NpgsqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@type", cmbType.Text.Trim());
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@director", txtDirector.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", maskedTextBoxPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@inn", maskedTxtINN.Text.Trim());
            cmd.Parameters.AddWithValue("@rating", (int)numRating.Value);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                if (_partnerId.HasValue)
                {
                    // Обновление
                    const string updateQuery = @"
                        UPDATE partners SET 
                            partner_type = @type, 
                            name = @name, 
                            director = @director, 
                            email = @email, 
                            phone = @phone, 
                            address = @address, 
                            inn = @inn,
                            rating = @rating 
                        WHERE partner_id = @id";

                    using var cmd = new NpgsqlCommand(updateQuery, connection);
                    AddParameters(cmd);
                    cmd.Parameters.AddWithValue("@id", _partnerId.Value);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    // Добавление
                    const string insertQuery = @"
                        INSERT INTO partners 
                        (partner_type, name, director, email, phone, address, inn, rating) 
                        VALUES (@type, @name, @director, @email, @phone, @address, @inn, @rating)";

                    using var cmd = new NpgsqlCommand(insertQuery, connection);
                    AddParameters(cmd);
                    cmd.ExecuteNonQuery();
                }
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Npgsql.PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                if (ex.Message.Contains("inn"))
                    MessageBox.Show("Партнёр с таким ИНН уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Партнёр с таким наименованием уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}