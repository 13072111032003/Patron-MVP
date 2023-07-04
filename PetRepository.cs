using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using PatrónMVP.Models;

namespace PatrónMVP._Repositories
{
    public class PetRepository : BaseRepository, IPetRepository
    {
        //Constructor
        public PetRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Metodos
        public void Añadir(PetModel petModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Pet values (@name, @type, @colour)";
                command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = petModel.Nombre;
                command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = petModel.Tipo;
                command.Parameters.Add("@color", SqlDbType.NVarChar).Value = petModel.Color;
                command.ExecuteNonQuery();
            }
        }
        public void Borrar(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "eliminar de Mascota donde Pet_Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;           
                command.ExecuteNonQuery();
            }
        }
        public void Editar(PetModel petModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"actualizar mascota
                                        establecer Pet_Name=@nombre,Pet_Type= @tipo,Pet_Colour= @color
                                        donde Pet_Id=@id";
                command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = petModel.Nombre;
                command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = petModel.Tipo;
                command.Parameters.Add("@color", SqlDbType.NVarChar).Value = petModel.Color;
                command.Parameters.Add("@id", SqlDbType.Int).Value = petModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PetModel> GetAll()
        {
            var petList = new List<PetModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Selecciona *de Pet order by Pet_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Nombre = reader[1].ToString();
                        petModel.Tipo = reader[2].ToString();
                        petModel.Color = reader[3].ToString();
                        petList.Add(petModel);
                    }
                }
            }
            return petList;
        }

        public IEnumerable<PetModel> GetByValue(string value)
        {
            var petList = new List<PetModel>();
            int petId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string petName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Selecciona *de Mascota
                                        donde Pet_Id=@id o Pet_Name como @name+'%'
                                        ordenar por Pet_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = petId;
                command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = petName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var petModel = new PetModel();
                        petModel.Id = (int)reader[0];
                        petModel.Nombre = reader[1].ToString();
                        petModel.Tipo = reader[2].ToString();
                        petModel.Color = reader[3].ToString();
                        petList.Add(petModel);
                    }
                }
            }
            return petList;
        }
    }
}
