using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeUtils
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            switch(cbWhat.SelectedIndex)
            {
                case 0:
                    setResult(Constructor.processConstructor(input.Lines));
                    break;
                case 1:
                    setResult(XmlRefactoring.processXmlNodes(input.Lines));
                    break;
                case 2:
                    setResult(Assignments.processIfThenElse(input.Lines));
                    break;
            }
        }

        private void setResult(String res)
        {
            output.Text = res;
            output.Focus();
            output.SelectAll();
        }
    }
}
