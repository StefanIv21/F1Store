namespace MobyLabWebProgramming.Core.Requests;

/// <summary>
/// Use this class to get the pagination query parameters from the HTTP request.
/// You can extend the class to add more parameters to the pagination query.
/// </summary>
public class PaginationQueryParams
{
    /// <summary>
    /// Page is the number of the page.
    /// </summary>
    public int Page { get; set; } = 1;
    /// <summary>
    /// PageSize is the maximum number of entries on each page.
    /// </summary>
    public int MaxPageSize  = 10;

    private int pageSize = 8;
    
    public int PageSize
    {
        get => pageSize;
        set => pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    
}