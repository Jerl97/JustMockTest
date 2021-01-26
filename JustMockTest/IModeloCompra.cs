using System.Collections.Generic;

namespace JustMockTest
{
    public interface IModeloCompra
    {
        List<string> Comprar(Cine cine, int cantidadEntrada, string pelicula);
    }
}