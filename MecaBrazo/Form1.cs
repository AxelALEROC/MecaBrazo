using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace MecaBrazo
{
    public partial class Form1 : Form
    {
        private Button button0;
        private Button button1;
        private Button button2;
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button0 = CreateButton("0", 10, 10);
            button1 = CreateButton("1", 70, 10);
            button2 = CreateButton("2", 130, 10);

            this.Controls.Add(button0);
            this.Controls.Add(button1);
            this.Controls.Add(button2);

            serialPort = new SerialPort("COM6", 9600);
            serialPort.Open();

            this.KeyPreview = true; // Habilitamos la detección de teclas presionadas en el formulario
            this.KeyDown += Form1_KeyDown; // Asociamos el evento KeyDown al método Form1_KeyDown
        }

        private Button CreateButton(string text, int x, int y)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Click += Button_Click; // Asociamos el evento Click al método Button_Click
            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string letter = button.Text;
            serialPort.Write(letter);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.A:
                    button0.PerformClick(); // Simulamos un clic en el botón 0
                    break;
                case Keys.D1:
                case Keys.W:
                    button1.PerformClick(); // Simulamos un clic en el botón 1
                    break;
                case Keys.D2:
                case Keys.E:
                    button2.PerformClick(); // Simulamos un clic en el botón 2
                    break;
                default:
                    break;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}
