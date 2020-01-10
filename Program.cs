using System.Collections.Generic;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using platzi_curso_csharp.Entidades;
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
            var ListaObjetos = engine.GetObjetosEscuela(out int Conteo_Evaluaciones,
            out int Conteo_Alumnos,
            out int Conteo_Asignaturas,
            out int Conteo_Cursos);

            var DicTemp = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(DicTemp);
            // Dictionary<int, string> Diccionario = new Dictionary<int, string>();
            // Diccionario.Add(10, "Juan");
            // Diccionario.Add(23, "Lorem impsum");


            // foreach (var llaveValor in Diccionario)
            // {
            //     WriteLine(llaveValor.Value);
            //     WriteLine(llaveValor.Key);
            // }

            // Printer.WriteTitle("Acceso a diccionario");
            // WriteLine(Diccionario[10]);
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
