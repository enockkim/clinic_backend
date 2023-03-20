using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;

namespace clinic
{
    //
    // Summary:
    //     Contains a LINQ query in a serializable format.
    public class Query
    {
        //
        // Summary:
        //     Gets or sets the filter.
        //
        // Value:
        //     The filter.
        public string Filter { get; set; }

        //
        // Summary:
        //     Gets or sets the filter parameters.
        //
        // Value:
        //     The filter parameters.
        public object[] FilterParameters { get; set; }

        //
        // Summary:
        //     Gets or sets the order by.
        //
        // Value:
        //     The order by.
        public string OrderBy { get; set; }

        //
        // Summary:
        //     Gets or sets the expand.
        //
        // Value:
        //     The expand.
        public string Expand { get; set; }

        //
        // Summary:
        //     Gets or sets the select.
        //
        // Value:
        //     The select.
        public string Select { get; set; }

        //
        // Summary:
        //     Gets or sets the skip.
        //
        // Value:
        //     The skip.
        public int? Skip { get; set; }

        //
        // Summary:
        //     Gets or sets the top.
        //
        // Value:
        //     The top.
        public int? Top { get; set; }

        //
        // Summary:
        //     Converts the query to OData query format.
        //
        // Parameters:
        //   url:
        //     The URL.
        //
        // Returns:
        //     System.String.
        public string ToUrl(string url)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (Skip.HasValue)
            {
                dictionary.Add("$skip", Skip.Value);
            }

            if (Top.HasValue)
            {
                dictionary.Add("$top", Top.Value);
            }

            if (!string.IsNullOrEmpty(OrderBy))
            {
                dictionary.Add("$orderBy", OrderBy);
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                dictionary.Add("$filter", UrlEncoder.Default.Encode(Filter));
            }

            if (!string.IsNullOrEmpty(Expand))
            {
                dictionary.Add("$expand", Expand);
            }

            if (!string.IsNullOrEmpty(Select))
            {
                dictionary.Add("$select", Select);
            }

            return string.Format("{0}{1}", url, dictionary.Any() ? ("?" + string.Join("&", dictionary.Select((KeyValuePair<string, object> a) => $"{a.Key}={a.Value}"))) : "");
        }
    }
}