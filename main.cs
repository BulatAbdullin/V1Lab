public class Program
{
	public static int Main()
	{
		// Let's create a V1DataOnGrid object and print its info.
		Grid grid = new Grid(0, 0.2f, 30);
		System.DateTime date = new System.DateTime(2020, 9, 27);
		V1DataOnGrid dataOnGrid = new V1DataOnGrid("some info here", date, grid);
		dataOnGrid.InitRandom(0, 0.2f);
		System.Console.WriteLine(dataOnGrid.ToLongString());

		// Convert it to V1DataOnGrid explicitly.
		V1DataCollection dataCollection = (V1DataCollection) dataOnGrid;
		System.Console.WriteLine(dataCollection.ToLongString());

		// Create V1MainCollection
		V1MainCollection mainCollection = new V1MainCollection();
		mainCollection.AddDefaults();
		System.Console.WriteLine("V1MainCollection");
		System.Console.WriteLine(mainCollection.ToString());

		// NearZero
		System.Console.WriteLine("NearZero:");
		foreach (V1Data data in mainCollection) {
			System.Array.ForEach(data.NearZero(0.5f), System.Console.WriteLine);
			System.Console.WriteLine("");
		}
		return 0;
	}
}
