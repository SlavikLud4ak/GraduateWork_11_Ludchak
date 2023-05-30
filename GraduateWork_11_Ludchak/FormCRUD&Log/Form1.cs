using GraduateWork_11_Ludchak.Data;
using GraduateWork_11_Ludchak.Models;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (var vacationDBContext = new VacationDBContext())
            {
                if (vacationDBContext.Employees != null)
                {
                    var employees = await vacationDBContext.Employees.OrderBy(e => e.Departament).ToListAsync();
                    var generator = new EmployeeDocumentGenerator();
                    generator.Generate(employees);
                }
            }
        }
    }
}
