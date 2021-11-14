using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace SportLeagueOverview.Core.Common
{
  public static class DatenbankHelfer
  {
    private static SqliteConnection m_Connection;

    #region [Ctor]
    static DatenbankHelfer()
    {
      try
      {
        m_Connection = new SqliteConnection($"Data Source={Directory.GetCurrentDirectory()}\\FussballLigenÜbersicht.db");
        m_Connection.Open();
        __CreateTableSchema();
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
      }
    }
    #endregion

    #region [ExecuteReader]
    public static List<T> ExecuteReader<T>(string CommandText) where T : EntityBase
    {
      try
      {
        var Result = new List<T>();
        __OpenIfNeeded();
        var AllValues = new List<Dictionary<string, object>>();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = CommandText;
        var Reader = Command.ExecuteReader();
        var Properties = typeof(T).GetProperties();
        while (Reader.Read())
        {
          T Entity = Activator.CreateInstance<T>();
          for (int i = 0; i < Reader.FieldCount; i++)
          {
            var ColumnName = Reader.GetName(i);
            if (Properties.Any(x => x.Name == ColumnName))
            {
              var PropertyInfo = Properties.First(x => x.Name == ColumnName);
              var FieldValue = Reader.GetFieldValue<object>(i);
              if (FieldValue == DBNull.Value)
                continue;
              if (FieldValue.GetType() == typeof(long))
                PropertyInfo.SetValue(Entity, Convert.ToInt32(FieldValue));
              else
                PropertyInfo.SetValue(Entity, FieldValue);
            }
          }
          Result.Add(Entity);
        }
        return Result;
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
        return new List<T>();
      }
      finally
      {
        m_Connection.Close();
      }
    }
    #endregion

    #region [ExecuteNonQuery]
    public static bool ExecuteNonQuery(string CommandText)
    {
      try
      {
        __OpenIfNeeded();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = CommandText;
        return Command.ExecuteNonQuery() == 1;
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
        return false;
      }
      finally
      {
        m_Connection.Close();
      }
    }
    #endregion

    #region [ExecuteScalar]
    public static object ExecuteScalar(string CommandText)
    {
      try
      {
        __OpenIfNeeded();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = CommandText;
        return Command.ExecuteScalar();
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
        return false;
      }
      finally
      {
        m_Connection.Close();
      }
    }
    #endregion

    #region [Initialize]
    public static void Initialize()
    {
      //MethodMustStayHere
    }
    #endregion

    #region [__OpenIfNeeded]
    private static void __OpenIfNeeded()
    {
      if (m_Connection.State == System.Data.ConnectionState.Closed)
        m_Connection.Open();
    }
    #endregion

    #region [__CreateTableSchema]
    private static void __CreateTableSchema()
    {
      var Cmd = "CREATE TABLE IF NOT EXISTS \"Adresse\" (	\"AdressId\"	INTEGER NOT NULL UNIQUE,	\"Straße\"	TEXT NOT NULL,	\"AdressZusatz\"	" +
        "TEXT NOT NULL,	\"PLZ\"	INTEGER NOT NULL,	\"Stadt\"	TEXT NOT NULL,	PRIMARY KEY(\"AdressId\" AUTOINCREMENT));";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Ereignis\" ( \"EreignisId\"  INTEGER NOT NULL UNIQUE, \"MannId\"  INTEGER NOT NULL," +
        "\"EreignisTyp\" INTEGER NOT NULL,\"Minute\"  INTEGER,	\"SpielerId\" INTEGER,	FOREIGN KEY(\"MannId\") REFERENCES \"Mannschaft\"" +
        "(\"MannschaftId\"),	FOREIGN KEY(\"SpielerId\") REFERENCES \"Person\"(\"PersonId\"),	PRIMARY KEY(\"EreignisId\" AUTOINCREMENT)); ";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Mannschaft\" (  \"MannschaftId\"  INTEGER NOT NULL UNIQUE,  \"Name\"  TEXT NOT NULL,	\"Gruendungsjahr\"  INTEGER," +
        "	\"Wappen\"  TEXT,	\"TrainerId\" INTEGER,	FOREIGN KEY(\"TrainerId\") REFERENCES \"Person\"(\"PersonId\"),	PRIMARY KEY(\"MannschaftId\" AUTOINCREMENT)); ";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Person\" (  \"PersonId\"  INTEGER NOT NULL UNIQUE,  \"Name\"  TEXT,	\"AktuelleMannId\"  INTEGER,	\"Rückennummer\"  INTEGER,	\"IsTrainer\"" +
        " INTEGER NOT NULL,	\"Geburtsdatum\"  TEXT,	\"Bild\"  TEXT,	\"AdressId\"  INTEGER,	\"Eintrittsdatum\"  TEXT,	PRIMARY KEY(\"PersonId\" AUTOINCREMENT),	FOREIGN KEY(\"AktuelleMannId\") REFERENCES \"Mannschaft\"" +
        "(\"MannschaftId\")); ";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Spiel\" ( \"SpielId\" INTEGER NOT NULL UNIQUE, \"Status\"  INTEGER NOT NULL, \"Anpfiff\" TEXT," +
        "	\"HeimMannId\"  INTEGER,\"AuswaertsMannId\" INTEGER,	\"AustrgOrtId\" INTEGER,	PRIMARY KEY(\"SpielId\" AUTOINCREMENT)," +
        "	FOREIGN KEY(\"AuswaertsMannId\") REFERENCES \"Mannschaft\"(\"MannschaftId\"),	FOREIGN KEY(\"HeimMannId\") REFERENCES \"Mannschaft\"(\"MannschaftId\")," +
        "	FOREIGN KEY(\"AustrgOrtId\") REFERENCES \"Adresse\"(\"AdressId\")); ";

      ExecuteNonQuery(Cmd);
    }
    #endregion

    #region [__ThrowMessage]
    private static void __ThrowMessage(string Message)
    {
      MessageBox.Show(Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    #endregion

  }
}
