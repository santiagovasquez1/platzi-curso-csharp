namespace CoreEscuela.Entidades
{
    //Definicion de una estructura para un objeto
    public interface ILugar
    {
        string Direccion { get; set; }

        void LimpiarLugar();
    }
}