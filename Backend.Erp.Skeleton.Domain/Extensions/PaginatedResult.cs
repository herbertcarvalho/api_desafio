using System;
using System.Collections.Generic;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    public class PaginatedResult<T> : Result
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        public List<T> Data { get; set; }

        internal PaginatedResult(bool succeeded, List<T> data = default, long count = 0, int page = 1, int pageSize = 10, string message = null)
        {
            Data = data;
            Page = page;
            Succeeded = succeeded;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Message = message;
        }
        public static PaginatedResult<T> Failure(string message)
        => new PaginatedResult<T>(false, default, message: message);

        public static PaginatedResult<T> Success(List<T> data, long count, int page, int pageSize)
            => new PaginatedResult<T>(true, data, count, page, pageSize);

        public static PaginatedResult<T> Success(string message, List<T> data, long count, int page, int pageSize)
            => new PaginatedResult<T>(true, data, count, page, pageSize, message: message);
        public int Page { get; set; }

        public int TotalPages { get; set; }

        public long TotalCount { get; set; }

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
