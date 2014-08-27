﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPA.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            var outcome = EPA.Data.Test.AddCompany();
            SetMessage(outcome);

        }


        private void SetMessage(Ravka.Outcome outcome)
        {
            txt_Result.Text = (outcome.HasError ? "Error" : "Success") + ": " + outcome.Message;
        }
    }
}
