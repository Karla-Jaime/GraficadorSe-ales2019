using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Muestra
    {//cada vez que se crea un nuevo valor
        /*Guardad Instante de tiempo y su valor*/
        //autopropertis
        public double X { get; set; }
        public double Y { get; set; }
        
        public Muestra()
        {
            X = 0.0;
            Y = 0.0;
        }

        public Muestra(double x, double y)
        {
            X = x;
            Y = y;

        }
    }
}
