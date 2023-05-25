using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace OrdenamientoInsertion
{
    public partial class Insercion : Form
    {
        bool estado = false;
        int[] arregloNumeros;
        Button[] arregloBotones;
        Numeros datos = new Numeros();

        public Insercion()
        {
            InitializeComponent();
        }

        private void Insercion_Load(object sender, EventArgs e)
        {

        }

        public void InsertionSort(ref int[] arreglo, ref Button[] arregloNumeros)
        {
            int n = arreglo.Length;
            for (int i = 1; i < n; i++)
            {
                int clave = arreglo[i];
                int j = i - 1;

                while (j >= 0 && arreglo[j] > clave)
                {
                    Intercambio(ref arregloNumeros, j + 1, j);
                    arreglo[j + 1] = arreglo[j];
                    j = j - 1;
                }
                arreglo[j + 1] = clave;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int num = Convert.ToInt32(txtNumero.Text);
                datos.InsertarDato(num);
                arregloNumeros = datos.ObtenerArreglo();

                arregloBotones = datos.ArregloBotones();
            }
            catch
            {
                MessageBox.Show("Solo se admiten números enteros");
            }
            estado = true;
            tabPage1.Refresh();
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            if (estado)
            {
                Point xy = new Point(50, 70);
                try
                {
                    DibujarArreglo(ref arregloBotones, xy, ref tabPage1);
                }
                catch
                {
                }
                estado = false;
            }
        }

        public void DibujarArreglo(ref Button[] arreglo, Point xy, ref TabPage t)
        {
            for (int i = 1; i < arreglo.Length; i++)
            {
                arreglo[i].Location = xy;
                t.Controls.Add(arreglo[i]);
                xy += new Size(70, 0);
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            btnOrdenar.Enabled = false;
            txtNumero.Enabled = false;
            btnAgregar.Enabled = false;

            InsertionSort(ref arregloNumeros, ref arregloBotones);
            this.Cursor = Cursors.Default;

            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
        }

        public void Intercambio(ref Button[] boton, int a, int b)
        {
            string temp = boton[a].Text;

            Point pa = boton[a].Location;
            Point pb = boton[b].Location;
            int diferencia = pa.X - pb.X;
            int x = 10;
            int y = 10;
            int t = 70;

            while (y != 70)
            {
                Thread.Sleep(t);
                boton[a].Location += new Size(0, 10);
                boton[b].Location += new Size(0, -10);
                y += 10;
            }
            while (x != diferencia + 10)
            {
                Thread.Sleep(t);
                boton[a].Location += new Size(-10, 0);
                boton[b].Location += new Size(10, 0);
                x += 10;
            }

            y = 0;

            while (y != -60)
            {
                Thread.Sleep(t);
                boton[a].Location += new Size(0, -10);
                boton[b].Location += new Size(0, +10);
                y -= 10;
            }

            boton[a].Text = boton[b].Text;
            boton[b].Text = temp;
            boton[b].Location = pb;
            boton[a].Location = pa;
            estado = true;
            tabPage1.Refresh();
        }
    }

    class Numeros
    {
        private int longitud;
        private int[] arreglo = new int[1];
        private Button[] arregloBotones = new Button[1];

        public Numeros()
        {
            int a = 0;
            arreglo[0] = a;
            arregloBotones[0] = new Button();
            arregloBotones[0].Width = 40;
            arregloBotones[0].Height = 40;
            arregloBotones[0].BackColor = Color.GreenYellow;
            arregloBotones[0].Text = a.ToString();
            CalcularLongitud();
        }

        public void CalcularLongitud()
        {
            longitud = arreglo.Length;
        }

        public int ObtenerLongitud()
        {
            return longitud;
        }

        public int[] ObtenerArreglo()
        {
            return arreglo;
        }

        public void InsertarDato(int dato)
        {
            Array.Resize<int>(ref arreglo, longitud + 1);
            arreglo[longitud] = dato;
            Array.Resize<Button>(ref arregloBotones, longitud + 1);
            arregloBotones[longitud] = new Button();
            arregloBotones[longitud].Width = 50;
            arregloBotones[longitud].Height = 50;
            arregloBotones[longitud].BackColor = Color.GreenYellow;
            arregloBotones[longitud].Text = dato.ToString();
            CalcularLongitud();
        }

        public Button[] ArregloBotones()
        {
            return arregloBotones;
        }
    }
}