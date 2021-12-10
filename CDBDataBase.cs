using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGZ_POGI3_
{
    public struct SPlayer
    {
        public string name;
        public int score;
        public string time;
    }

    public class CDBDataBase : CDataBase
    {
        List<SPlayer> players;

        public CDBDataBase()
        {
            players = new List<SPlayer>();
        }

        public void addPlayer(SPlayer player)
        {
            players.Add(player);
        }

        public List<SPlayer> GetPlayers()
        {
            return players;
        }
    }

    public class CDBConnector : CConnector
    {
        string sql;

        public CDBConnector(string path) : base(path) { }

        public CDBConnector() : base() { }

        public override CDataBase LoadData()
        {
            CDBDataBase DataBase = new CDBDataBase();

            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection(/*"DataSource=" + */this.GetPath()/* + ";Version=3;"*/);
            m_dbConnection.Open();

            sql = "SELECT * FROM test_table;";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SPlayer player = new SPlayer();

                player.name = reader["name"].ToString();
                player.score = int.Parse(reader["score"].ToString());
                player.time = reader["time"].ToString();

                DataBase.addPlayer(player);
            }

            m_dbConnection.Close();

            return DataBase;
        }

        public override bool SaveData(CDataBase db)
        {
            CDBDataBase DataBase = (CDBDataBase)db;

            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection(/*"DataSource=" + */this.GetPath()/* + ";Version=3;"*/);

            try
            {
                m_dbConnection.Open();

                List<SPlayer> list = DataBase.GetPlayers();

                foreach (SPlayer player in list)
                {
                    sql = "INSERT INTO test_table (name, score, time) VALUES('" + player.name + "'," + player.score + ",'" + player.time + "');";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader reader = command.ExecuteReader();
                }
            }
            catch
            {
                return false;
            }

            m_dbConnection.Close();

            return true;
        }
    }
}
