public struct DataItem
{
	public float time;
	public System.Numerics.Vector3 magneticField;

	public DataItem(float time_, System.Numerics.Vector3 magneticField_)
	{
		time = time_;
		magneticField = magneticField_;
	}

	public override string ToString()
	{
		return time.ToString() + ": " + magneticField.ToString();
	}
}

public struct Grid
{
	public float startTime;
	public float timeStep;
	public int numNodes;

	public Grid(float startTime_, float timeStep_, int numNodes_)
	{
		startTime = startTime_;
		timeStep = timeStep_;
		numNodes = numNodes_;
	}

	public override string ToString()
	{
		return startTime.ToString() + " " + timeStep.ToString()
			+ " " + numNodes.ToString();
	}
}

public abstract class V1Data
{
	public string id
	{
		get;
		protected set;
	}

	public System.DateTime date
	{
		get;
		protected set;
	}

	public V1Data(string id_, System.DateTime date_)
	{
		id = id_;
		date = date_;
	}

	public abstract float[] NearZero(float eps);

	public abstract string ToLongString();

	public override string ToString()
	{
		return id + ": " + date.ToString();
	}
}
