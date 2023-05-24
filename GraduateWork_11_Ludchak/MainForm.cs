using GraduateWork_11_Ludchak.Data;
using GraduateWork_11_Ludchak.FormCRUD_Log;
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
            if (formEmployeeCRUD.IsDisposed) {              
                upDateTable(); }
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

        private void CalledForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            upDateTable();
        }

    }
}
