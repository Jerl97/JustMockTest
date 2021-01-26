using System;
using System.Collections.Generic;
using System.Linq;

namespace JustMockTest
{
    internal class Persona
    {
        public bool ObtuvoEntradas { get { return Entradas != null && Entradas.Any();}}

        public List <string>Entradas { get; internal set; }

        internal void CompraEntradas(Cine cine, int cantidadEntrada, string pelicula)
        {
            var butacasLibres = cine.ButacasLibres(pelicula);

            if (butacasLibres>= cantidadEntrada)
            {
                Entradas = cine.Descargar(pelicula, cantidadEntrada);

            }
            else
            {
                Entradas = cine.Descargar(pelicula, butacasLibres);
            }

           
        }
        internal void CompraEntradas(Cine cine, int cantidadEntrada, string pelicula, IModeloCompra modeloCompra)
        {
            Entradas = modeloCompra.Comprar(cine, cantidadEntrada, pelicula);
        }
    }
}