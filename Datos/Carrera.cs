namespace API_Institucion.Entidades
{
    public class Carrera
    {
        public int Id;
        public string Nombre;
        public int duracion;
        public string Director;
        private readonly string Comisiones;
        public Carrera(int id, string nombre, string director, string comisiones)
        {
            Id = id;
            Nombre = nombre;
            Director = director;
            Comisiones = comisiones;
        }
    }
}
