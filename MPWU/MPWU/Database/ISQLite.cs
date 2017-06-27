using SQLite.Net;

namespace MPWU.Database
{
	public interface ISQLite
	{
		SQLiteConnection OpenConnection();
	}
}