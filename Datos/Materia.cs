namespace API_Institucion.Datos
{
    public class Materia
    {
        public int Id;
        public string Nombre;
        public int CarreraId;

        public Materia(int id, string nombre, int carreraId)
        {
            Id = id;
            Nombre = nombre;
            CarreraId = carreraId;
        }
    }
}
