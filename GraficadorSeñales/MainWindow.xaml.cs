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
                      
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);

           
            //FuncionSigno señal = new FuncionSigno();
            Señal señal;

            switch (CbTipoSenal.SelectedIndex)
            {
                case 0:
                    señal = new SeñalParabolica();
                    break;
                   
                case 1://Casteo y luego parse 
                    double amplitud = double.Parse(
                    ((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtAmplitud.Text);


                    double fase = double.Parse(
                    ((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);

                    double frecuencia = double.Parse(
                    ((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                case 2:
                    double alpha = double.Parse(
                        ((ConfiguaracionExponencial)(panelConfiguracion.Children[0])).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    
                    break;
                case 3:
                    string rutaArchivo = ((ConfiguracionAudio)(panelConfiguracion.Children[0])).txtRutaArchivo.Text;
                        señal = new SeñalAudio(rutaArchivo);
                    txtTiempoInicial.Text = señal.TiempoInicial.ToString();
                    txtTiempoFinal.Text = señal.TiempoFinal.ToString();
                    txtFrecuenciaMuestreo.Text = señal.FrecuenciaMuestreo.ToString();
                    break;
                default:
                    señal = null;
                    break;
            }
             if (CbTipoSenal.SelectedIndex != 3 && señal != null)
            {
                señal.construirSeña();

                señal.TiempoInicial = tiempoInicial;
                señal.TiempoFinal = tiempoFinal;
                señal.FrecuenciaMuestreo = frecuenciaMuestreo;
            }
          

            

            double amplitudMaxima = señal.AmplitudMaxima;

            plnGrafica.Points.Clear();

            
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

       

        private void CbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion.Children.Clear();
            switch (CbTipoSenal.SelectedIndex)
            {
                case 0: //Exponencial
                    break;
                case 1: //Senoidal
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;
                case 2:
                    panelConfiguracion.Children.Add(new ConfiguaracionExponencial());
                    break;
                case 3:
                    panelConfiguracion.Children.Add(new ConfiguracionAudio());
                    break;
                default:
                    break;

            }
        }
    }
}