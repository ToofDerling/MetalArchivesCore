using System.Text.Json.Serialization;

namespace MetalArchivesCore.Models.Responses
{
    /// <summary>
    /// Ajax response from server
    /// </summary>
    class SearchResponse<T>
    {
        public string error { get; set; }
     
        public int iTotalRecords { get; set; }
        
        public int iTotalDisplayRecords { get; set; }
        
        public int sEcho { get; set; }
        
        public string[][] aaData { get; set; }

        /// <summary>
        /// Deserialized aaData
        /// </summary>
        [JsonIgnore]
        public List<T> Items { get; set; }
    }
}
