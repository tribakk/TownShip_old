using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TownShip_Form
{
    public partial class Form2 : Form
    {
        Kernal.CZakazCalc m_zakazCalc = new Kernal.CZakazCalc();
        public Form2()
        {
            InitializeComponent();
        }

        private void EmptyHaveAlready_Click(object sender, EventArgs e)
        {
            m_zakazCalc.CreateEmptyAlreadyHaveSheet();
        }

        private void updateAlreadyHave_Click(object sender, EventArgs e)
        {
            m_zakazCalc.UpdateAlreadyHaveList();
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            m_zakazCalc.Calc();
        }

        private void LoadZakazButton_Click(object sender, EventArgs e)
        {
            m_zakazCalc.UpdateZakazList();
        }

        private void Form2_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //m_zakazCalc.WriteInfoAboutSourceToExcel();
            m_zakazCalc.WriteInfoAboutFactroryToExcel();
        }
    }
}
