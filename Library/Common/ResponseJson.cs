using Library.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class ResponseJson<T, TLines> where T : BaseDto<TLines>, new() where TLines : BaseLineDto, new()
    {

        public string Message { get; set; } = "failed. Try again.";
        public bool IsSuccess { get; set; } = false;
        public T Data { get; set; } = new();
    }


    public class DocsUploadModel
    {
        public string DocumentNo { get; set; } = string.Empty; 
        public int IncomingEntryNo { get; set; } = 0; 
        public string FileName { get; set; } = string.Empty;
        public string DocUrl { get; set; } = string.Empty;
        public string DocType { get; set; } = string.Empty;
        public string UploadMessage { get; set; } = string.Empty;
        public IFormFile FormFile { get; set; }
    }

    public class JsonBody<T, TLines> where T : BaseDto<TLines>, new() where TLines : BaseLineDto, new()
    {

        public string Message { get; set; } = "failed. Try again.";
        public bool IsSuccess { get; set; } = false;
        public List<T> Data { get; set; } = new();
        public List<TLines> Lines { get; set; } = new();
        public Metadata Metadata { get; set; } = new();
    }

    public  class Metadata
    {
        public int Page { get; set; } = 0;
        public int PerPage { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
    }
}
