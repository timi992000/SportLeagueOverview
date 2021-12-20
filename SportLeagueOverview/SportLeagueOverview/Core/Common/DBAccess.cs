using Microsoft.Data.Sqlite;
using SportLeagueOverview.Core.Attributes;
using SportLeagueOverview.Core.Entitites;
using SportLeagueOverview.Core.Extender;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Windows;

namespace SportLeagueOverview.Core.Common
{

  public static class DBAccess
  {
    private static SqliteConnection m_Connection;
    private static List<AdressEntity> m_Adresses;

    #region [Ctor]
    static DBAccess()
    {
      try
      {
        m_Connection = new SqliteConnection($"Data Source={Directory.GetCurrentDirectory()}\\FussballLigenÜbersicht.db");
        m_Connection.Open();
        __CreateTableSchema();
        m_Adresses = ReadEntity<AdressEntity>();
        EntitySpectator.SaveCompleted += (sender, e) =>
        {
          m_Adresses = ReadEntity<AdressEntity>();
        };
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
      }
    }
    #endregion

    #region [ReadEntity]
    public static List<T> ReadEntity<T>(List<int> Ids = null, bool IsSubCall = false)
    {
      string tmpColumnName = string.Empty;
      try
      {
        var tmpEntity = Activator.CreateInstance<T>();
        var PrimaryKeyColumn = tmpEntity.GetPrimaryKeyColumn();
        var Result = new List<T>();
        __OpenIfNeeded();
        var Command = m_Connection.CreateCommand();
        Command.CommandText = $"SELECT * FROM {tmpEntity.GetType().GetProperty("TableName").GetValue(tmpEntity)}";
        if (Ids != null && Ids.Any())
          Command.CommandText += $" WHERE {PrimaryKeyColumn} IN ({string.Join(",", Ids)})";
        var Reader = Command.ExecuteReader();
        var Properties = typeof(T).GetProperties();
        var Columns = new Dictionary<string, ColumnNameAttribute>();
        Properties.ToList().ForEach(x => Columns.Add(x.Name, (ColumnNameAttribute)Attribute.GetCustomAttribute(x, typeof(ColumnNameAttribute), true)));
        Columns.Where(x => x.Value == null).Select(y => y.Key).ToList().ForEach(x => Columns.Remove(x));
        while (Reader.Read())
        {
          T Entity = Activator.CreateInstance<T>();
          for (int i = 0; i < Reader.FieldCount; i++)
          {
            var ColumnName = Reader.GetName(i);
            tmpColumnName = ColumnName;
            if (Columns.Any(x => x.Value.ColumnName == ColumnName))
            {
              var ColumnProperty = Columns.FirstOrDefault(x => x.Value.ColumnName.Equals(ColumnName));
              if (ColumnProperty.Key == null)
                continue;
              var PropertyName = ColumnProperty.Key;
              var PropertyInfo = Properties.First(x => x.Name == PropertyName);
              var FieldValue = Reader.GetFieldValue<object>(i);
              if (FieldValue == DBNull.Value)
                continue;
              if (PropertyInfo.PropertyType == typeof(AdressEntity))
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
        if (m_Adresses != null)
          foreach (var Entity in Result)
          {
            var EntityType = Entity.GetType();
            var AdressProperty = EntityType.GetProperty("AdressId");
            if (AdressProperty != null)
            {
              var AdressId = AdressProperty.GetValue(Entity);
              if (AdressId != null && m_Adresses != null)
              {
                var MatchingAdress = m_Adresses.FirstOrDefault(x => x.AdressId == Convert.ToInt32(AdressId));
                if (MatchingAdress != null)
                  EntityType.GetProperty(nameof(EntityBase.Adress)).SetValue(Entity, MatchingAdress);
              }
            }
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
        if(!IsSubCall)
          m_Connection.Close();
      }
    }
    #endregion

    public static void SaveEntity<T>(this T Entity)
    {
      try
      {
        if (Entity == null)
          return;
        var PrimaryKeyColumn = Entity.GetPrimaryKeyColumn();
        string Cmd = string.Empty;
        string Columns = string.Empty;
        string Values = string.Empty;
        if (!Convert.ToBoolean(Entity.GetType().GetProperty("IsNew").GetValue(Entity)))
        {
          Entity.UpdateEntity();
        }
        else
        {
          Columns += "(";
          Values += "VALUES (";
          var TableName = Entity.GetType().GetProperty("TableName").GetValue(Entity);
          var Properties = Entity.GetType().GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);
          int i = 0;
          foreach (var Property in Properties)
          {
            i++;
            var IsDataColAttribute = (IsDataColumnAttribute)Attribute.GetCustomAttribute(Property, typeof(IsDataColumnAttribute), true);
            if (IsDataColAttribute != null && !IsDataColAttribute.IsDataColumn)
              continue;
            if (Property.PropertyType == typeof(AdressEntity))
              continue;
            var ColumnName = __GetColumnNameForProperty(Property);
            var FieldValue = Property.GetValue(Entity);
            if (i > Properties.Length || ColumnName.Equals(PrimaryKeyColumn) || FieldValue.IsNullOrEmpty())
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
            Columns += ColumnName;
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
          if (Columns.Equals("()") || Values.Equals("()"))
            return;
          var Result = ExecuteNonQuery(Cmd);
        }
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
      }
    }

    public static object GetPrimaryKeyColumn<T>(this T Entity)
    {
      return Entity.GetType().GetProperty(nameof(EntityBase.PrimaryKeyColumn)).GetValue(Entity);
    }

    public static object GetTableName<T>(this T Entity)
    {
      return Entity.GetType().GetProperty(nameof(EntityBase.TableName)).GetValue(Entity);
    }

    public static void UpdateEntity<T>(this T Entity)
    {
      try
      {
        var PrimaryKeyColumn = Entity.GetPrimaryKeyColumn().ToString();
        int PrimaryKeyValue = 0;

        PrimaryKeyValue = __GetPrimaryKeyValue(Entity, PrimaryKeyColumn, PrimaryKeyValue);

        var TableName = Entity.GetType().GetProperty("TableName").GetValue(Entity);
        var Cmd = string.Empty;
        var PropertyCmds = new List<string>();
        var Properties = Entity.GetType().GetProperties(System.Reflection.BindingFlags.Public |
              System.Reflection.BindingFlags.Instance |
              System.Reflection.BindingFlags.DeclaredOnly);
        int i = 0;
        foreach (var Property in Properties)
        {
          i++;
          var IsDataColAttribute = (IsDataColumnAttribute)Attribute.GetCustomAttribute(Property, typeof(IsDataColumnAttribute), true);
          if (IsDataColAttribute != null && !IsDataColAttribute.IsDataColumn)
            continue;
          if (Property.PropertyType == typeof(AdressEntity))
            continue;
          var ColumnName = __GetColumnNameForProperty(Property);
          var FieldValue = Property.GetValue(Entity);
          if (i > Properties.Length || ColumnName.Equals(PrimaryKeyColumn) || FieldValue.IsNullOrEmpty())
            continue;
          if (Property.PropertyType == typeof(int))
          {
            if (Convert.ToInt32(FieldValue) == 0)
              continue;
            PropertyCmds.Add($"{ColumnName} = {FieldValue}");
          }
          else if (Property.PropertyType == typeof(bool))
          {
            PropertyCmds.Add($"{ColumnName} = {Convert.ToInt32(FieldValue)}");
          }
          else
            PropertyCmds.Add($"{ColumnName} = '{FieldValue}'");
        }
        Cmd = $"UPDATE {TableName} SET {string.Join(",", PropertyCmds)} WHERE {PrimaryKeyColumn} = {PrimaryKeyValue}";
        var Result = ExecuteNonQuery(Cmd);
      }
      catch (Exception ex)
      {
        __ThrowMessage(ex.ToString());
      }
    }

    private static int __GetPrimaryKeyValue<T>(T Entity, string PrimaryKeyColumn, int PrimaryKeyValue)
    {
      foreach (var PropertyInfo in Entity.GetType().GetProperties())
      {
        var ColNameAttribute = (ColumnNameAttribute)Attribute.GetCustomAttributes(PropertyInfo, typeof(ColumnNameAttribute), true).FirstOrDefault();
        if (ColNameAttribute == null)
          continue;
        if (ColNameAttribute.ColumnName.Equals(PrimaryKeyColumn))
        {
          PrimaryKeyValue = Convert.ToInt32(PropertyInfo.GetValue(Entity));
          break;
        }
      }
      return PrimaryKeyValue;
    }

    private static string __GetColumnNameForProperty(System.Reflection.PropertyInfo property)
    {
      var Attributes = Attribute.GetCustomAttributes(property, typeof(ColumnNameAttribute), true);
      if (Attributes.Length == 0)
        return string.Empty;
      var tmpColumnNameAttribute = Attributes[0];
      string ColumnName = string.Empty;
      if (tmpColumnNameAttribute != null && tmpColumnNameAttribute is ColumnNameAttribute ColNameAtr)
        ColumnName = ColNameAtr.ColumnName;
      return ColumnName;
    }

    private static string __RemoveEndingSeparator(string CommandText, bool FirstOnly = false)
    {
      CommandText = CommandText.Trim();
      if (CommandText.EndsWith(","))
        CommandText = CommandText.Substring(0, CommandText.Length - 1);
      CommandText = CommandText.Trim();
      if (FirstOnly)
        return CommandText;
      if (CommandText.EndsWith(","))
        CommandText = __RemoveEndingSeparator(CommandText);
      return CommandText;
    }

    #region [DeleteEntity]
    public static bool DeleteEntity<T>(this T Entity)
    {
      try
      {
        var TableName = Entity.GetType().GetProperty("TableName").GetValue(Entity);
        var PrimaryKeyColumn = Entity.GetType().GetProperty("PrimaryKeyColumn").GetValue(Entity).ToString();
        int PrimaryKeyValue = 0;

        PrimaryKeyValue = __GetPrimaryKeyValue(Entity, PrimaryKeyColumn, PrimaryKeyValue);

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
      var Cmd = "CREATE TABLE IF NOT EXISTS \"Adresse\" (	\"AdressId\"	INTEGER NOT NULL UNIQUE,	\"Straße\"	TEXT NOT NULL, \"Hausnummer\"	INTEGER NOT NULL,	\"AdressZusatz\"	" +
        "TEXT NOT NULL,	\"PLZ\"	INTEGER NOT NULL,	\"Stadt\"	TEXT NOT NULL,	PRIMARY KEY(\"AdressId\" AUTOINCREMENT));";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Ereignis\" ( \"EreignisId\"  INTEGER NOT NULL UNIQUE, \"SpielId\"  INTEGER NOT NULL, \"MannId\"  INTEGER NOT NULL," +
        "\"EreignisTyp\" INTEGER NOT NULL,\"Minute\"  INTEGER,	\"SpielerId\" INTEGER,	FOREIGN KEY(\"MannId\") REFERENCES \"Mannschaft\"" +
        "(\"MannschaftId\"),	FOREIGN KEY(\"SpielerId\") REFERENCES \"Person\"(\"PersonId\"),	PRIMARY KEY(\"EreignisId\" AUTOINCREMENT)); ";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Mannschaft\" (  \"MannschaftId\"  INTEGER NOT NULL UNIQUE,  \"Name\"  TEXT NOT NULL, \"AdressId\"  INTEGER,	\"Gruendungsjahr\"  INTEGER," +
        "	\"Wappen\"  BLOB,	\"TrainerId\" INTEGER,	FOREIGN KEY(\"TrainerId\") REFERENCES \"Person\"(\"PersonId\"),	PRIMARY KEY(\"MannschaftId\" AUTOINCREMENT), FOREIGN KEY(\"AdressId\") REFERENCES \"Adresse\"(\"AdressId\")); ";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Person\" (  \"PersonId\"  INTEGER NOT NULL UNIQUE,  \"Name\"  TEXT,	\"AktuelleMannId\"  INTEGER,	\"Rückennummer\"  INTEGER,	\"IsTrainer\"" +
        " INTEGER NOT NULL,	\"Geburtsdatum\"  TEXT,	\"Bild\"  BLOB,	\"AdressId\"  INTEGER,	\"Eintrittsdatum\"  TEXT,	PRIMARY KEY(\"PersonId\" AUTOINCREMENT), FOREIGN KEY(\"AdressId\") REFERENCES \"Adresse\"(\"AdressId\")," +
        "	FOREIGN KEY(\"AktuelleMannId\") REFERENCES \"Mannschaft\"(\"MannschaftId\")); ";

      Cmd += "CREATE TABLE IF NOT EXISTS \"Spiel\" ( \"SpielId\" INTEGER NOT NULL UNIQUE, \"Status\"  INTEGER NOT NULL, \"Anpfiff\" TEXT," +
        "	\"HeimMannId\"  INTEGER,\"AuswaertsMannId\" INTEGER,	\"AdressId\" INTEGER,	PRIMARY KEY(\"SpielId\" AUTOINCREMENT)," +
        "	FOREIGN KEY(\"AuswaertsMannId\") REFERENCES \"Mannschaft\"(\"MannschaftId\"),	FOREIGN KEY(\"HeimMannId\") REFERENCES \"Mannschaft\"(\"MannschaftId\")," +
        "	FOREIGN KEY(\"AdressId\") REFERENCES \"Adresse\"(\"AdressId\")); ";

      ExecuteNonQuery(Cmd);
    }
    #endregion

    public static bool CheckTrainerIsUsed(int TrainerId)
    {
      var Cmd = $"SELECT Count(*) FROM Mannschaft WHERE TrainerId = {TrainerId}";
      return Convert.ToBoolean(ExecuteScalar(Cmd));
    }

    public static int GetHighestId<T>(this T Entity)
    {
      var Cmd = $"SELECT COALESCE((SELECT MAX ({Entity.GetPrimaryKeyColumn()}) FROM {Entity.GetTableName()}), 0)";
      return Convert.ToInt32(ExecuteScalar(Cmd));
    }

    public static int CountEntities<T>(this T Entity)
    {
      var Cmd = $"SELECT COUNT (*) FROM {Entity.GetTableName()}";
      return Convert.ToInt32(ExecuteScalar(Cmd));
    }

    #region [__ThrowMessage]
    private static void __ThrowMessage(string Message)
    {
      MessageBox.Show(Message, "Fehlgeschlagen", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    #endregion

  }
}
