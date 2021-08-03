using GamesCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GamesCatalogAPI.Repositories {
    public class GameRepositorySqlServer:IGameRepository {
        private readonly SqlConnection sqlConnection;

        public GameRepositorySqlServer(IConfiguration configuration) {

            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));           
        }

        public async Task<List<Game>> Get(int page, int quantity) {
            var games = new List<Game>();

            var command = $"SELECT * FROM games ORDER BY id OFFSET {((page - 1) * quantity)} ROWS FETCH NEXT {quantity} ROWS ONLY";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read()) {
                games.Add(new Game {
                    Id = (Guid)sqlDataReader["id"],
                    Name = (string)sqlDataReader["name"],
                    Producer = (string)sqlDataReader["producer"],
                    Price = (double)sqlDataReader["price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> Get(Guid id) {
            Game game = null;
            
            var command = $"SELECT * FROM games WHERE id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read()) {
                game = new Game {
                    Id = (Guid)sqlDataReader["id"],
                    Name = (string)sqlDataReader["name"],
                    Producer = (string)sqlDataReader["producer"],
                    Price = (double)sqlDataReader["price"]
                };
            }
            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Get(string name, string producer) {
            var games = new List<Game>();

            var command = $"SELECT * FROM jogos WHERE name = '{name}' AND producer = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read()) {
                games.Add(new Game {
                    Id = (Guid)sqlDataReader["id"],
                    Name = (string)sqlDataReader["name"],
                    Producer = (string)sqlDataReader["producer"],
                    Price = (double)sqlDataReader["price"]
                });
            }
            await sqlConnection.CloseAsync();
            
            return games;
        }

        public async Task Insert(Game game) {
            var command = $"INSERT games(id, name, producer, price) VALUES('{game.Id}','{game.Name}','{game.Producer}','{game.Price.ToString().Replace(",",".")} WHERE id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game) {
            var command = $"UPDATE games SET name = '{game.Name}', producer = ' {game.Price}', price = '{game.Price.ToString()}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remove(Guid idGame) {
            var command = $"DELETE FROM games WHERE id = '{idGame}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose() {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
