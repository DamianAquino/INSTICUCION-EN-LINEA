using API_Institucion.Entidades;

namespace API_Institucion.Datos
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Constructor opcional para tu lógica de negocio
        public Rol(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = Nombre;
        }

        // Relación inversa: un rol puede tener muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }

}
