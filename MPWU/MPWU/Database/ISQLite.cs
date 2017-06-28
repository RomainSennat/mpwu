using SQLite.Net;

namespace MPWU.Database
{
	/// <summary>
	/// SQLite interface.
	/// </summary>
	public interface ISQLite
	{
		/// <summary>
		/// Opens the database connection.
		/// </summary>
		/// <returns>The connection.</returns>
		SQLiteConnection OpenConnection();
	}
}