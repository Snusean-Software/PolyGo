namespace PolyGo.Models.Navigation.Campus
{
	public class Route
	{
		public int ID { get; set; } // ID for SQLite
		public string Start { get; set; }
		public string End { get; set; }
		public string GeoJSON { get; set; }

		public Route()
		{

		}
		public Route(string s1, string s2, string geoJson)
		{
			Start = s1;
			End = s2;
			GeoJSON = geoJson;
		}
	}
}
