namespace TP05.Models;
using Microsoft.Data.SqlClient;
using Dapper;


public class BD
{


private string _connectionString =@"Server=localhost; DataBase = TP05; Integrated Security = True; TrustServerCertificate = True;";


public Usuario ObtenerUsuario(string nombreUsuario)
{
    Usuario miUsuario = null;
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"SELECT * FROM Usuario WHERE nombreUsuario = @nombreUsuario";
        miUsuario = connection.QueryFirstOrDefault<Usuario>(query,new { NombreUsuario = nombreUsuario });
    }
    return miUsuario;


}



public void RegistrarUsuario(Usuario usuario)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"INSERT INTO Usuario (nombreUsuario, contrasenia, nombre, apellido, TipoDeUsuario) VALUES (@nombreUsuario, @contrasenia, @nombre, @apellido, @TipoDeUsuario)";

        connection.Execute(query, new
        {
            nombreUsuario = usuario.NombreUsuario,
            contrasenia = usuario.Contraseña,
            nombre = usuario.Nombre,
            apellido = usuario.Apellido,
            TipoDeUsuario = usuario.TipoDeUsuario
        });
    }
}





public bool ExisteUsuario(string nombreUsuario)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"SELECT * FROM Usuario WHERE nombreUsuario = @nombreUsuario";

        Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { nombreUsuario }
        );

        return usuario != null;
    }
}

}