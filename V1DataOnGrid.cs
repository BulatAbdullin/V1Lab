public class V1DataOnGrid : V1Data, System.Collections.Generic.IEnumerable<DataItem>
{
	private Grid grid;
	private System.Numerics.Vector3[] measurements;

	public V1DataOnGrid(string id_, System.DateTime date_, Grid grid_)
		: base(id_, date_)
	{
		grid = grid_;
		measurements = new System.Numerics.Vector3[grid.numNodes];
	}

	public V1DataOnGrid(string filename)
	{
		try {
			System.IO.StreamReader sr = System.IO.File.OpenText(filename);
			this.id = sr.ReadLine();
			this.date = System.DateTime.Parse(sr.ReadLine());
			string[] gridParams
				= sr.ReadLine().Split(" ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			if (gridParams.Length != 3) {
				throw new System.Exception("Not a grid.");
			}
			grid.startTime = float.Parse(gridParams[0]);
			grid.timeStep = float.Parse(gridParams[1]);
			grid.numNodes = int.Parse(gridParams[2]);
			this.measurements = new System.Numerics.Vector3[grid.numNodes];
			for (int i = 0; i < grid.numNodes; i++) {
				string input = sr.ReadLine();
				if (input == null) {
					throw new System.Exception("Too few measurements.");
				}
				string[] measurement
					= input.Split(" ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
				if (measurement.Length != 3) {
					throw new System.Exception(
							System.String.Format("Measurement {0} must contain 3 components", i));
				}
				this.measurements[i].X = float.Parse(measurement[0]);
				this.measurements[i].Y = float.Parse(measurement[1]);
				this.measurements[i].Z = float.Parse(measurement[2]);
			}
		} catch (System.Exception e) {
			System.Console.WriteLine(e.Message);
			throw new System.Exception("Failed to create V1DataOnGrid object.");
		}
	}

	public void InitRandom(float minValue, float maxValue)
	{
		System.Random rand = new System.Random();
		for (int i = 0; i < grid.numNodes; i++) {
			float range = maxValue - minValue;
			measurements[i].X = (float) (minValue + range * rand.NextDouble());
			measurements[i].Y = (float) (minValue + range * rand.NextDouble());
			measurements[i].Z = (float) (minValue + range * rand.NextDouble());
		}
	}

	public void SetMeasurements(System.Numerics.Vector3[] measurements_)
	{
		measurements = measurements_;
	}

	public static explicit operator V1DataCollection(V1DataOnGrid data)
	{
		V1DataCollection to = new V1DataCollection(data.id, data.date);
		System.Collections.Generic.List<DataItem> list
				= new System.Collections.Generic.List<DataItem>(data.grid.numNodes);
		for (int i = 0; i < data.grid.numNodes; i++) {
			DataItem item = new DataItem(
					data.grid.startTime + i * data.grid.timeStep,
					data.measurements[i]
			);
			list.Add(item);
		}
		to.SetMeasurements(list);
		return to;
	}

	public override float[] NearZero(float eps)
	{
		System.Collections.Generic.List<float> nearZeroTimestamps
				= new System.Collections.Generic.List<float>();
		for (int i = 0; i < grid.numNodes; i++) {
			if (measurements[i].Length() < eps) {
				nearZeroTimestamps.Add(grid.startTime + i * grid.timeStep);
			}
		}
		return nearZeroTimestamps.ToArray();
	}

	public override string ToString()
	{
		return "type: " + this.GetType().Name + "; id: " + id
				+ "; date: " +  date.ToString() + "; grid: " + grid.ToString();
	}

	public override string ToLongString()
	{
		string gridInfo = "";
		for (int i = 0; i < grid.numNodes; i++) {
			gridInfo += (grid.startTime + i * grid.timeStep).ToString("N5")
					+ ": " + measurements[i].ToString("N5") + "\n";
		}
		return ToString() + "\n" + gridInfo;
	}

	public override string ToLongString(string format)
	{
		string gridInfo = "";
		for (int i = 0; i < grid.numNodes; i++) {
			gridInfo += (grid.startTime + i * grid.timeStep).ToString(format)
					+ ": " + measurements[i].ToString(format) + "\n";
		}
		return "type: " + this.GetType().Name
			+ "; id: " + id
			+ "; date: " +  date.ToString()
			+ "; grid: " + grid.ToString(format)
			+ "\n" + gridInfo;
	}

	// Implementation of IEnumerable<DataItem> interface
	public override System.Collections.Generic.IEnumerator<DataItem> GetEnumerator()
	{
		return (System.Collections.Generic.IEnumerator<DataItem>)
			new V1DataOnGridEnum(this.grid, this.measurements);
	}

	// When we implement IEnumerable, we must also implement IEnumerator.
	public class V1DataOnGridEnum : System.Collections.Generic.IEnumerator<DataItem>
	{
		public DataItem[] dataItems;
		// Position the enumerator before the first element
		// until the first MoveNext() call.
		private int position = -1;

		public V1DataOnGridEnum(Grid grid, System.Numerics.Vector3[] measurements)
		{
			dataItems = new DataItem[grid.numNodes];
			for (int i = 0; i < grid.numNodes; i++) {
				dataItems[i] = new DataItem(grid.startTime + i * grid.timeStep, measurements[i]);
			}
		}

		public bool MoveNext()
		{
			position++;
			return (position < dataItems.Length);
		}

		public void Reset()
		{
			position = -1;
		}

		object System.Collections.IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		public DataItem Current
		{
			get
			{
				try {
					return dataItems[position];
				} catch (System.IndexOutOfRangeException) {
					throw new System.InvalidOperationException();
				}
			}
		}

		void System.IDisposable.Dispose()
		{}
	}
}
