﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DBConnect
{
    // Start is called before the first frame update
    private const string fileName = "db.bytes";
    private static string DBPath;
    private static SqliteConnection connection;
    private static SqliteCommand command;


    static DBConnect()
    {
        DBPath = GetDatabasePath();
    }

    private static string GetDatabasePath()
    {
        string filePath = Path.Combine(Application.dataPath, fileName);
        if(!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
    }

    private static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.dataPath, fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }

    // private static void OpenConnection()
    // {
    //     connection = new SqliteConnection("Data Source=" + DBPath);
    //     command = new SqliteCommand(connection);
    //     connection.Open();
    // }

    public static void CloseConnection()
    {
        connection.Close();
        command.Dispose();
    }

    /// <summary> Этот метод выполняет запрос query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    public static void ExecuteQueryWithoutAnswer(string query)
    {
     //   OpenConnection();
        command.CommandText = query;
        command.ExecuteNonQuery();
        CloseConnection();
    }


    public static string ExecuteQueryWithAnswer(string query)
    {
        //OpenConnection();
        command.CommandText = query;
        var answer = command.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    public static DataTable GetTable(string query)
    {
        //OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

        DataSet DS = new DataSet();
        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();

        return DS.Tables[0];
    }


}


