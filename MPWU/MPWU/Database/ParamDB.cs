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

		public Param InitParam() 
		{
			if (this.GetLastParam() == null)
			{
				this.AddParam(new Param());
			}
			return this.GetLastParam();
		}

		//Met a jour le dernier param de la DB  
		public void UpdateParam(Param param)
		{
			sqlconnection.Update(param);
		}

		public void AddParam(Param param)
		{
			sqlconnection.Insert(param);
		}
	}
}