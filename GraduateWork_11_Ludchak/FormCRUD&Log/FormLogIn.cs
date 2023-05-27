using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraduateWork_11_Ludchak.Data;
using GraduateWork_11_Ludchak.Models;
using Microsoft.Azure.Cosmos.Spatial;

namespace GraduateWork_11_Ludchak.FormCRUD_Log
{
    public partial class FormLogIn : MaterialForm
    {
        public string Pass { get; set; }
        public FormLogIn()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber400, Primary.BlueGrey900, Primary.Amber400, Accent.Amber400, TextShade.WHITE);            
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.materialTextBox24.Enabled = true;
                string fromAddress = "vpapp.email@gmail.com";
                string toAddress = this.materialTextBox23.Text;
                string subject = "Ваш ключ для підтвердження";
                string body = $"Ваш ключ для підтвердження -> {Pass}";

                //string password = "admin1vp1";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromAddress, "ksfptwxvlbzqjemq");

                MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                smtpClient.Send(mailMessage);
                

                MaterialMessageBox.Show("Код відправлено на ваш електронний адрес");
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show($"Ваша електронна адреса не вірна. Ви отримали таку помилку: {ex.Message}");
            }
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {
            Pass = Guid.NewGuid().ToString();            
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void MaterialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            using (var vacationDBContext = new VacationDBContext())
            {

                var logUser = new LogUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = this.materialTextBox21.Text,
                    Password = this.materialTextBox22.Text,
                    Email = this.materialTextBox23.Text                    
                };
                if (Pass == this.materialTextBox24.Text && !string.IsNullOrEmpty(materialTextBox21.Text.ToString()) && !string.IsNullOrEmpty(materialTextBox22.Text.ToString())) //
                {
                    MaterialMessageBox.Show("Створено акаунт");
                    vacationDBContext.LogUsers?.Add(logUser);
                    await vacationDBContext.SaveChangesAsync();
                    this.Close();                    
                }
                else {
                    MaterialMessageBox.Show($"Дані не вірні або відсутні {Pass}");
                }

            }
        }
    }
}
