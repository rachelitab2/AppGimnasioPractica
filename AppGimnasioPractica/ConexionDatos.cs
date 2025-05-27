using System;
using Microsoft.Data.SqlClient;

namespace AppGimnasioPractica
{
  public class Membresia // clase Principal o clase Padre, para poder heredar en otras clases hijas
  {
        public int Id;
        public string Tipo;
        public DateTime FechaInicio;
        public DateTime FechaFin;
        public decimal CostoTotal;
    }

    // creamos la clase hija para heredar

    public class Mensual : Membresia
    {
        public Mensual()
        {
            this.Tipo = "Mensual";
        }
    }

    public class Anual : Membresia
    {
        public Anual()
        {
            this.Tipo = "Anual";

        }
    }

    public class MembresiaDatos // vamos a conectar operaciones 
    {
        public string conexion = "Server =.; Database=Gimnasio;Integrated Segurity=true";

        //Creamos un metodo para obtener membresia por id
        public Membresia ObtenerMembresiaPorId(int IdBusqueda)
        {

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                //Consulta SQL para buscar por Id
                string query = "SELECT * FROM Membresia WHERE Id = @Id";

                //Comando SQL
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", IdBusqueda);
                //Agregar parametro de busqueda
                
                //Ejecutando la consulta 
                SqlDataReader reader = cmd.ExecuteReader();


                //verificar si hay resultado
                if (reader.Read())
                {

                    //Se determina el tipo de Membresia 
                    string tipo = reader["Tipo"].ToString();
                    Membresia membresia;

                    if (tipo == " Mensual")
                        membresia = new Mensual();
                    else
                        membresia = new Anual();


                    //Se asignan valores desde las base
                    //de datos a los atributos de la clase

                    membresia.Id = (int)reader["Id"];
                    membresia.Tipo = tipo;
                    membresia.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                    membresia.FechaFin = Convert.ToDateTime(reader["FechaFin"]);
                    membresia.CostoTotal = Convert.ToDecimal(reader["CostoTotal"]);
                    return membresia; // Devulve la membresias que  encontra

                }

                return null; // Si no se encuentra, devuelve null



            }
        }

        public void InsertarMembresia(Membresia m)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();
                string query = "INSERT INTO Membresia ( Tipo, FechaIncio, FechaFin, CostoTotal) VALUES (@Tipo, @Inicio, @Fin, @Costo)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(@"Tipo", m.Id);
                cmd.Parameters.AddWithValue("@Inicio", m.FechaInicio);
                cmd.Parameters.AddWithValue("@Fin", m.FechaFin);
                cmd.Parameters.AddWithValue("@Costo", m.CostoTotal);

                cmd.ExecuteNonQuery();

            }
        }
    }

}
