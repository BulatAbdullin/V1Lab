public class Program
{
	public static int Main()
	{
		// Let's create a V1DataOnGrid object and print its info.
		try {
			V1DataOnGrid dataOnGrid = new V1DataOnGrid("input.txt");
			//System.Console.WriteLine(dataOnGrid.ToLongString("n3"));
			System.Console.WriteLine(dataOnGrid.ToString());
			foreach (DataItem dataItem in dataOnGrid) {
				System.Console.WriteLine(dataItem.ToString("n3"));
			}
			return 0;
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
		} catch (System.Exception e) {
			System.Console.WriteLine(e.Message);
		}
		return 0;
	}
}
