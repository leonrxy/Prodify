namespace Prodify.Dtos
{

    public class PaginatedResponseDto<T>
    {
        public PaginatedResponseDto() { }
        public int page_number { get; set; }
        public int total_pages { get; set; }
        public int page_size { get; set; }
        public int total_count { get; set; }
        public string first_page_url { get; set; }
        public string previous_page_url { get; set; }
        public string next_page_url { get; set; }
        public string last_page_url { get; set; }
        public bool has_previous_page => page_number > 1;
        public bool has_next_page => page_number < total_pages;
        public List<LinkDto> links { get; set; }
        public List<T> items { get; set; }

        public PaginatedResponseDto(List<T> items, int count, int page_number, int page_size)
        {
            this.page_number = page_number;
            total_pages = (int)Math.Ceiling(count / (double)page_size);
            this.page_size = page_size;
            total_count = count;
            previous_page_url = page_number > 1 ? $"?page_number={page_number - 1}&page_size={page_size}" : "";
            next_page_url = page_number < total_pages ? $"?page_number={page_number + 1}&page_size={page_size}" : "";
            first_page_url = page_number > 1 ? "?page_number=1&page_size={page_size}" : "";
            last_page_url = page_number < total_pages ? $"?page_number={total_pages}&page_size={page_size}" : "";
            this.items = items;

            var _links = new List<LinkDto>();

            for (int i = 1; i <= total_pages; i++)
            {
                _links.Add(new LinkDto
                {
                    number = i,
                    url = $"?page_number={i}&page_size={page_size}",
                    active = i == page_number,
                });
            }

            links = _links;
        }
    }

    public class LinkDto
    {
        public int number { get; set; }
        public string? url { get; set; }
        public bool active { get; set; }
    }
}

