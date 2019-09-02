//Tony Stark estaria orgulloso de esto
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            //convertido de String a Variable Num
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);

            //Mandar a llamar
            //SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);
            //SeñalParabolica señal = new SeñalParabolica();
            FuncionSigno señal = new FuncionSigno();

            double periodoMuestreo = 1.0 / frecuenciaMuestreo;

            //Declarar la Amplitud Máxima
            double amplitudMaxima = 0.0;

            //Para borrar la grafica anterior
            plnGrafica.Points.Clear();

            //para poder reuperar el valor de la señal 
            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = señal.evaluar(i);
                if(Math.Abs(valorMuestra) > amplitudMaxima)
                {
                    amplitudMaxima = Math.Abs(valorMuestra);
                }
                Muestra muestra = new Muestra(i, señal.evaluar(i));
                señal.Muestras.Add(muestra);

            }
            //para graficar los puntos
            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(adaptarCoordenadas(muestra.X, muestra.Y, tiempoInicial,amplitudMaxima));
            }

            lbllimiteSuperior.Text = amplitudMaxima.ToString();
            lbllimiteInferior.Text = "-" + amplitudMaxima.ToString();

            plnEjeX.Points.Clear(); 
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoInicial, 0.0, tiempoInicial, amplitudMaxima));
            plnEjeX.Points.Add(adaptarCoordenadas(tiempoFinal, 0.0, tiempoInicial, amplitudMaxima));

            plnEjeY.Points.Clear();
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima,tiempoInicial, amplitudMaxima));
            plnEjeY.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima * -1, tiempoInicial, amplitudMaxima));

        }

        //Nueva funcion 
        public Point adaptarCoordenadas(double x, double y, double tiempoInicial, double amplitudMaxima)
        {
            return new Point( (x - tiempoInicial) * scrGrafica.Width, 
                -y * ( ((scrGrafica.Height / 2.0) - 25) )/ amplitudMaxima + (scrGrafica.Height / 2.0) );
        }
    }
}