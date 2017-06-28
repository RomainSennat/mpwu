using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace MPWU.Database
{
	/// <summary>
	/// Parameters database.
	/// </summary>
	public class ParamDB
	{
		/// <summary>
		/// The connection to database.
		/// </summary>
		private SQLiteConnection connection;

		public ParamDB()
		{
			//Getting conection and Creating table  
			connection = DependencyService.Get<ISQLite>().OpenConnection();
			connection.CreateTable<CustomSchedule>();
			connection.CreateTable<Param>();
		}

		/// <summary>
		/// Get Last parameter.
		/// </summary>
		/// <returns>The parameters.</returns>
		public Param LastParam()
		{
			return connection.Table<Param>().LastOrDefault();
		}

		/// <summary>
		/// Get last schedule.
		/// </summary>
		/// <returns>The schedule.</returns>
		public CustomSchedule LastSchedule()
		{
			return connection.Table<CustomSchedule>().LastOrDefault();
		}

		/// <summary>
		/// Init the parameter.
		/// </summary>
		/// <returns>The parameters.</returns>
		public Param InitParam()
		{
			if (this.LastParam() == null)
			{
				this.AddParam(new Param());
			}
			return this.LastParam();
		}

		/// <summary>
		/// Init the custom schedule.
		/// </summary>
		/// <returns>The custom schedule.</returns>
		public CustomSchedule InitCustomSchedule()
		{
			if (this.LastSchedule() == null)
			{
				this.AddSchedule(new CustomSchedule());
			}
			return this.LastSchedule();
		}

		/// <summary>
		/// Updates the parameters.
		/// </summary>
		/// <param name="param">Parameter.</param>
		public void UpdateParam(Param param)
		{
			connection.Update(param);
		}

		/// <summary>
		/// Add the parameters.
		/// </summary>
		/// <param name="param">Parameter.</param>
		public void AddParam(Param param)
		{
			connection.Insert(param);
		}

		/// <summary>
		/// Updates the schedule.
		/// </summary>
		/// <param name="schedule">Schedule.</param>
		public void UpdateSchedule(CustomSchedule schedule)
		{
			connection.Update(schedule);
		}

		/// <summary>
		/// Add the schedule.
		/// </summary>
		/// <param name="schedule">Schedule.</param>
		public void AddSchedule(CustomSchedule schedule)
		{
			connection.Insert(schedule);
		}
	}
}