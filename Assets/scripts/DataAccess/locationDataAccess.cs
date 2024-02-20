using System.Data;
using Location_package;

/**************************************/
/*need implemnts ACCORDING TO DATABSE */
/*AND DELETE THE TESTING IMPLEMENT*/
/*************************************/
public class locationDataAccess
{

    private static locationDataAccess instance;
    private Database database = Database.getInstance();
    private locationDataAccess()
    {

    }
    public static locationDataAccess getInstance()
    {
        if (instance == null)
        {
            instance = new locationDataAccess();
        }
        return instance;
    }
    public string getLocationName(int id)
    {
        string query = "select name from location where id = " + id + ";";
        string name = "";
        IDataReader reader = database.openConnectionAndRunQuery(query);
        if (reader.Read())
        {
            name = reader.GetString(0);
        }
        database.closeConnection(reader);
        return name;
    }

    public string getLocationImage(int id)
    {
        string query = $"select image from location where id = {id};";
        string image = "";
        IDataReader reader = database.openConnectionAndRunQuery(query);
        if (reader.Read())
        {
            image = reader.GetString(0);
        }
        database.closeConnection(reader);
        return image;

    }

    public string getLocationDesc(int id)
    {
        string query = "select desc from location where id = " + id + ";";
        string desc = "";
        IDataReader reader = database.openConnectionAndRunQuery(query);
        if (reader.Read())
        {
            desc = reader.GetString(0);
        }
        database.closeConnection(reader);
        return desc;
    }

    public string getLocationAbility(int id)
    {
        string query = "select ability from location where id = " + id + ";";
        string ability = "";
        IDataReader reader = database.openConnectionAndRunQuery(query);
        if (reader.Read())
        {
            ability = reader.GetString(0);
        }
        database.closeConnection(reader);
        return ability;
    }
    public int numOfLocations()
    {
        string query = $"SELECT COUNT(id) FROM location; ";
        int count = 0;
        IDataReader reader = database.openConnectionAndRunQuery(query);
        if (reader.Read())
        {
            count = reader.GetInt16(0);
        }
        database.closeConnection(reader);
        return count;
    }


}
