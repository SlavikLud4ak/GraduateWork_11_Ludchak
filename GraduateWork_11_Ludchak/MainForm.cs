using GraduateWork_11_Ludchak.Data;
using GraduateWork_11_Ludchak.FormCRUD_Log;
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

namespace GraduateWork_11_Ludchak
{
    public partial class MainForm : MaterialForm
    {

        public MainForm()
        {
            
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber400, Primary.BlueGrey900, Primary.Amber400, Accent.Amber400, TextShade.WHITE);
            upDateTable();
            upDateTableVP();

        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            FormEmployeeCRUD formEmployeeCRUD = new FormEmployeeCRUD("c");
            formEmployeeCRUD.FormClosed += CalledForm_FormClosed;
            formEmployeeCRUD.Show();            
            if (formEmployeeCRUD.IsDisposed) upDateTable();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {            
            FormEmployeeCRUD formEmployeeCRUD = new FormEmployeeCRUD("u", this.bunifuDataGridView1.CurrentRow.Cells[0].Value.ToString());
            formEmployeeCRUD.FormClosed += CalledForm_FormClosed;
            formEmployeeCRUD.Show();
            if (formEmployeeCRUD.IsDisposed) upDateTable(); 
        }

        private void materialFloatingActionButton3_Click(object sender, EventArgs e)
        {
            FormEmployeeCRUD formEmployeeCRUD = new FormEmployeeCRUD("d", this.bunifuDataGridView1.CurrentRow.Cells[0].Value.ToString());
            formEmployeeCRUD.FormClosed += CalledForm_FormClosed;
            formEmployeeCRUD.Show();
            if (formEmployeeCRUD.IsDisposed) upDateTable();
        }

        private async void upDateTable()
        {
            int i = 0;
            this.bunifuDataGridView1.Rows.Clear();
            using (var vacationDBContext = new VacationDBContext()) { 
                if (vacationDBContext.Employees != null)
                {
                    var employees = await vacationDBContext.Employees.ToListAsync();  
                    foreach (var employee in employees)
                    {
                        this.bunifuDataGridView1.Rows.Add();
                        this.bunifuDataGridView1.Rows[i].Cells[0].Value = employee.Id;
                        this.bunifuDataGridView1.Rows[i].Cells[1].Value = employee.FullName;
                        this.bunifuDataGridView1.Rows[i].Cells[2].Value = employee.Position;
                        this.bunifuDataGridView1.Rows[i].Cells[3].Value = employee.Departament;
                        this.bunifuDataGridView1.Rows[i].Cells[4].Value = employee.StartDate;                        
                        ++i;
                    }
                }
            }
        }

        private async void upDateTableVP()
        {
            int i = 0;
            this.bunifuDataGridView2.Rows.Clear();
            using (var vacationDBContext = new VacationDBContext())
            {
                if (vacationDBContext.Employees != null)
                {
                    var employees = await vacationDBContext.Employees.ToListAsync();
                    var VP = vacationDBContext.Employees
                 .SelectMany(e => e.VacationPlans)
                 .ToList(); // Switch to client-side evaluation using ToList()
                    foreach (var vp in VP)
                    {
                        this.bunifuDataGridView2.Rows.Add();
                        this.bunifuDataGridView2.Rows[i].Cells[0].Value = vp.Id;
                        this.bunifuDataGridView2.Rows[i].Cells[1].Value = vp.WorkingPeriodStart;
                        this.bunifuDataGridView2.Rows[i].Cells[2].Value = vp.WorkingPeriodEnd;
                        this.bunifuDataGridView2.Rows[i].Cells[3].Value = vp.AnnualBase;
                        this.bunifuDataGridView2.Rows[i].Cells[4].Value = vp.VacationTime;
                        this.bunifuDataGridView2.Rows[i].Cells[5].Value = vp.AdditionalVacation;
                        this.bunifuDataGridView2.Rows[i].Cells[6].Value = vp.AddVacationTime;
                        this.bunifuDataGridView2.Rows[i].Cells[7].Value = employees.FirstOrDefault(employee => employee.VacationPlans != null && employee.VacationPlans.Any(vacationPlan => vacationPlan.Id == vp.Id))?.FullName;
                        ++i;
                    }
                }
            }
        }

        private void CalledForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            upDateTable();
        }

        private void CalledForm_FormClosedVP(object sender, FormClosedEventArgs e)
        {
            upDateTableVP();
        }

        private void materialFloatingActionButton6_Click(object sender, EventArgs e)
        {
            FormVPCRUD formVPCRUD = new FormVPCRUD("c");
            formVPCRUD.FormClosed += CalledForm_FormClosedVP;
            formVPCRUD.Show();
            if (formVPCRUD.IsDisposed) upDateTableVP();
        }

        private void materialFloatingActionButton5_Click(object sender, EventArgs e)
        {
            FormVPCRUD formVPCRUD = new FormVPCRUD("u", this.bunifuDataGridView2.CurrentRow.Cells[0].Value.ToString(), this.bunifuDataGridView2.CurrentRow.Cells[7].Value.ToString());
            formVPCRUD.FormClosed += CalledForm_FormClosedVP;
            formVPCRUD.Show();
            if (formVPCRUD.IsDisposed) upDateTableVP();
        }

        private void materialFloatingActionButton4_Click(object sender, EventArgs e)
        {
            FormVPCRUD formVPCRUD = new FormVPCRUD("d", this.bunifuDataGridView2.CurrentRow.Cells[0].Value.ToString(), this.bunifuDataGridView2.CurrentRow.Cells[7].Value.ToString());
            formVPCRUD.FormClosed += CalledForm_FormClosedVP;
            formVPCRUD.Show();
            if (formVPCRUD.IsDisposed) upDateTableVP();
        }
    }
}
