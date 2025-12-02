namespace API_Institucion.Entidades
{
    public class Institucion
    {
        public string CUIT { get; private set; }
        public string Nombre { get; private set; }
        public string Direccion { get; private set; }
        private readonly List<Carrera> Carreras;

        public Institucion(string cuit, string nombre, string direccion, List<Carrera> carreras)
        {
            CUIT = cuit;
            Nombre = nombre;
            Direccion = direccion;
            Carreras = carreras;
        }
    }
}
