using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using System;
using static System.Console;

namespace CoreEscuela
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(1000, 1000, 1);
            //AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var EvalList = reporteador.GetListaEvaluaciones();
            var AsignaturaList = reporteador.GetListAsignaturas();
            var DicEval = reporteador.GetDicEvaluacionesPorAsignatura();
            var ListaPromXAsignatura = reporteador.GetPromedioAlumnosPorAsignatura();

            Printer.WriteTitle("Captura evaluación por consola");

            string Nombre, notaString;
            var newEval = new Evaluación();

            WriteLine("Ingrese el nombre de la evaluación:");
            Printer.PresioneENTER();
            Nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Printer.WriteTitle("El valor del nombre no puede ser vacio");
                WriteLine("Saliendo del programa");

            }
            else
            {
                newEval.Nombre = Nombre.ToLower();
                WriteLine("El nombre de la evaluación ha sido ingresado correctamente.");
            }

            WriteLine("Ingrese la nota de la evaluación:");
            Printer.PresioneENTER();
            notaString = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(notaString))
            {
                Printer.WriteTitle("El valor de la nota no puede ser vacio");
                WriteLine("Saliendo del programa");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notaString);
                    if (newEval.Nota < 0 | newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe de estar entre 0 y 5");
                    }
                    WriteLine("la nota de la evaluación ha sido ingresado correctamente.");
                    return;
                }
                catch (ArgumentOutOfRangeException arge)
                {
                    Printer.WriteTitle(arge.Message);
                    WriteLine("Saliendo del programa");
                }
                catch (Exception)
                {
                    Printer.WriteTitle("El valor de la nota no es un número valido");
                    WriteLine("Saliendo del programa");
                }
                finally
                {
                    Printer.WriteTitle("FINALLY");
                    Printer.Beep(2500, 500, 3);
                }
            }


            // foreach (var item in ListaPromXAsignatura)
            // {
            //     foreach (var alumn in item.Value)
            //     {
            //         var temp = alumn as Alumno;

            //     }
            // }

            //Printer.Beep(10000, cantidad: 10);
            // ImpimirCursosEscuela(engine.Escuela);
            // var ListaObjetos = engine.GetObjetosEscuela(out int Conteo_Evaluaciones,
            // out int Conteo_Alumnos,
            // out int Conteo_Asignaturas,
            // out int Conteo_Cursos);

            // var DicTemp = engine.GetDiccionarioObjetos();
            // engine.ImprimirDiccionario(DicTemp, true);
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

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo");
            //Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIO");
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