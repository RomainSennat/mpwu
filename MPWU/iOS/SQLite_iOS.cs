using System;
using Xamarin.Forms;
using MPWU.iOS;
using System.IO;
using MPWU.Database;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace MPWU.iOS
{
	/// <summary>
	/// SQLite for iOS platform.
	/// </summary>
	public class SQLite_iOS : ISQLite
	{
		/// <summary>
		/// Opens the connection to database.
		/// </summary>
		/// <returns>The database connection.</returns>
		public SQLite.Net.SQLiteConnection OpenConnection()
		{
			var fileName = "Param.db3";
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine(documentsPath, "..", "Library");
			var path = Path.Combine(libraryPath, fileName);

			var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var connection = new SQLite.Net.SQLiteConnection(platform, path);

			return connection;
		}
	}
}