using System;
using System.Windows.Forms;

namespace Calculator2024
{
    public partial class FormCalc : Form
    {
        private char[,] buttons =
        {
            { '%', '\u0152', 'C', '\u232B'},
            { '\u215F', '\u00B2', '\u221A', '\u00F7'},
            { '7', '8', '9', '\u00D7'},
            { '4', '5', '6', '-'},
            { '1', '2', '3', '+'},
            { '\u00B1', '0', ',', '='},
        };
        public FormCalc()
        {
            InitializeComponent();
        }

        private void FormCalc_Load(object sender, System.EventArgs e)
        {
            MakeButtons(buttons.GetLength(0), buttons.GetLength(1));
        }

        private void MakeButtons(int rows, int cols)
        {
            int btnWidth = 80;
            int btnHeight = 60;
            int posY = 0;
            for (int i = 0; i < rows; i++)
            {
                int posX = 0;
                for (int j = 0; j < cols; j++)
                {
                    Button myButton = new Button();
                    myButton.Width = btnWidth;
                    myButton.Height = btnHeight;
                    myButton.Top = posY;
                    myButton.Left = posX;
                    myButton.Font = new System.Drawing.Font("Segoe UI", 16);
                    myButton.Text = buttons[i,j].ToString();
                    panelBottom.Controls.Add(myButton);
                    posX += btnWidth;
                }
                posY += btnHeight;
            }
        }
    }
}
