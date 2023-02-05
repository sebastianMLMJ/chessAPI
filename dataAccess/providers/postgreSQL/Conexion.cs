using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using chessAPI.models.player;

namespace chessAPI.dataAccess.providers.postgreSQL
{
    public class Conexion
    {
        NpgsqlConnection conexion = new NpgsqlConnection();

        public NpgsqlConnection conect()
        {
            try
            {
                conexion.ConnectionString = "Server=localhost;Port=5432;Database=chessDB;User Id=postgres;Password=misDatos2023!;Pooling=true;MinPoolSize=3;MaxPoolSize=20;Max Auto Prepare=15;Enlist=false;Auto Prepare Min Usages=3";
                conexion.Open();
                Console.WriteLine("Conexion exitosa");
            }
            catch (NpgsqlException e)
            {

                throw(e);
            }
            return conexion;
        }

        public async void ExecuteNonQuery(string query)
        {
            conect();
            await using var comando = new NpgsqlCommand(query, conexion);
            await comando.ExecuteNonQueryAsync();
            conexion.Close();

        }

        public async Task<List<clsNewPlayer>>ReadPlayers (string query)
        {
            conect();
            await using var command = new NpgsqlCommand(query, conexion);
            await using var reader = await command.ExecuteReaderAsync();
            List<clsNewPlayer> querylist = new List<clsNewPlayer>();
            while(await reader.ReadAsync())
            {
                clsNewPlayer pl = ReadPlayer(reader);
                querylist.Add(pl);    
            }

            conexion.Close();
            return querylist;
        }

        public async Task<List<clsGame>>ReadGames (string query)
        {
            conect();
            await using var command = new NpgsqlCommand(query, conexion);
            await using var reader = await command.ExecuteReaderAsync();
            List<clsGame> querylist = new List<clsGame>();
            while(await reader.ReadAsync())
            {
                clsGame pl = ReadGames(reader);
                querylist.Add(pl);    
            }
            conexion.Close();
            return querylist;
        }

        private static clsNewPlayer ReadPlayer(NpgsqlDataReader reader)
        {
            //int? _id = reader["id"] as int?;
            string _email = reader["email"] as string;

            clsNewPlayer readedplayer = new clsNewPlayer
            {
                //id = _id,
                email = _email
            };

            return readedplayer;

        }

        private static clsGame ReadGames(NpgsqlDataReader reader)
        {
            int? _id = reader["id"] as int?;
            DateTime? _started = reader["started"] as DateTime?;
            int? _whites = reader["whites"] as int?;
            int? _blacks = reader["blacks"] as int?;
            bool? _turn = reader["turn"] as bool?;
            int? _winner = reader["winner"] as int?;

            clsGame game = new clsGame
            {

                id = _id,
                started = _started,
                whites = _whites,
                blacks = _blacks,
                turn = _turn,
                winner = _winner
            };

            return game;

        }
    }
}