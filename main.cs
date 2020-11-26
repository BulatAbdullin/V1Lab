public class Program
{
	public static int Main()
	{
		try {
			// Let's create a V1DataOnGrid object and print its info.
			System.Console.WriteLine("V1DataOnGrid");
			V1DataOnGrid dataOnGrid = new V1DataOnGrid("input.txt");
			System.Console.WriteLine(dataOnGrid.ToLongString("n3"));

			// Create V1MainCollection
			System.Console.WriteLine("V1MainCollection");
			V1MainCollection mainCollection = new V1MainCollection();
			mainCollection.AddDefaults();
			System.Console.WriteLine(mainCollection.ToLongString("n2"));
			System.Console.WriteLine("MaxLength: " + mainCollection.MaxLength.ToString());
			System.Console.WriteLine("ArgmaxLength: " + mainCollection.ArgmaxLength.ToString());
			System.Console.WriteLine("NonUniqueTime:");
			foreach (float t in mainCollection.NonUniqueTime) {
				System.Console.WriteLine("                " + t.ToString());
			}
		} catch (System.Exception e) {
			System.Console.WriteLine(e.Message);
		}
		return 0;
	}
}
