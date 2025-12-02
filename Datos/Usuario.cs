using API_Institucion.Datos;

namespace API_Institucion.Entidades
{
    public class Usuario
    {
        public int id { get; set; }
        public string dni { get; set; }
        public string email { get; set; }
        public string carrera { get; set; }
        public string password { get; set; }
        // Clave foránea
        public int rolId { get; set; }
        // Propiedad de navegación
        public Rol Rol { get; set; }

        public Usuario(string dni, string email, string carrera, string password, int rolId)
        {
            this.dni = dni;
            this.email = email;
            this.carrera = carrera;
            this.password = password;
            this.rolId = rolId;
        }
    }
}
