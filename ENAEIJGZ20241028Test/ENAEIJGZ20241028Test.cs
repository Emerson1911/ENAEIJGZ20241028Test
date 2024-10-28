using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;

namespace ENAEIJGZ20241028Test
{
    [TestClass]
    public class ENAEIJGZ20241028Test : IDisposable
    {
        private IWebDriver driver;

        public ENAEIJGZ20241028Test()
        {
            driver = new ChromeDriver();
        }

        [TestMethod]
        public void crearproducto()
        {
            NavegarAPagina("https://localhost:7157/product-list");
            EsperarCarga();

            if (HazClickEnBoton("crear"))
            {
                EsperarCarga();
                CompletarFormulario("PruebaN", "PruebaD", "100");
                HazClickEnBoton("guardar");
                EsperarCarga();
                HazClickEnBoton("regresar");
                EsperarCarga();
            }
        }

        private void NavegarAPagina(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        private void EsperarCarga()
        {
            System.Threading.Thread.Sleep(1000);
        }

        private bool HazClickEnBoton(string nombre)
        {
            try
            {
                var btn = driver.FindElement(By.Name(nombre));
                btn.Click();
                return true;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine($"No se pudo encontrar el botón '{nombre}': " + e.Message);
                return false;
            }
        }

        private void CompletarFormulario(string nombre, string descripcion, string precio)
        {
            driver.FindElement(By.Id("Nombre")).SendKeys(nombre);
            driver.FindElement(By.Id("Descripcion")).SendKeys(descripcion);
            driver.FindElement(By.Id("Precio")).SendKeys(precio);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}

