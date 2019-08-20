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
            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);

            double periodoMuestreo = 1.0 / frecuenciaMuestreo;

            //Para borrar la grafica anterior
            plnGrafica.Points.Clear();

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                //i * ancho grafica para visualizar mejor la grafica. Se escala
                //El compenente se le suma scrGrafica.Height /2 para visualizarlo en Polyline
                
                plnGrafica.Points.Add(adaptarCoordenadas(i, señal.evaluar(i)));

            }  
        }
        //Nueva funcion 
        public Point adaptarCoordenadas(double x, double y)
        {
          

            return new Point( x * scrGrafica.Width, y * ((scrGrafica.Height / 2.0) - 25) + (scrGrafica.Height / 2.0) );
        }
    }
}

/*Its been a long day with out you my friend 
 Halt dich and mir fest 

     */