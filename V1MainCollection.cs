using System.Linq;

public class V1MainCollection : System.Collections.Generic.IEnumerable<V1Data>
{
	private System.Collections.Generic.List<V1Data> list;

	public int Count
	{
		get
		{
			return list.Count;
		}
	}

	public float MaxLength
	{
		get
		{
			return 0;
		}
	}

	public V1MainCollection()
	{
		list = new System.Collections.Generic.List<V1Data>();
	}

	public void Add(V1Data item)
	{
		list.Add(item);
	}

	public bool Remove(string id, System.DateTime date)
	{
		bool is_found = false;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].id == id && list[i].date == date) {
				list.RemoveAt(i);
				i--;
				is_found = true;
			}
		}
		return is_found;
	}

	public void AddDefaults()
	{
		list.Add(new V1DataOnGrid("V1DataOnGrid_default"));

		V1DataOnGrid emptyDataOnGrid = new V1DataOnGrid(
				"default V1DataOnGrid",
				new System.DateTime(2020, 9, 27),
				new Grid(0, 0.2f, 0)
		);
		list.Add(emptyDataOnGrid);

		V1DataCollection emptyDataCollection = new V1DataCollection(
				"default V1DataCollection",
				System.DateTime.Today
		);
		list.Add(emptyDataCollection);
	}

	public override string ToString()
	{
		string info = "";
		for (int i = 0; i < Count; i++) {
			info += i.ToString() + ") " + list[i].ToString() + "\n";
		}
		return info;
	}

	public string ToLongString(string format)
	{
		string info = "";
		for (int i = 0; i < Count; i++) {
			info += "Dataset " + i.ToString() + ":\n"
				+ list[i].ToLongString(format) + "\n";
		}
		return info;
	}

	// Implementation of IEnumerable<V1Data> interface
	public System.Collections.Generic.IEnumerator<V1Data> GetEnumerator()
	{
		return list.GetEnumerator();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}
}
