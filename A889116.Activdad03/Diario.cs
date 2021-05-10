using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A889116.Activdad03
{
    class Diario
    {
        private static readonly Dictionary<int, Asientos> entradas;
        const string nombreArchivo = "Diario.txt";

        

        static Diario()
        {
            entradas = new Dictionary<int, Asientos>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var asientos = new Asientos(linea);
                        entradas.Add(asientos.NroAsiento, asientos);
                    }
                }

            }

        }

        public static void MovimientosFuturos(int codigoCuenta, DateTime fecha, ref decimal debe, ref decimal haber)
        {
            if (entradas.Count == 0)
            {
                Console.WriteLine("No existen asientos cargados en el libro diario.");
            }
            else
            {
                foreach (var asiento in entradas.Values)
                {
                    if (codigoCuenta == asiento.CodCuenta)
                    {
                        if (fecha < asiento.Fecha)
                        {
                            debe += asiento.Debe;
                            haber += asiento.Haber;
                        }
                    }

                }
            }
        }

        public static Asientos Seleccionar()
        {
            var modelo = Asientos.CrearModeloBusqueda();
            foreach (var asientos in entradas.Values)
            {
                if (asientos.CoincideCon(modelo))
                {
                    return asientos;
                }
            }

            Console.WriteLine("No se ha encontrado una cuenta que coincida");
            return null;
        }
    }
}
