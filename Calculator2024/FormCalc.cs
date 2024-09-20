using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator2024
{
    public partial class FormCalc : Form
    {
        static private Color operationBackground = Color.LightGray;
        static private Color numbersBackground = Color.WhiteSmoke;
        static private Color equalSignBackground = Color.LightSeaGreen;

        public struct BtnStruct
        {
            public char Content;
            public Color Color;
            public bool IsNumber;

            public BtnStruct(char content, Color color, bool isNumber = false)
            {
                this.Content = content;
                this.Color = color;
                this.IsNumber = isNumber;
            }
        }

        private BtnStruct[,] buttons =
        {
            { new BtnStruct('%', operationBackground), new BtnStruct('\u0152', operationBackground), new BtnStruct('C', operationBackground), new BtnStruct('\u232B', operationBackground)},
            { new BtnStruct('\u215F', operationBackground), new BtnStruct('\u00B2', operationBackground), new BtnStruct('\u221A', operationBackground), new BtnStruct('\u00F7', operationBackground)},
            { new BtnStruct('7', numbersBackground, true), new BtnStruct('8', numbersBackground, true), new BtnStruct('9', numbersBackground, true), new BtnStruct('\u00D7', operationBackground)},
            { new BtnStruct('4', numbersBackground, true), new BtnStruct('5', numbersBackground, true), new BtnStruct('6', numbersBackground, true), new BtnStruct('-', operationBackground)},
            { new BtnStruct('1', numbersBackground, true), new BtnStruct('2', numbersBackground, true), new BtnStruct('3', numbersBackground, true), new BtnStruct('+', operationBackground)},
            { new BtnStruct('\u00B1', numbersBackground), new BtnStruct('0', numbersBackground, true), new BtnStruct(',', numbersBackground), new BtnStruct('=', equalSignBackground)},
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
                    myButton.Tag = buttons[i, j];
                    myButton.Click += Button_Click;
                    panelBottom.Controls.Add(myButton);
                    posX += btnWidth;
                }
                posY += btnHeight;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            BtnStruct clickedButtonStruct = (BtnStruct)clickedButton.Tag;
            if (clickedButtonStruct.IsNumber)
            {
                lblResult.Text += clickedButton.Text;
            }
        }
    }
}
