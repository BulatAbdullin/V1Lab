using System.Linq;

public struct DataItem
{
	public float time;
	public System.Numerics.Vector3 magneticField;

	public System.Numerics.Vector3 MagneticField
	{
		get
		{
			return magneticField;
		}
	}

	public DataItem(float time_, System.Numerics.Vector3 magneticField_)
	{
		time = time_;
		magneticField = magneticField_;
	}

	public override string ToString()
	{
		return time.ToString() + " " + magneticField.ToString();
	}

	public string ToString(string format)
	{
		return time.ToString(format) + " " + magneticField.ToString(format)
			+ " " +  magneticField.Length().ToString(format);
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
		return startTime.ToString() + " " + timeStep.ToString() + " " + numNodes.ToString();
	}

	public string ToString(string format)
	{
		return startTime.ToString(format) + " " + timeStep.ToString(format)
			+ " " + numNodes.ToString();
	}
}

public abstract class V1Data : System.Collections.Generic.IEnumerable<DataItem>
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

	public V1Data(string id_ = "id", System.DateTime date_ = new System.DateTime())
	{
		id = id_;
		date = date_;
	}

	public abstract float[] NearZero(float eps);

	public abstract string ToLongString();

	public abstract string ToLongString(string format);

	public override string ToString()
	{
		return id + ": " + date.ToString();
	}

	public abstract System.Collections.Generic.IEnumerator<DataItem> GetEnumerator();

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
