using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace JustMockTest
{
    [TestClass]
    public class CineTest
    {
        [TestMethod]
        public void SiExistenButacasLibresComproTicketsEntoncesReservoButacas()
        {
            string pelicula = "La Vida Es Bella";
            int cantidadEntrada = 2;

            var cine = Mock.Create<Cine>();
            cine.Arrange(c => c.ButacasLibres(pelicula)).Returns(20);
            cine.Arrange(c => c.Descargar(pelicula, cantidadEntrada)).Returns(new List<string> { "E1", "E2" });          
           

            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntrada, pelicula);

            Assert.IsTrue(persona.ObtuvoEntradas);
            cine.Assert(c => c.ButacasLibres(pelicula));
            cine.Assert(c => c.Descargar(pelicula, cantidadEntrada));

        }

        [TestMethod]
        public void DadoQueNoHayButacasCuandoComproEntradasEntoncesNoSeReservanButacas()
        {
            string pelicula = "La Vida Es Bella";
            int cantidadEntrada = 2;

            var cine = Mock.Create<Cine>();
            cine.Arrange(c => c.ButacasLibres(pelicula)).Returns(0);
            
            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntrada, pelicula);

            Assert.IsFalse(persona.ObtuvoEntradas);
            cine.Assert(c => c.ButacasLibres(pelicula));
        }

        [TestMethod]
        public void DadoQueNoHayTodasLasEntradasQueQuieroCuandoComproConsigoLasQueEstanDisponibles()
        {
            string pelicula = "La Vida Es Bella";
            int cantidadEntrada = 6;
            int entradasDisponible = 5;

            var cine = Mock.Create<Cine>();
            cine.Arrange(c => c.ButacasLibres(pelicula)).Returns(entradasDisponible);
            cine.Arrange(c => c.Descargar(pelicula, 5)).Returns(new List<string> { "E1", "E2", "E3", "E4", "E5" });

            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntrada, pelicula);

            Assert.IsTrue(persona.ObtuvoEntradas);
            Assert.AreEqual(entradasDisponible, persona.Entradas.Count);
            cine.Assert(c => c.ButacasLibres(pelicula));
            cine.Assert(c => c.Descargar(pelicula, entradasDisponible));
        }

        [TestMethod]
        public void DadoQueNoHayTodasLasEntradasQueQuieroCuandoComproConsigoSoloUna()
        {
            string pelicula = "La Vida Es Bella";
            int cantidadEntrada = 7;
            int entradasDisponible = 5;
            int entradasEsperadas = 1;

            var cine = Mock.Create<Cine>();
            cine.Arrange(c => c.ButacasLibres(pelicula)).Returns(entradasDisponible);
            cine.Arrange(c => c.Descargar(pelicula, entradasEsperadas)).Returns(new List<string> { "E1" });

            var modeloCompra = Mock.Create<IModeloCompra>();
            modeloCompra.Arrange(m => m.Comprar(cine, cantidadEntrada, pelicula)).Returns(new List<string> { "E1" });

            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntrada, pelicula, modeloCompra);

            Assert.IsTrue(persona.ObtuvoEntradas);
            Assert.AreEqual(entradasDisponible, persona.Entradas.Count);
            modeloCompra.Assert(m => m.Comprar(cine, cantidadEntrada, pelicula));
            cine.Arrange(c => c.ButacasLibres(pelicula));
            cine.Arrange(c => c.Descargar(pelicula, entradasEsperadas));
        }
    }
}
