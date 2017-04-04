using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace MPWU.Database
{
	public class ParamDB
	{
		private SQLiteConnection sqlconnection;

		public ParamDB()
		{
			//Getting conection and Creating table  
			sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
			sqlconnection.CreateTable<Param>();
		}


		//Retourne le dernier param enregistré 
		public Param GetLastParam()
		{
			return sqlconnection.Table<Param>().LastOrDefault();
		}


		//Ajoute un nouveau param à la DB  
		public void AddParam(Param param)
		{
			sqlconnection.Insert(param);
		}
	}
}