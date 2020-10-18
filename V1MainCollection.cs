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

	public V1MainCollection()
	{
		list = new System.Collections.Generic.List<V1Data>();
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

	public void Add(V1Data item)
	{
		list.Add(item);
	}

	public bool Remove(string id, System.DateTime date)
	{
		bool found = false;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].id == id && list[i].date == date) {
				list.RemoveAt(i);
				i--;
				found = true;
			}
		}
		return found;
	}

	public void AddDefaults()
	{
		V1DataOnGrid dataOnGrid = new V1DataOnGrid(
				"default",
				new System.DateTime(2020, 9, 27),
				new Grid(0, 0.2f, 30)
		);
		dataOnGrid.InitRandom(0, 0.8f);
		list.Add(dataOnGrid);

		V1DataCollection[] dataCollection = new V1DataCollection[2];
		for (int i = 0; i < 2; i++) {
			dataCollection[i] = new V1DataCollection(
					"id" + i.ToString(),
					System.DateTime.Today
			);
			dataCollection[i].InitRandom(15, 0, 10, -0.8f, 0.5f);
			list.Add(dataCollection[i]);
		}
	}

	public override string ToString()
	{
		string info = "";
		for (int i = 0; i < Count; i++) {
			info += i.ToString() + ") " + list[i].ToString() + "\n";
		}
		return info;
	}
}
