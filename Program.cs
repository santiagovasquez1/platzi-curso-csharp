using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad: 10);
            ImpimirCursosEscuela(engine.Escuela);
            var ListaObjetos=engine.GetObjetoEscuelas();

            // Printer.DrawLine(20);
            // Printer.DrawLine(20);
            // Printer.DrawLine(20);
            // Printer.WriteTitle("Pruebas de poliformismo");

            // var Alumno_Test = new Alumno { Nombre = "Pepito Perez" };
            // ObjetoEscuelaBase objeto = Alumno_Test;

            // Printer.WriteTitle("Alumno_or");
            // WriteLine($"Alumno: {Alumno_Test.Nombre}");
            // WriteLine($"Alumno: {Alumno_Test.UniqueId}");
            // WriteLine($"Alumno: {Alumno_Test.GetType()}");

            // Printer.WriteTitle("Alumno_pol");
            // WriteLine($"Alumno: {objeto.Nombre}");
            // WriteLine($"Alumno: {objeto.UniqueId}");
            // WriteLine($"Alumno: {objeto.GetType()}");
            // var listaObjetos = engine.GetObjetosEscuela();
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
