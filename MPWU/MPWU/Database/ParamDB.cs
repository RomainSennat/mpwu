using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace MPWU.Database
{
	public class StudentDB
	{
		private SQLiteConnection _sqlconnection;

		public StudentDB()
		{
			//Getting conection and Creating table  
			_sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
			_sqlconnection.CreateTable<Param>();
		}


		//Get specific student  
		public Param GetStudent()
		{
			return _sqlconnection.Table<Param>().First();
		}


		//Add new student to DB  
		public void AddStusent(Param param)
		{
			_sqlconnection.Insert(param);
		}
	}
}