using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;//contiene toda la lectura de archivo

namespace GraficadorSeñales
{
    class SeñalAudio : Señal
    {
        public string RutaArchivo { get; set; }
        //constructor /rutaArchivo es el parametro que contiene la ruta 
        public SeñalAudio(string rutaArchivo)
        {
            RutaArchivo = rutaArchivo;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0;

            AudioFileReader reader = new AudioFileReader(rutaArchivo);

            TiempoInicial = 0.0;
            TiempoFinal = reader.TotalTime.TotalSeconds;
            FrecuenciaMuestreo = reader.WaveFormat.SampleRate;

            //ir leyendo las muestras de una en una
            var bufferLectura = new float[reader.WaveFormat.Channels];

            double instanteActual = 0.0;
            //-->inverso frec. muestreo--> del audio
            double periodoMuestreo = 1.0 / FrecuenciaMuestreo; 
            int muestrasLeidas = 0;
            do
            {
                muestrasLeidas = reader.Read(bufferLectura,0,reader.WaveFormat.Channels);
                if (muestrasLeidas > 0)
                {
                    double max = bufferLectura.Take(muestrasLeidas).Max();
                    Muestras.Add(new Muestra(instanteActual, max));
                    if (Math.Abs(max)> AmplitudMaxima)
                    {
                        AmplitudMaxima = Math.Abs(max);
                    }
                }
                instanteActual += periodoMuestreo;
            } while (muestrasLeidas > 0);
        }
        public override double evaluar(double tiempo)
        {
            throw new NotImplementedException();
        }
    }
}
