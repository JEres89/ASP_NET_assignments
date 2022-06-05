namespace ASP_NET_assignments.Models
{
	public interface I_DataViewModel<T> where T : I_DbDataModel<T>
	{
		string[] PostNames { get; set; }
		public bool ListEnd { get; }
		public T GetItem { get; }
		public bool SetNextItem(int id);
		public void Search(string value);
		public void AddItem(T item);
		public bool RemoveItem(int id);
	}
}
