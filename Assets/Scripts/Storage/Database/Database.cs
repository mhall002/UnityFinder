//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Mono.Data.SqliteClient;
using UnityEngine;
namespace AssemblyCSharp
{
		public class Database
		{
		    public const string connectionString = "URI=file:C:/Users/Gk/Documents/GitHub/UnityFinder/SQLite/UnityFinder";
				public Database ()
				{
                    
                    SqliteConnection dbCon = new SqliteConnection(connectionString);

                    dbCon.Open();
                    SqliteCommand dbCmd;
                    string sql = "SELECT * from Terrain";
                    
                    dbCmd = new SqliteCommand(sql, dbCon);
                    SqliteDataReader reader = dbCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string a = reader.GetValue(0).ToString();
                        string b = reader.GetValue(1).ToString();
                    }

                    reader.Close();
                    reader = null;
				}

                public static SqliteConnection getDBConnection()
                {
                    return new SqliteConnection(connectionString);
                }

                public static void CloseDBConnection(SqliteConnection dbConnection)
                {
                    dbConnection.Close();
                    dbConnection = null;
                }

                public static SqliteDataReader runSelectQuery(string sqlText)
                {
                    return runSelectQuery(getDBConnection(), sqlText);

                }

                public static SqliteDataReader runSelectQuery(SqliteConnection connection, SqliteCommand command)
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    return reader;
                }

                public static SqliteDataReader runSelectQuery(SqliteConnection connection, string sqlText)
                {
                    if (sqlText != "")
                    {
                        connection.Open();
                        var command = new SqliteCommand(sqlText, connection);
                        var reader = command.ExecuteReader();

                        return reader;
                    }
                    return null;
                }
                public static int runModifyQuery(string sqlText)
                {
                    var connection = getDBConnection();
                    var command = new SqliteCommand(sqlText,connection);
                    return runModifyQuery(connection, command);
                }
                public static int runModifyQuery(SqliteConnection connection, SqliteCommand command)
                {
                    connection.Open();
                    int modifiedRows = 0;
                    modifiedRows = command.ExecuteNonQuery();


                    return modifiedRows;
                }
        }
}

