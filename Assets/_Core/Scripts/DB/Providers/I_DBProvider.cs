using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface I_DBProvider
{

	void createDB ();
    void restoreDB();
    void beginTransaction();

	void commit();

	/// обновляет запись row в базе данных
	void updateRowValue(object row);

	/// добавляет запись row в базе данных
	void insertRowValue(object row);

	// удаляет запись
	void removeRow(object row);

	int getLastTableId<T>();

    IEnumerable<T> getQueryResult<T>(string query) where T : class, new();
}
