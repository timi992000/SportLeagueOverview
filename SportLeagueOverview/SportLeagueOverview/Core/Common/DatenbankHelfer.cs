using Microsoft.Data.Sqlite;
using SportLeagueOverview.Core.Extender;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

    #region [ReadEntity]
    public static List<T> ReadEntity<T>()
    {
      string tmpColumnName = string.Empty;
      try
      {
        var tmpEntity = Activator.CreateInstance<T>();
        var Result = new List<T>();
        __OpenIfNeeded();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = $"SELECT * FROM {tmpEntity.GetType().GetProperty("TableName").GetValue(tmpEntity)}";
        var Reader = Command.ExecuteReader();
        var Properties = typeof(T).GetProperties();
        while (Reader.Read())
        {
          T Entity = Activator.CreateInstance<T>();
          for (int i = 0; i < Reader.FieldCount; i++)
          {
            var ColumnName = Reader.GetName(i);
            tmpColumnName = ColumnName;
            if (Properties.Any(x => x.Name == ColumnName))
            {
              var PropertyInfo = Properties.First(x => x.Name == ColumnName);
              var FieldValue = Reader.GetFieldValue<object>(i);
              if (FieldValue == DBNull.Value)
                continue;
              if (PropertyInfo.PropertyType == typeof(DateTime) && FieldValue.GetType() == typeof(string))
              {
                PropertyInfo.SetValue(Entity, Convert.ToDateTime(FieldValue));
                continue;
              }
              if (PropertyInfo.PropertyType == typeof(bool))
              {
                PropertyInfo.SetValue(Entity, Convert.ToBoolean(FieldValue));
                continue;
              }
              if (FieldValue.GetType() == typeof(long) || FieldValue.GetType() == typeof(int))
                PropertyInfo.SetValue(Entity, Convert.ToInt32(FieldValue));
              else
              {
                if (FieldValue.GetType() == typeof(string) && PropertyInfo.PropertyType == typeof(int))
                {
                  FieldValue = FieldValue.ToString().IsNullOrEmpty() ? 0 : FieldValue;
                  PropertyInfo.SetValue(Entity, Convert.ToInt32(FieldValue));
                  continue;
                }
                else
                  PropertyInfo.SetValue(Entity, FieldValue);
              }
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

    public static void SaveEntity<T>(this T Entity)
    {
      try
      {
        var PrimaryKeyColumn = Entity.GetType().GetProperty("PrimaryKeyColumn").GetValue(Entity);
        string Cmd = string.Empty;
        string Columns = string.Empty;
        string Values = string.Empty;
        if (Convert.ToBoolean(Entity.GetType().GetProperty("IsNew").GetValue(Entity)))
        {

        }
        else
        {
          Columns += "(";
          Values += "VALUES (";
          var TableName = Entity.GetType().GetProperty("TableName").GetValue(Entity);
          var Properties = Entity.GetType().GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.DeclaredOnly);
          int i = 0;
          foreach (var Property in Properties)
          {
            i++;
            var FieldValue = Property.GetValue(Entity);
            if (i > Properties.Length || Property.Name.Equals(PrimaryKeyColumn) || FieldValue.IsNullOrEmpty())
              continue;
            if (Property.PropertyType == typeof(int))
            {
              if (Convert.ToInt32(FieldValue) == 0)
                continue;
              Values += $"{FieldValue}";
            }
            else if (Property.PropertyType == typeof(bool))
            {
              Values += $"{Convert.ToInt32(FieldValue)}";
            }
            else
              Values += $"'{FieldValue}'";
            Columns += Property.Name;
            if (i < Properties.Length)
            {
              Columns += ",";
              Values += ",";
            }
          }
          Columns = __RemoveEndingSeparator(Columns);
          Values = __RemoveEndingSeparator(Values);
          Columns += ")";
          Values += ")";

          Cmd += $"INSERT INTO {TableName} {Columns} {Values};";
          var Result = ExecuteNonQuery(Cmd);
        }
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
      }
    }

    private static string __RemoveEndingSeparator(string CommandText)
    {
      if (CommandText.EndsWith(","))
        CommandText = CommandText.Substring(0, CommandText.Length - 1);
      return CommandText;
    }

    #region [DeleteEntity]
    public static bool DeleteEntity<T>(this T Entity)
    {
      try
      {
        var TableName = Entity.GetType().GetProperty("TableName").GetValue(Entity);
        var PrimaryKeyColumn = Entity.GetType().GetProperty("PrimaryKeyColumn").GetValue(Entity);
        var PrimaryKeyValue = Entity.GetType().GetProperty(PrimaryKeyColumn.ToString()).GetValue(Entity);
        if (PrimaryKeyColumn.IsNullOrEmpty() || PrimaryKeyValue.IsNullOrEmpty())
          return false;
        __OpenIfNeeded();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = $"DELETE FROM {TableName} WHERE {PrimaryKeyColumn} = '{PrimaryKeyValue}'";
        return !Convert.ToBoolean(Convert.ToInt16(Command.ExecuteScalar()));
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

    #region [ExecuteReader]
    public static DbDataReader ExecuteReader(string CommandText)
    {
      try
      {
        __OpenIfNeeded();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = CommandText;
        return Command.ExecuteReader();
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
        return null;
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
