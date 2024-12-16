﻿namespace Shared.Models
{
    public class PagedResult<T>
    {
        public List<T> Result { get; set; } = [];
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
