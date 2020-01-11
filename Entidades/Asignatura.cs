using System;

namespace CoreEscuela.Entidades
{
    public class Asignatura : ObjetoEscuelaBase
    {
        public override bool Equals(object obj)
        {
            if (obj is Asignatura)
            {
                Asignatura temp = (Asignatura)obj;
                if (Nombre == temp.Nombre) return true;
            }
            return false;

        }

    }

}