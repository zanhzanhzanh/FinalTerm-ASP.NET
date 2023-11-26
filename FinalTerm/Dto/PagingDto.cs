using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class PagingDto {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize {  get; set; }

        public bool SortType { get; set; }

        public string SortField { get; set; }
    }
}
