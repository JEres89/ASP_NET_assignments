namespace ASP_NET_assignments.Data
{
	public class AppRepo
	{
		readonly AppDbContext _context;

		public AppRepo(AppDbContext DbContext)
		{
			_context = DbContext;
		}
	}
}
