using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFront.Models
{
	public class PagedList
	{
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
 		public int CurrentPage { get; set; }
		public int TotalCount { get; set; }
		public bool HasPrevious { get; set; }
		public bool HasNext { get; set; }

	}

	public class PagedList<T> : List<T>
	{
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
		public int TotalCount { get; set; }
		public bool HasPrevious { get; set; }
		public bool HasNext { get; set; }

		public PagedList(List<T> items, int count, int pageNumber, int pageSize, bool nex, bool prev)
		{
			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			HasNext = nex;
			HasPrevious = prev;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);

			AddRange(items);
		}

		public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize, bool nex, bool prev)
		{
			var count = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PagedList<T>(items, count, pageNumber, pageSize, nex, prev);
		}

	}
}
