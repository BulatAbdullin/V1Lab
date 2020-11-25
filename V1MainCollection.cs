using System.Linq;

public class V1MainCollection : System.Collections.Generic.IEnumerable<V1Data>
{
	private System.Collections.Generic.List<V1Data> datasets;

	public int Count
	{
		get
		{
			return datasets.Count;
		}
	}

	public float MaxLength
	{
		get
		{
			return (
				from dataset in datasets
				from dataItem in dataset
				select dataItem.MagneticField.Length()).Max();

		}
	}

	public V1MainCollection()
	{
		datasets = new System.Collections.Generic.List<V1Data>();
	}

	public void Add(V1Data item)
	{
		datasets.Add(item);
	}

	public bool Remove(string id, System.DateTime date)
	{
		bool is_found = false;
		for (int i = 0; i < datasets.Count; i++)
		{
			if (datasets[i].id == id && datasets[i].date == date) {
				datasets.RemoveAt(i);
				i--;
				is_found = true;
			}
		}
		return is_found;
	}

	public void AddDefaults()
	{
		datasets.Add(new V1DataOnGrid("V1DataOnGrid_default.txt"));

		V1DataOnGrid emptyDataOnGrid = new V1DataOnGrid(
				"default V1DataOnGrid (empty)",
				new System.DateTime(2020, 9, 27),
				new Grid(0, 0.2f, 0)
		);
		datasets.Add(emptyDataOnGrid);

		V1DataCollection emptyDataCollection = new V1DataCollection(
				"default V1DataCollection (empty)",
				System.DateTime.Today
		);
		datasets.Add(emptyDataCollection);
	}

	public override string ToString()
	{
		string info = "";
		for (int i = 0; i < Count; i++) {
			info += i.ToString() + ") " + datasets[i].ToString() + "\n";
		}
		return info;
	}

	public string ToLongString(string format)
	{
		string info = "";
		for (int i = 0; i < Count; i++) {
			info += "Dataset " + i.ToString() + ":\n"
				+ datasets[i].ToLongString(format) + "\n";
		}
		return info;
	}

	// Implementation of IEnumerable<V1Data> interface
	public System.Collections.Generic.IEnumerator<V1Data> GetEnumerator()
	{
		return datasets.GetEnumerator();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}
}
