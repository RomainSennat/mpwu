using SQLite.Net;
using MPWU; 

namespace MPWU.Database
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}