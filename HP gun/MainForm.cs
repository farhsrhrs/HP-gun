using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace HP_gun
{
    public partial class MainForm : Form
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=MasterPOL";

        public MainForm()
        {
            InitializeComponent();
            SetupForm();
            LoadPartners();
        }

        private void SetupForm()
        {
            Text = "Учет партнеров — HP Gun";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            // Установите иконку приложения в свойствах проекта!
            // Добавьте PictureBox с логотипом в Designer
        }

        private void LoadPartners()
        {
            flowLayoutPanel1.Controls.Clear();

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                // 🔧 Запрос с учётом реальной структуры БД
                const string query = @"
                    SELECT 
                        p.partner_id,
                        p.partner_type,
                        p.name AS partner_name,
                        p.director,
                        p.phone,
                        p.rating,

                        COALESCE(SUM(pp.quantity), 0) AS total_sales
                    FROM partners p 
                    LEFT JOIN partner_products pp ON p.partner_id = pp.partner_id_fk
                    GROUP BY p.partner_id, p.partner_type, p.name, p.director, p.phone, p.rating
                    ORDER BY p.partner_id;";

                using var cmd = new NpgsqlCommand(query, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var panel = CreatePartnerCard(reader);
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки партнёров: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreatePartnerCard(NpgsqlDataReader reader)
        {
            // Безопасное чтение данных
            int partnerId = Convert.ToInt32(reader["partner_id"]);
            string partnerType = reader["partner_type"]?.ToString() ?? "—";
            string partnerName = reader["partner_name"]?.ToString() ?? "Без имени";
            string director = reader["director"]?.ToString() ?? "—";
            string phone = reader["phone"]?.ToString() ?? "";
            int rating = reader["rating"] as int? ?? 0;
            int totalSales = Convert.ToInt32(reader["total_sales"]) as int? ?? 0;
            System.Diagnostics.Debug.WriteLine($"DEBUG: Partner = {partnerName}, ID = {partnerId}, total_sales = {totalSales}");

            int discount = CalculateDiscount(totalSales);

            var cardPanel = new Panel
            {
                Width = flowLayoutPanel1.Width - 30,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Tag = partnerId // для будущего редактирования
            };

            var headerLabel = new Label
            {
                Text = $"{partnerType} | {partnerName}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            var discountLabel = new Label
            {
                Text = $"{discount}%",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(cardPanel.Width - 60, 10),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var directorLabel = new Label
            {
                Text = $"Директор: {director}",
                Location = new Point(10, 40),
                AutoSize = true
            };

            var phoneLabel = new Label
            {
                Text = string.IsNullOrWhiteSpace(phone)
                    ? "Телефон не указан"
                    : phone.StartsWith("+7") ? phone : $"+7 {phone}",
                Location = new Point(10, 65),
                AutoSize = true
            };

            var ratingLabel = new Label
            {
                Text = $"Рейтинг: {rating}",
                Location = new Point(10, 90),
                AutoSize = true
            };
            var btnHistory = new Button
            {
                Text = "История продаж",
                Location = new Point(10, 110),
                Size = new Size(150, 30),
                FlatStyle = FlatStyle.Flat,
                Tag = partnerId
            };

            btnHistory.Click += (sender, e) =>
            {
                var historyForm = new PartnerSalesHistoryForm(
                    partnerId,
                    partnerName,
                    _connectionString);
                historyForm.Show();
            };
            cardPanel.Controls.AddRange(new Control[]
            {
                headerLabel, discountLabel, directorLabel, phoneLabel, ratingLabel,btnHistory
            });

            // Клик по карточке — редактирование
            cardPanel.Click += (s, e) =>
            {
                var editForm = new PartnerEditForm(partnerId, _connectionString);
                editForm.FormClosed += (sender, args) => LoadPartners();
                editForm.ShowDialog();
            };



            return cardPanel;
        }

        private int CalculateDiscount(int totalSales)
        {
            if (totalSales >= 300000) return 15;
            if (totalSales >= 50000) return 10;
            if (totalSales >= 10000) return 5;
            return 0;
        }

        private void btnAddPartner_Click_1(object sender, EventArgs e)
        {
            var editForm = new PartnerEditForm(_connectionString);
            editForm.FormClosed += (s, args) => LoadPartners();
            editForm.ShowDialog();
        }
    }
}