using GraduateWork_11_Ludchak.Data;
using GraduateWork_11_Ludchak.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Azure.Cosmos.Spatial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraduateWork_11_Ludchak.FormCRUD_Log
{
    public partial class FormEmployeeCRUD : MaterialForm
    {
        public string CrudOperation { get; set; }
        public string IdPosition { get; set; }
        public FormEmployeeCRUD(string crudOperation, string idPosition = "")
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber400, Primary.BlueGrey900, Primary.Amber400, Accent.Amber400, TextShade.WHITE);
            CrudOperation = crudOperation;
            IdPosition = idPosition;
        }

        private async void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            using (var vacationDBContext = new VacationDBContext())
            {
                switch (CrudOperation)
                {
                    case "c":
                        var emp = new Employee()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FullName = this.materialTextBox21.Text,
                            Position = this.materialTextBox22.Text,
                            Departament = this.materialTextBox23.Text,
                            StartDate = this.bunifuDatePicker1.Value.Year
                        };
                        vacationDBContext.Employees?.Add(emp);
                        await vacationDBContext.SaveChangesAsync();
                        this.Close();
                        break;
                    case "u":
                        if (vacationDBContext.Employees != null)
                        {
                            var employee = await vacationDBContext.Employees.Where(e => e.Id == IdPosition).FirstOrDefaultAsync();
                            if (employee != null)
                            {
                                employee.FullName = this.materialTextBox21.Text;
                                employee.Position = this.materialTextBox22.Text;
                                employee.Departament = this.materialTextBox23.Text;
                                employee.StartDate = this.bunifuDatePicker1.Value.Year;
                                await vacationDBContext.SaveChangesAsync();                                
                            }
                        }
                        this.Close();
                        break;
                    case "d":
                        if (vacationDBContext.Employees != null)
                        {
                            var employee = await vacationDBContext.Employees.Where(e => e.Id == IdPosition).FirstOrDefaultAsync();
                            if (employee != null)
                            {
                                vacationDBContext.Employees.Remove(employee);
                                await vacationDBContext.SaveChangesAsync();                                
                            }
                        }
                        this.Close();
                        break;                    
                }

            }

        }        

        private async void FormEmployeeCRUD_LoadAsync(object sender, EventArgs e)
        {
            bunifuDatePicker1.CustomFormat = "yyyy";
            if (!string.IsNullOrEmpty(IdPosition))
            {
                using (var vacationDBContext = new VacationDBContext())
                {
                    if (vacationDBContext.Employees != null)
                    {
                        var employee = await vacationDBContext.Employees.Where(e => e.Id == IdPosition).FirstAsync();
                        this.materialTextBox21.Text = employee.FullName;
                        this.materialTextBox22.Text = employee.Position;
                        this.materialTextBox23.Text = employee.Departament;
                        this.bunifuDatePicker1.Value = new DateTime(Convert.ToInt16(employee.StartDate), 1, 1);

                    }
                }
            }

            if (CrudOperation == "d") {
                this.materialTextBox21.ReadOnly = true;
                this.materialTextBox22.ReadOnly = true;
                this.materialTextBox23.ReadOnly = true;
                this.bunifuDatePicker1.Enabled = false;
            } 
        }
    }
}
