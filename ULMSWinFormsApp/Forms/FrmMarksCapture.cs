using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ULMSWinFormsApp.Models;

namespace ULMSWinFormsApp.Forms
{
    public partial class FrmMarksCapture : Form
    {
        public FrmMarksCapture()
        {
            InitializeComponent();
        }

        private void btnCalculateResults_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(txtMarkStudentId.Text) ||
                   string.IsNullOrWhiteSpace(txtMarkStudentName.Text) ||
                   string.IsNullOrWhiteSpace(txtSubject1.Text) ||
                   string.IsNullOrWhiteSpace(txtSubject2.Text) ||
                   string.IsNullOrWhiteSpace(txtSubject3.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                // Fixed the issue by creating a new MarkRecord instance and populating it with the input values
                MarkRecord record = new MarkRecord();

                record.StudentId = txtMarkStudentId.Text;
                record.StudentName = txtMarkStudentName.Text;

                if (!double.TryParse(txtSubject1.Text, out double S1) ||
                    !double.TryParse(txtSubject2.Text, out double S2) ||
                    !double.TryParse(txtSubject3.Text, out double S3))
                {
                    MessageBox.Show("Please enter a number between 0 and 100.");
                    return;
                }

                if (S1 < 0 || S1 > 100 || S2 < 0 || S2 > 100 || S3 < 0 || S3 > 100)
                {
                    MessageBox.Show("Please enter a number between 0 and 100.");
                    return;
                }

                record.Subject1 = S1;
                record.Subject2 = S2;
                record.Subject3 = S3;

                
                record.Average = (record.Subject1 + record.Subject2 + record.Subject3) / 3;

                //  Newly added code to determine the result status based on the average mark

                if (record.Average >= 75)
                {
                    record.ResultStatus = "DISTINCTION";
                }
                else if (record.Average < 50)
                {
                    record.ResultStatus = "PASS";
                }
                else
                {
                    record.ResultStatus = "FAIL";
                }

                txtMarksOutput.Text =
                    "Marks processed successfully!" + Environment.NewLine +
                    "Student ID: " + record.StudentId + Environment.NewLine +
                    "Student Name: " + record.StudentName + Environment.NewLine +
                    "Subject 1: " + record.Subject1 + Environment.NewLine +
                    "Subject 2: " + record.Subject2 + Environment.NewLine +
                    "Subject 3: " + record.Subject3 + Environment.NewLine +
                    "Average: " + record.Average.ToString("0.00") + Environment.NewLine +
                    "Final Result: " + record.ResultStatus;
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error has occured"+ ex.Message);
            }
        }

        private void btnClearMarks_Click(object sender, EventArgs e)
        {
            txtMarkStudentId.Clear();
            txtMarkStudentName.Clear();
            txtSubject1.Clear();
            txtSubject2.Clear();
            txtSubject3.Clear();
            txtMarksOutput.Clear();
            txtMarkStudentId.Focus();
        }

        private void btnBackMarks_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
