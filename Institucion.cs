public class Institucion
{
    public string Nombre { get; private set; }
    public string CUIT { get; private set; }
    public string Direccion { get; private set; }
    private List<string> carreras;

    public Institucion(string Cuit, string nombre, string direccion)
    {
        CUIT = Cuit;
        Nombre = nombre;
        Direccion = direccion;
        carreras = new List<string>();
    }
}
