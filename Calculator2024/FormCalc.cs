using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator2024
{
    public partial class FormCalc : Form
    {
        public struct BtnStruct
        {
            public char Content;
            public Color Color;
            public BtnStruct(char content, Color color)
            {
                this.Content = content;
                this.Color = color;
            }
        }

        static private Color operationBackground = Color.LightGray;
        static private Color numbersBackground = Color.WhiteSmoke;
        static private Color equalSignBackground = Color.LightSeaGreen;

        private BtnStruct[,] buttons =
        {
            { new BtnStruct('%', operationBackground), new BtnStruct('\u0152', operationBackground), new BtnStruct('C', operationBackground), new BtnStruct('\u232B', operationBackground)},
            { new BtnStruct('\u215F', operationBackground), new BtnStruct('\u00B2', operationBackground), new BtnStruct('\u221A', operationBackground), new BtnStruct('\u00F7', operationBackground)},
            { new BtnStruct('7', numbersBackground), new BtnStruct('8', numbersBackground), new BtnStruct('9', numbersBackground), new BtnStruct('\u00D7', operationBackground)},
            { new BtnStruct('4', numbersBackground), new BtnStruct('5', numbersBackground), new BtnStruct('6', numbersBackground), new BtnStruct('-', operationBackground)},
            { new BtnStruct('1', numbersBackground), new BtnStruct('2', numbersBackground), new BtnStruct('3', numbersBackground), new BtnStruct('+', operationBackground)},
            { new BtnStruct('\u00B1', numbersBackground), new BtnStruct('0', numbersBackground), new BtnStruct(',', numbersBackground), new BtnStruct('=', equalSignBackground)},
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
                    myButton.Font = new Font("Segoe UI", 16);
                    myButton.BackColor = buttons[i, j].Color;
                    myButton.Text = buttons[i,j].Content.ToString();
                    panelBottom.Controls.Add(myButton);
                    posX += btnWidth;
                }
                posY += btnHeight;
            }
        }
    }
}
