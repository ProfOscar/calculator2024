using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Calculator2024
{
    public partial class FormCalc : Form
    {
        static private Color operatorsBackground = Color.LightGray;
        static private Color numbersBackground = Color.WhiteSmoke;
        static private Color equalSignBackground = Color.LightSeaGreen;

        public enum SymbolType
        {
            Number,
            Operator,
            EqualSign,
            DecimalPoint,
            PlusMinusSign,
            Backspace,
            ClearAll,
            ClearEntry,
            Undefined
        }

        public struct BtnStruct
        {
            public char Content;
            public SymbolType Type;

            public BtnStruct(char content, SymbolType type = SymbolType.Undefined)
            {
                this.Content = content;
                this.Type = type;
            }
        }

        private BtnStruct[,] buttons =
        {
            { new BtnStruct('%'), new BtnStruct('\u0152', SymbolType.ClearEntry), new BtnStruct('C', SymbolType.ClearAll), new BtnStruct('\u232B', SymbolType.Backspace)},
            { new BtnStruct('\u215F'), new BtnStruct('\u00B2'), new BtnStruct('\u221A'), new BtnStruct('\u00F7', SymbolType.Operator)},
            { new BtnStruct('7', SymbolType.Number), new BtnStruct('8', SymbolType.Number), new BtnStruct('9', SymbolType.Number), new BtnStruct('\u00D7', SymbolType.Operator)},
            { new BtnStruct('4', SymbolType.Number), new BtnStruct('5', SymbolType.Number), new BtnStruct('6', SymbolType.Number), new BtnStruct('-', SymbolType.Operator)},
            { new BtnStruct('1', SymbolType.Number), new BtnStruct('2', SymbolType.Number), new BtnStruct('3', SymbolType.Number), new BtnStruct('+', SymbolType.Operator)},
            { new BtnStruct('\u00B1', SymbolType.PlusMinusSign), new BtnStruct('0', SymbolType.Number), new BtnStruct(',', SymbolType.DecimalPoint), new BtnStruct('=', SymbolType.EqualSign)},
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
                    switch (buttons[i, j].Type)
                    {
                        case SymbolType.Number:
                            myButton.BackColor = numbersBackground;
                            break;
                        case SymbolType.Operator:
                            myButton.BackColor = operatorsBackground;
                            break;
                        case SymbolType.EqualSign:
                            myButton.BackColor = equalSignBackground;
                            break;
                        case SymbolType.DecimalPoint:
                            myButton.BackColor = numbersBackground;
                            break;
                        case SymbolType.PlusMinusSign:
                            myButton.BackColor = numbersBackground;
                            break;
                        default:
                            myButton.BackColor = operatorsBackground;
                            break;
                    }
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
            switch (clickedButtonStruct.Type)
            {
                case SymbolType.Number:
                    if (lblResult.Text == "0") lblResult.Text = "";
                    // lblResult.Text += clickedButtonStruct.Content.ToString();
                    lblResult.Text += clickedButton.Text;
                    break;
                case SymbolType.Operator:
                    break;
                case SymbolType.EqualSign:
                    break;
                case SymbolType.DecimalPoint:
                    if (!lblResult.Text.Contains(","))
                        lblResult.Text += ",";
                    break;
                case SymbolType.PlusMinusSign:
                    if (lblResult.Text != "0")
                        if (!lblResult.Text.Contains("-"))
                            lblResult.Text = "-" + lblResult.Text;
                        else
                            lblResult.Text = lblResult.Text.Substring(1);
                    break;
                case SymbolType.Backspace:
                    // cancello sempre l'ultimo carattere della stringa
                    lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
                    if (lblResult.Text == "-0" || lblResult.Text == "-" || lblResult.Text == "")
                        lblResult.Text = "0";
                    break;
                case SymbolType.ClearAll:
                case SymbolType.ClearEntry:
                    lblResult.Text = "0";
                    break;
                case SymbolType.Undefined:
                    break;
                default:
                    break;
            }
        }

        private void lblResult_TextChanged(object sender, EventArgs e)
        {
            // Formattiamo con separatore decimale e separatore delle migliaia
            if (lblResult.Text.Length>0 && lblResult.Text != "-")
            {
                if (!decimal.TryParse(lblResult.Text, out decimal result))
                    lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
                decimal num = decimal.Parse(lblResult.Text);
                NumberFormatInfo nfi = new CultureInfo("it-IT", false).NumberFormat;
                int decimalSeparatorPosition = lblResult.Text.IndexOf(',');
                nfi.NumberDecimalDigits = 
                    decimalSeparatorPosition == -1 ?
                    0 :
                    lblResult.Text.Length - decimalSeparatorPosition - 1;
                string stOut = num.ToString("N", nfi);
                if (decimalSeparatorPosition == lblResult.Text.Length - 1) stOut += ",";
                lblResult.Text = stOut;
            }

            // Controlliamo lunghezza per dimensione label
            int textWidth = TextRenderer.MeasureText(lblResult.Text, lblResult.Font).Width;
            const int lblResultHorizontalMargin = 24; const int lblDefaultFontSize = 36;
            float newSize = lblResult.Font.Size * (((float)lblResult.Size.Width - lblResultHorizontalMargin) / textWidth);
            if (newSize > lblDefaultFontSize) newSize = lblDefaultFontSize;
            lblResult.Font = new Font("Segoe UI Semibold", newSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
