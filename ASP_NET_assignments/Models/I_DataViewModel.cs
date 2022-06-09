namespace ASP_NET_assignments.Models
{
	public interface I_DataViewModel<T> where T : I_DbDataModel<T>
	{
		string[] ColumnNames { get; set; }
		public bool ListEnd { get; }
		public T GetNextItem { get; }
		public T GetItem(int id);
		public void Search(string value);
		public void AddItem(T item);
		public bool RemoveItem(int id);
	}
}
