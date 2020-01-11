using System.Collections.Generic;
using CoreEscuela.Entidades;
using platzi_curso_csharp.Entidades;
using System;
using System.Linq;

namespace CoreEscuela.App
{
    //Clase para extraer informacion de nuestra base de datos
    public class Reporteador
    {
        Dictionary<Llaves_Diccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<Llaves_Diccionario, IEnumerable<ObjetoEscuelaBase>> DicObjetoEscuela)
        {
            if (DicObjetoEscuela == null)
            {
                throw new ArgumentNullException(nameof(DicObjetoEscuela));
            }
            else
            {
                _diccionario = DicObjetoEscuela;
            }
        }
        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {
            if (_diccionario.TryGetValue(Llaves_Diccionario.Evaluaciones, out IEnumerable<ObjetoEscuelaBase> listatemp))
            {
                return listatemp.Cast<Evaluación>();
            }
            else
            {
                return new List<Evaluación>();
            }

        }

        public IEnumerable<string> GetListAsignaturas()
        {
            return GetListAsignaturas(0, out var dummy);
        }

        public IEnumerable<string> GetListAsignaturas(float NotaMin, out IEnumerable<Evaluación> ListaEvaluaciones)
        {
            ListaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluación ev in ListaEvaluaciones
                    where ev.Nota >= NotaMin
                    select ev.Asignatura.Nombre).Distinct();
        }

        public IEnumerable<Escuela> GetListaEscuela()
        {
            if (_diccionario.TryGetValue(Llaves_Diccionario.Escuela, out IEnumerable<ObjetoEscuelaBase> listatemp))
            {
                return listatemp.Cast<Escuela>();
            }
            else
            {
                return new List<Escuela>();
            }

        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDicEvaluacionesPorAsignatura()
        {
            Dictionary<string, IEnumerable<Evaluación>> DicRta = new Dictionary<string, IEnumerable<Evaluación>>();
            var ListaAsignaturas = GetListAsignaturas(0f, out var ListaEvaluaciones);

            foreach (var asig in ListaAsignaturas)
            {
                var evalsAsig = from eval in ListaEvaluaciones
                                where eval.Asignatura.Nombre == asig
                                select eval;

                DicRta.Add(asig, evalsAsig);
            }

            return DicRta;
        }
        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromedioAlumnosPorAsignatura()
        {
            var Rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var DicEvaluacionesAsignaturas = GetDicEvaluacionesPorAsignatura();

            foreach (var AsigConEvaluaciones in DicEvaluacionesAsignaturas)
            {
                var PromAlumn = from eval in AsigConEvaluaciones.Value
                                group eval by new
                                {
                                    eval.Alumno.UniqueId,
                                    eval.Alumno.Nombre
                                }
                            into grupoEvalAlumno
                                select new AlumnoPromedio
                                {
                                    alumnoId = grupoEvalAlumno.Key.UniqueId,
                                    alumnoNombre = grupoEvalAlumno.Key.Nombre,
                                    alumnopromedio = grupoEvalAlumno.Average(x => x.Nota)
                                };


                Rta.Add(AsigConEvaluaciones.Key, PromAlumn);
            }

            return Rta;
        }

    }
}