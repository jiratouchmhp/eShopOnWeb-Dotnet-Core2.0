namespace BlazorShared.DTOs;

public class CatalogIndexDto
{
    public IEnumerable<CatalogItemDto> CatalogItems { get; set; } = Enumerable.Empty<CatalogItemDto>();
    public IEnumerable<SelectListItemDto> Brands { get; set; } = Enumerable.Empty<SelectListItemDto>();
    public IEnumerable<SelectListItemDto> Types { get; set; } = Enumerable.Empty<SelectListItemDto>();
    public int? BrandFilterApplied { get; set; }
    public int? TypesFilterApplied { get; set; }
    public PaginationInfoDto PaginationInfo { get; set; } = new();
}

public class SelectListItemDto
{
    public string Value { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool Selected { get; set; }
}

public class PaginationInfoDto
{
    public int ActualPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public string Previous { get; set; } = string.Empty;
    public string Next { get; set; } = string.Empty;
    public bool HasPrevious => ActualPage > 0;
    public bool HasNext => ActualPage < TotalPages - 1;
}