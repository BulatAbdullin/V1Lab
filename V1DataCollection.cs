public class V1DataCollection : V1Data, System.Collections.Generic.IEnumerable<DataItem> 
{
	private System.Collections.Generic.List<DataItem> dataItems;

	public V1DataCollection(string id_, System.DateTime date_)
		: base(id_, date_)
	{
		dataItems = new System.Collections.Generic.List<DataItem>();
	}

	public void InitRandom(int nItems, float tmin, float tmax, float minValue,
				float maxValue)
	{
		System.Random rand = new System.Random();
		for (int i = 0; i < nItems; i++) {
			float time = (float) (tmin + (tmax - tmin) * rand.NextDouble());
			float range = maxValue - minValue;
			System.Numerics.Vector3 vector = new System.Numerics.Vector3();
			vector.X = (float) (minValue + range * rand.NextDouble());
			vector.Y = (float) (minValue + range * rand.NextDouble());
			vector.Z = (float) (minValue + range * rand.NextDouble());
			dataItems.Add(new DataItem(time, vector));
		}
	}

	public void SetMeasurements(System.Collections.Generic.List<DataItem> dataItems_)
	{
		dataItems = dataItems_;
	}

	public override float[] NearZero(float eps)
	{
		System.Collections.Generic.List<float> nearZeroTimestamps
				= new System.Collections.Generic.List<float>();
		foreach (DataItem item in dataItems) {
			if (item.magneticField.Length() < eps) {
				nearZeroTimestamps.Add(item.time);
			}
		}
		return nearZeroTimestamps.ToArray();
	}


	public override string ToString()
	{
		return "type: " + this.GetType().Name
			+ "; id: " + id
			+ "; date: " +  date.ToString()
			+ "; nItems: " + dataItems.Count.ToString();
	}

	public override string ToLongString()
	{
		string dataItemsInfo = "";
		foreach (DataItem item in dataItems) {
			dataItemsInfo += item.time.ToString("N5") +
					": " + item.magneticField.ToString("N5") + "\n";
		}
		return ToString() + "\n" + dataItemsInfo;
	}

	public override string ToLongString(string format)
	{
		string dataItemsInfo = "";
		foreach (DataItem item in dataItems) {
			//dataItemsInfo += item.time.ToString(format) +
					//": " + item.magneticField.ToString(format) + "\n";
			dataItemsInfo += item.ToString(format) + "\n";
		}
		return this.ToString() + "\n" + dataItemsInfo;
	}

	// Implementation of IEnumerable<DataItem> interface
	public override System.Collections.Generic.IEnumerator<DataItem> GetEnumerator()
	{
		return dataItems.GetEnumerator();
	}
}
