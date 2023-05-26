using GraduateWork_11_Ludchak.Data;
using GraduateWork_11_Ludchak.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraduateWork_11_Ludchak.FormCRUD_Log
{
    public partial class FormVPCRUD : MaterialForm
    {
        public string CrudOperation { get; set; }
        public string IdPosition { get; set; }
        public string FullNameEmp { get; set; }
        public FormVPCRUD(string crudOperation, string idPosition = "", string fullNameEmp = "")
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber400, Primary.BlueGrey900, Primary.Amber400, Accent.Amber400, TextShade.WHITE);
            CrudOperation = crudOperation;
            IdPosition = idPosition;
            FullNameEmp = fullNameEmp;
        }

        private async void FormVPCRUD_Load(object sender, EventArgs e)
        {
            using (var vacationDBContext = new VacationDBContext())
            {
                var employees = await vacationDBContext.Employees.ToListAsync();
                var fullNames = employees.Select(employee => employee.FullName).ToList();
                materialComboBox3.DataSource = fullNames;

                if (!string.IsNullOrEmpty(IdPosition))
                {

                    if (vacationDBContext.Employees != null)
                    {
                        var vp = await vacationDBContext.Employees.SelectMany(employee => employee.VacationPlans).Where(vacationPlan => vacationPlan.Id == IdPosition).FirstAsync();
                        this.bunifuDatePicker1.Value = (DateTime)vp.WorkingPeriodStart;
                        this.bunifuDatePicker2.Value = (DateTime)vp.WorkingPeriodEnd;
                        this.materialSlider1.Value = Convert.ToInt16(vp.AnnualBase);
                        this.materialSlider2.Value = Convert.ToInt16(vp.AdditionalVacation);
                        this.materialComboBox1.Text = vp.VacationTime.ToString();
                        this.materialComboBox2.Text = vp.AddVacationTime.ToString();
                        this.materialComboBox3.Text = FullNameEmp;
                    }

                }
            }

            if (CrudOperation == "d")
            {
                this.bunifuDatePicker1.Enabled = false;
                this.bunifuDatePicker2.Enabled = false;
                this.materialSlider1.Enabled = false;
                this.materialSlider2.Enabled = false;
                this.materialComboBox1.Enabled = false;
                this.materialComboBox2.Enabled = false;
                this.materialComboBox3.Enabled = false;                
            }
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void MaterialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            using (var vacationDBContext = new VacationDBContext())
            {
                switch (CrudOperation)
                {
                    case "c":
                        if (vacationDBContext.Employees != null)
                        {
                            var employee = await vacationDBContext.Employees.Where(e => e.FullName == this.materialComboBox3.Text).FirstOrDefaultAsync();
                            if (employee != null)
                            {
                                var vacationPlan = new VacationPlan()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    WorkingPeriodStart = this.bunifuDatePicker1.Value,
                                    WorkingPeriodEnd = this.bunifuDatePicker2.Value,
                                    AnnualBase = this.materialSlider1.Value,
                                    VacationTime = this.materialComboBox1.Text,
                                    AdditionalVacation = this.materialSlider2.Value,
                                    AddVacationTime = this.materialComboBox2.Text
                                };
                                employee.VacationPlans.Add(vacationPlan);
                                await vacationDBContext.SaveChangesAsync();
                            }
                        }                        
                        this.Close();
                        break;
                    case "u":
                        if (vacationDBContext.Employees != null)
                        {
                            var vp = await vacationDBContext.Employees.Where(e => e.FullName == this.materialComboBox3.Text).SelectMany(employee => employee.VacationPlans).Where(vacationPlan => vacationPlan.Id == IdPosition).FirstOrDefaultAsync();
                            if (vp != null)
                            {
                                vp.WorkingPeriodStart = this.bunifuDatePicker1.Value;
                                vp.WorkingPeriodEnd = this.bunifuDatePicker2.Value;
                                vp.AnnualBase = this.materialSlider1.Value;
                                vp.VacationTime = this.materialComboBox1.Text;
                                vp.AdditionalVacation = this.materialSlider2.Value;
                                vp.AddVacationTime = this.materialComboBox2.Text;
                                await vacationDBContext.SaveChangesAsync();
                            }
                        }
                        this.Close();
                        break;
                    case "d":
                        if (vacationDBContext.Employees != null)
                        {
                            var employee = await vacationDBContext.Employees.FirstOrDefaultAsync(e => e.FullName == FullNameEmp);

                            if (employee != null)
                            {
                                var vacationPlan = employee.VacationPlans.FirstOrDefault(vp => vp.Id == IdPosition);

                                if (vacationPlan != null)
                                {
                                    employee.VacationPlans.Remove(vacationPlan);
                                    await vacationDBContext.SaveChangesAsync();
                                }
                            }
                        }
                        this.Close();
                        break;
                }

            }
        }
    }
}
