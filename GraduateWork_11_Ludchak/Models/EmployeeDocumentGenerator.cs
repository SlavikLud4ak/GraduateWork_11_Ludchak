using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;




namespace GraduateWork_11_Ludchak.Models
{
    internal class EmployeeDocumentGenerator
    {
        public void Generate(List<Employee> employees) {
            // Create a new Word document
            Application wordApp = new Application();
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();

            Range fontRange = doc.Range();
            fontRange.Font.Name = "Times New Roman";
            fontRange.Font.Size = 12;

            // Set the document format to landscape
            doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            int countrow = 0;
            foreach (Employee employee in employees)
            {
                if (employee.VacationPlans != null)
                {
                    foreach (var vacationPlan in employee.VacationPlans)
                    {
                        countrow++;
                    }                    
                }
            }
            wordApp.WindowState = WdWindowState.wdWindowStateNormal;

            // Create a table
            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(doc.Range(), 1, 9);
            table.Borders.Enable = 1;
            table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitContent);

            // Set column titles
            List<string> columnHeaders = new List<string>()
            {
                "Record Number",
                "FullName",
                "Position",
                "Working Period",
                "Annual Base",
                "Vacation Time",
                "Additional Vacation",
                "Add Vacation Time",
                "Signature"
            };

            foreach (Row row in table.Rows) {
                for (int i = 1; i <= row.Cells.Count; i++)
                {
                    Cell cell = row.Cells[i];
                    if (cell.RowIndex == 1)
                    {
                        cell.Range.Text = columnHeaders[i - 1].ToString();
                        cell.Range.Bold = 1;
                        cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }                    
                }
            }         
            

            for (int index = 0; index < employees.Count; index++)
            {
                Employee employee = employees[index];
                if (employee.VacationPlans != null)
                {
                    foreach (var vacationPlan in employee.VacationPlans)
                    {
                        Row row = table.Rows.Add();
                        row.Cells[1].Range.Text = Convert.ToString(row.Index - 1); // Record Number
                        row.Cells[2].Range.Text = employee.FullName; // FullName
                        row.Cells[3].Range.Text = employee.Position; // Position
                        row.Cells[4].Range.Text = $"{vacationPlan.WorkingPeriodStart:dd.MM.yyyy} - {vacationPlan.WorkingPeriodEnd:dd.MM.yyyy}"; // Working Period
                        row.Cells[5].Range.Text = vacationPlan.AnnualBase.ToString(); // Annual Base
                        row.Cells[6].Range.Text = vacationPlan.VacationTime; // Vacation Time
                        row.Cells[7].Range.Text = vacationPlan.AdditionalVacation.ToString(); // Additional Vacation
                        row.Cells[8].Range.Text = vacationPlan.AddVacationTime; // Add Vacation Time
                        for (int i = 1; i < 10; i++) row.Cells[i].Range.Bold = 0;

                        if (index + 1 < employees.Count)
                        {
                            Employee employeeNext = employees[index + 1];
                            if (employee.Departament != employeeNext.Departament)
                            {
                                Row totalRow = table.Rows.Add();
                                totalRow.Cells[5].Range.Bold = 1;
                                //totalRow.Cells[5].Merge(totalRow.Cells[6]);
                                totalRow.Cells[5].Range.Text = employee.Departament.ToString();                                
                            }
                        }
                    }
                }
            }

            List<string> departments = employees.Select(e => e.Departament)
                                    .Distinct()
                                    .ToList();

            //foreach (Row row in table.Rows)
            //{
            //    for (int j = 0; j < departments.Count; j++)
            //    {
            //        if (row.Cells[1].Range.Text == departments[j].ToString())
            //        {
            //            row.Cells.Merge();
            //            for (int i = 1; i <= row.Cells.Count; i++)
            //            {
            //                Cell cell = row.Cells[i];
            //                cell.Range.Bold = 1;
            //                cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //                cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            //            }
            //        }
            //    }
            //}            


            //foreach (Row row in table.Rows)
            //{
            //    for (int i = 1; i <= row.Cells.Count; i++)
            //    {
            //        Cell cell = row.Cells[i];

            //        {

            //            cell.Range.Bold = 1;
            //            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            //        }
            //    }
            //}

            // Group employees by Department

            doc.Save();

            // Close the document and Word application
            doc.Close();
            wordApp.Quit();
        }

        static string GetSignature()
        {
            // Replace this with your own logic to get the employee's signature
            // You can return a placeholder string for now
            return "             ";
        }
    }
}
