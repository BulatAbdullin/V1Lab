public class Program
{
	public static int Main()
	{
		try {
			// Let's create a V1DataOnGrid object and print its info.
			V1DataOnGrid dataOnGrid = new V1DataOnGrid("input.txt");
			System.Console.WriteLine(dataOnGrid.ToLongString("n3"));

			// Create V1MainCollection
			V1MainCollection mainCollection = new V1MainCollection();
			mainCollection.AddDefaults();
			return 0;
		} catch (System.Exception e) {
			System.Console.WriteLine(e.Message);
		}
		return 0;
	}
}
