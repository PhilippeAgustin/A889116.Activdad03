using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889116.Activdad03
{
    class Asientos
    {


        public int NroAsiento { get; set; }
        public DateTime Fecha { get; set; }
        public int CodCuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }

        public Asientos() { }

        public Asientos(string linea)
        {
            var datos = linea.Split(';');
            NroAsiento = int.Parse(datos[0]);
            Fecha = DateTime.Parse(datos[1]);
            CodCuenta = int.Parse(datos[2]);
            Debe = decimal.Parse(datos[3]);
            Haber = decimal.Parse(datos[4]);
        }

        public void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine($"Numero de asiento {NroAsiento}");
            Console.WriteLine($"Fecha {Fecha.ToShortDateString()}");
            Console.WriteLine($"Codigo cuenta {CodCuenta}");
            Console.WriteLine($"Debe {Debe}");
            Console.WriteLine($"Haber {Haber}");
            Console.WriteLine();
        }

        public static Asientos CrearModeloBusqueda()
        {
            var modelo = new Asientos();
            modelo.NroAsiento = IngresarNumeroAsiento(obligatorio: false);
            return modelo;
        }

        public bool CoincideCon(Asientos modelo)
        {
            if (modelo.NroAsiento != 0 && modelo.NroAsiento != NroAsiento)
            {
                return false;
            }
            return true;

        }

        

        private static int IngresarNumeroAsiento(bool obligatorio = true)
        {
            var titulo = "Ingrese el numero de asiento";
            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar";
            }

            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }

                if (!int.TryParse(ingreso, out var numeroAsiento))
                {
                    Console.WriteLine("No ha ingresado un numero de asiento válido");
                    continue;
                }

                return numeroAsiento;

            } while (true);

        }
    }
}
