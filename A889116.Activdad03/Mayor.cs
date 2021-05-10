﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A889116.Activdad03
{
    class Mayor
    {
        private static readonly Dictionary<int, Cuenta> entradasC;
     
        const string nombreArchivo = "Mayor.txt";


        static Mayor()
        {

            entradasC = new Dictionary<int, Cuenta>();
            

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuentas = new Cuenta(linea);
                        entradasC.Add(cuentas.CodCuenta, cuentas);
                    }
                }

            }

        }

        public static bool Existe(int codigoCuenta)
        {
            return entradasC.ContainsKey(codigoCuenta);
        }


        public static bool Actualizar()
        {
            bool actualizar = false;
            do
            {
                if (entradasC.Count == 0)
                {

                    

                    Console.WriteLine("No es posible actualizar las cuentas del libro mayor ya que no hay cuentas cargadas.");
                }
                else
                {
                    foreach (var cuenta in entradasC.Values)
                    {
                        var codigoCuenta = cuenta.CodCuenta;
                        var fechaCuenta = cuenta.Fecha;
                        decimal debe = 0;
                        decimal haber = 0;

                        Diario.MovimientosFuturos(codigoCuenta, fechaCuenta, ref debe, ref haber);

                        if (debe != 0 || haber != 0)
                        {
                            cuenta.Debe += debe;
                            cuenta.Haber += haber;
                            cuenta.Fecha = DateTime.Today;
                        }
                    }
                    Mayor.Grabar();
                }
                actualizar = true;

            } while (actualizar == false);

            return actualizar;
        }


        public static void MostrarDatosActualizados()
        {
            string Mensaje = "";
            foreach (var cuentas in entradasC.Values)
            {
                if (cuentas.Fecha == DateTime.Today)
                {
                    Mensaje += $"Cuenta: {cuentas.CodCuenta}\n" +
                               $"Fecha: {DateTime.Now.ToShortDateString()} \n" +
                               $"Saldo del Debe: {cuentas.Debe}\n" +
                               $"Saldo del Haber: {cuentas.Haber}\n" +
                               System.Environment.NewLine;
                }
            }
            if (Mensaje == "")
            {
                Console.WriteLine("No se actualizó ninguna cuenta.");
            }
            if (Mensaje != "")
            {
                Console.WriteLine("Las cuentas y los datos que fueron actualizados son: " + System.Environment.NewLine + Mensaje);
            }
        }


        public static Cuenta Seleccionar()
        {
            var modelo = Cuenta.CrearModeloBusqueda();
            foreach (var cuentas in entradasC.Values)
            {
                if (cuentas.CoincideCon(modelo))
                {
                    return cuentas;
                }
            }

            Console.WriteLine("No se ha encontrado una cuenta que coincida");
            return null;
        }

        public static void Grabar()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var cuentas in entradasC.Values)
                {
                    var linea = cuentas.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }


    }
}

