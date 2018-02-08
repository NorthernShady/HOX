using System.Linq;
using UnityEngine;
using System.Collections;
using SQLite4Unity3d;
using System.Collections.Generic;
using System;

public partial class DBProvider : I_DBProvider
{
	public static string dbName = "HOX_db";
	private DataService dataService = null;
	protected static DBProvider _instance = null;

	public static T instance <T>() where T: class
	{
		if (_instance == null) {
			DBProvider dbProvider = new DBProvider();
			_instance = dbProvider;
			dbProvider.init ();
		}
		return _instance as T;
	}

	public string getDBName ()
	{
		string DBName = dbName;
		DBName = DBName + "_" + Application.version + ".sqlite";
		return DBName;
	}

	public void clear() 
	{
		_instance = null;
	}

	protected virtual void init ()
	{
		dataService = new DataService (getDBName());
		if (!dataService.tableExists<User>())
		{
			createDB ();
		}
	}

	public DataService getDataService()
	{
		return dataService;
	}

	public virtual void createDB ()
	{
		DataService dataService = new DataService (getDBName());

		dataService.connection.DropTable<User> ();
		dataService.connection.DropTable<Config> ();
		dataService.connection.DropTable<XPLevel> ();


		dataService.connection.CreateTable<User> ();
		dataService.connection.CreateTable<Config> ();
		dataService.connection.CreateTable<XPLevel> ();
	}

	public virtual void backupDB()
	{
		dataService.DBBackup ();
	}

	#region interface methods

	public virtual void restoreDB ()
	{
		dataService.restoreDB ();
	}


	public virtual void beginTransaction ()
	{
		dataService.connection.BeginTransaction ();
	}

	public virtual void commit ()
	{
		dataService.connection.Commit ();
	}

	/// обновляет запись row в базе данных
	public virtual void updateRowValue (object row)
	{
		dataService.connection.Update (row);
	}

	/// добавляет запись row в базе данных
	public virtual void insertRowValue (object row)
	{
		dataService.connection.Insert (row);
	}

	// удаляет запись
	public virtual void removeRow (object row)
	{
		dataService.connection.Delete (row);
	}

	public virtual IEnumerable<T> getQueryResult<T> (string query) where T : class, new()
	{
		string cmdText = query;
		List<T> result = dataService.connection.Query<T> (cmdText);
		return result;
	}

	public virtual int getLastTableId<T>()
	{
		var table = dataService.connection.GetMapping (typeof(T));
		string cmdText = "SELECT Id FROM " +
			table.TableName +
			" ORDER BY Id DESC LIMIT 1";
		var result = dataService.connection.ExecuteScalar<int> (cmdText);
		return result;
	}

	#endregion interface methods
}
