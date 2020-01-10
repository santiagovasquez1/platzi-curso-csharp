using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using platzi_curso_csharp.Entidades;

namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academay", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        #region  Cargar

        private void CargarEvaluaciones()
        {

            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
        #endregion



        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int Conteo_Evaluaciones,
            out int Conteo_Alumnos,
            out int Conteo_Asignaturas,
            out int Conteo_Cursos,
            bool Trae_Evaluciacones = true,
            bool Trae_Alumnos = true,
            bool Trae_Asignaturas = true,
            bool Trae_Curos = true)
        {
            var listaObj = new List<ObjetoEscuelaBase>();

            Conteo_Evaluaciones = 0;
            Conteo_Asignaturas = 0;
            Conteo_Alumnos = 0;
            listaObj.Add(Escuela);

            if (Trae_Curos)
                listaObj.AddRange(Escuela.Cursos);
            Conteo_Cursos = Escuela.Cursos.Count;

            foreach (var curso in Escuela.Cursos)
            {
                Conteo_Asignaturas += curso.Asignaturas.Count;
                Conteo_Alumnos += curso.Alumnos.Count;

                if (Trae_Asignaturas)
                    listaObj.AddRange(curso.Asignaturas);
                if (Trae_Alumnos)
                    listaObj.AddRange(curso.Alumnos);

                if (Trae_Evaluciacones == true)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        Conteo_Evaluaciones++;
                    }
                }

            }

            return listaObj.AsReadOnly();
        }

        //Sobrecarga de metodos
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(bool Trae_Evaluciacones = true,
            bool Trae_Alumnos = true,
            bool Trae_Asignaturas = true,
            bool Trae_Curos = true)
        {
            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        public Dictionary<Llaves_Diccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var Diccionario = new Dictionary<Llaves_Diccionario, IEnumerable<ObjetoEscuelaBase>>();

            Diccionario.Add(Llaves_Diccionario.Escuela, new[] { Escuela });
            Diccionario.Add(Llaves_Diccionario.Cursos, Escuela.Cursos);

            var Alumnos_Totales = new List<Alumno>();
            var Asignaturas_Totales = new List<Asignatura>();
            var Evaluaciones_Totales = new List<Evaluación>();

            foreach (var Listai in Escuela.Cursos)
            {
                foreach (var Alumnoi in Listai.Alumnos)
                {
                    Alumnos_Totales.Add(Alumnoi);
                    Evaluaciones_Totales.AddRange(Alumnoi.Evaluaciones);
                }

                Asignaturas_Totales.AddRange(Listai.Asignaturas);
            }

            Diccionario.Add(Llaves_Diccionario.Alumnos, Alumnos_Totales);
            Diccionario.Add(Llaves_Diccionario.Asignaturas, Asignaturas_Totales);
            Diccionario.Add(Llaves_Diccionario.Evaluaciones, Evaluaciones_Totales);

            return Diccionario;
        }
        public void ImprimirDiccionario(Dictionary<Llaves_Diccionario, IEnumerable<ObjetoEscuelaBase>> Dic)
        {
            foreach (var obj in Dic)
            {

                Console.WriteLine(obj.Key.ToString());

                foreach (var val in obj.Value)
                    Console.WriteLine(val);
            }
        }
    }
}