﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{ //Agregar herencia de la clase señal
    class SeñalParabolica : Señal
    {
       
        public SeñalParabolica()
        {
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }
        //Para que no piense que es algo completamente nuevo
        override public double evaluar(double tiempo)
        {
            double resultado;

            if (tiempo > 0)
            {
                resultado = (tiempo * tiempo) / 2.0;
            }
            else
            {
                resultado = 0.0;
            }

            return resultado;
        }
    }
}

