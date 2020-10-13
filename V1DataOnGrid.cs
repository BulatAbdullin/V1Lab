public class V1DataOnGrid : V1Data
{
	private Grid grid;
	private System.Numerics.Vector3[] measurements;

	public V1DataOnGrid(string id_, System.DateTime date_, Grid grid_)
		: base(id_, date_)
	{
		grid = grid_;
		measurements = new System.Numerics.Vector3[grid.numNodes];
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
}
