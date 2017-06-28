using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace MPWU.Database
{
	public class ParamDB
	{
		private SQLiteConnection connection;

		public ParamDB()
		{
			//Getting conection and Creating table  
			connection = DependencyService.Get<ISQLite>().OpenConnection();
			connection.CreateTable<CustomSchedule>();
			connection.CreateTable<Param>();
		}

		//Retourne le dernier param enregistré 
		public Param LastParam()
		{
			return connection.Table<Param>().LastOrDefault();
		}

		//Retourne le dernier Schedule enregistré 
		public CustomSchedule LastSchedule()
		{
			return connection.Table<CustomSchedule>().LastOrDefault();
		}

		public Param InitParam()
		{
			if (this.LastParam() == null)
			{
				this.AddParam(new Param());
			}
			return this.LastParam();
		}

		public CustomSchedule InitCustomSchedule()
		{
			if (this.LastSchedule() == null)
			{
				this.AddSchedule(new CustomSchedule());
			}
			return this.LastSchedule();
		}

		//Met a jour le dernier param de la DB  
		public void UpdateParam(Param param)
		{
			connection.Update(param);
		}

		public void AddParam(Param param)
		{
			connection.Insert(param);
		}

		//Met a jour le dernier param de la DB
		public void UpdateSchedule(CustomSchedule schedule)
		{
			connection.Update(schedule);
		}

		public void AddSchedule(CustomSchedule schedule)
		{
			connection.Insert(schedule);
		}
	}
}