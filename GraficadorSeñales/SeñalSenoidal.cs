using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalSenoidal : Señal
    {
        public double Amplitud { get; set; }
        public double Fase { get; set; }
        public double Frecuencia { get; set; }
        

        public SeñalSenoidal()
        {
            Amplitud = 1.0;
            Fase = 0.0;
            Frecuencia = 1.0;
            //para que pueda tener elem nuevos
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0; 
        }

        public SeñalSenoidal(double amplitud,
            double fase, double frecuencia)
        {
            Amplitud = amplitud;
            Fase = fase;
            Frecuencia = frecuencia;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        /*TEOREMA DE MUESTREO
         fs = 2fmax + 1*/
        override public double evaluar(double tiempo)
        {
            double resultado;
            //la  formula de señal S. comienza hacia arriba
           
            resultado =
                Amplitud * Math.Sin(
                    ((2 * Math.PI * Frecuencia) *
                    tiempo) + Fase);
            return resultado;
        }

      
    }
}
