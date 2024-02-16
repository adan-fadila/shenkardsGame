using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

/*************************************************************/
/*DATABASE CONNECTION PROBLEM - NEED FIX OR CHANGE DATABASE TYPE*/
public class Database
{

    private static Database instance;
    string conn;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    IDataReader reader;
    private Database() { }
    public static Database getInstance()
    {
        if (instance == null)
        {
            instance = new Database();
            return instance;
        }
        return instance;
    }
    public IDataReader openConnectionAndRunQuery(string query)
    {
        conn = "URI=file:" + Application.dataPath + "/DB/shenkard.sqlite";
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = query;

        reader = dbcmd.ExecuteReader();

        return reader;

    }
    
    public void closeConnection(IDataReader reader)
    {
        if (reader != null)
        {
            reader.Close();
        }
        if (dbcmd != null)
        {
            dbcmd.Dispose();
        }
        if (dbconn != null && dbconn.State != ConnectionState.Closed)
        {
            dbconn.Close();
            dbconn.Dispose();
        }

    }
}