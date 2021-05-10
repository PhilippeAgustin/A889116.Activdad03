using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889116.Activdad03
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Menu Principal");
                Console.WriteLine("-------------");

                Console.WriteLine("1 - Consultar libro mayor");
                Console.WriteLine("2 - Actualizar libro mayor");
                Console.WriteLine("3 - Consultar libro diario");
                Console.WriteLine("4 - Salir");

                Console.WriteLine("Ingrese una opción y presione [Enter]");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Consultar();
                        break;
                    case "2":
                        ActualizarMayor();
                        break;
                    case "3":
                        MostrarLDiario();
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("No ha ingresado una opción del menú");
                        break;
                }

            } while (!salir);
        }

        private static void Consultar()
        {
            var cuentas = Mayor.Seleccionar();
            cuentas?.Mostrar();
        }

        private static void ActualizarMayor()
        {
            var cuentas = Mayor.Actualizar();
            if (cuentas == false)
            {
                return;
            }

            Mayor.MostrarDatosActualizados();
        }

        //Para usar de referencia, se puede borrar la opcion
        private static void MostrarLDiario()
        {
            var asientos = Diario.Seleccionar();
            asientos?.Mostrar();
        }
    }
}

