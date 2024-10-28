using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;

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
            // Navegar a la página de creación de productos
            NavegarAPagina("https://localhost:7157/create-product");
            EsperarCarga();

            // Completar el formulario
            CompletarFormulario();

            // Hacer clic en el botón "Guardar"
            HazClickEnBoton("Guardar");
            EsperarCarga();

            // Opcional: verificar que la navegación de regreso funcione
            HazClickEnBoton("regresar");
            EsperarCarga();
        }

        private void NavegarAPagina(string url)
        {
            Console.WriteLine("Navegando a: " + url);
            driver.Navigate().GoToUrl(url);
        }

        private void EsperarCarga()
        {
            Console.WriteLine("Esperando a que la página cargue...");
            System.Threading.Thread.Sleep(1000); // Espera de carga de 1 segundo
        }

        private void CompletarFormulario()
        {
            Console.WriteLine("Completando el formulario...");

            var inputs = driver.FindElements(By.XPath("//input | //textarea")); // Encuentra todos los inputs y textareas

            if (inputs.Count >= 3) // Asegúrate de que haya al menos 3 campos
            {
                inputs[0].SendKeys("Nombre del producto"); // Primer campo de texto
                inputs[1].SendKeys("Descripción del producto"); // Segundo campo de texto
                inputs[2].SendKeys("19.99"); // Campo de precio (decimal)
                Console.WriteLine("Valores ingresados en los campos.");
            }
            else
            {
                Console.WriteLine("No se encontraron suficientes campos en el formulario.");
                throw new Exception("Formulario incompleto: No se encontraron suficientes campos.");
            }

            // Si hay selectores, manejarlos
            var selects = driver.FindElements(By.TagName("select"));
            foreach (var select in selects)
            {
                var selectElement = new SelectElement(select);
                selectElement.SelectByIndex(0); // Cambia el índice según lo que necesites
                Console.WriteLine($"Seleccionando el primer elemento en el selector: {select.GetAttribute("id")}");
            }

            Console.WriteLine("Formulario completado.");
        }

        private bool HazClickEnBoton(string nombre)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var btn = wait.Until(d => d.FindElement(By.XPath($"//button[text()='{nombre}']")));
                Console.WriteLine($"Haciendo clic en el botón '{nombre}'.");
                btn.Click();
                return true;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine($"No se pudo encontrar el botón '{nombre}': " + e.Message);
                return false;
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine($"El botón '{nombre}' no está disponible para hacer clic: " + e.Message);
                return false;
            }
        }

        public void Dispose()
        {
            //driver.Quit();
        }
    }
}
