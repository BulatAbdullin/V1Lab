public class V1DataCollection : V1Data
{
	private System.Collections.Generic.List<DataItem> list;

	public V1DataCollection(string id_, System.DateTime date_)
		: base(id_, date_)
	{
		list = new System.Collections.Generic.List<DataItem>();
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
			list.Add(new DataItem(time, vector));
		}
	}

	public void SetMeasurements(System.Collections.Generic.List<DataItem> list_)
	{
		list = list_;
	}

	public override float[] NearZero(float eps)
	{
		System.Collections.Generic.List<float> nearZeroTimestamps
				= new System.Collections.Generic.List<float>();
		foreach (DataItem item in list) {
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
			+ "; nItems: " + list.Count.ToString();
	}

	public override string ToLongString()
	{
		string listInfo = "";
		foreach (DataItem item in list) {
			listInfo += item.time.ToString("N5") +
					": " + item.magneticField.ToString("N5") + "\n";
		}
		return ToString() + "\n" + listInfo;
	}

	public override string ToLongString(string format)
	{
		string listInfo = "";
		foreach (DataItem item in list) {
			listInfo += item.time.ToString(format) +
					": " + item.magneticField.ToString(format) + "\n";
		}
		return this.ToString() + "\n" + listInfo;
	}
}
