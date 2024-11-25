using System;
using System.Net;
using System.Runtime.Serialization;

namespace WebApplication3.Common
{
    [DataContract]
    public class ResponseService<T>
    {
        [DataMember]
        public bool status { get; set; }
        [DataMember]
        public int error_code { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public T data { get; set; }
        public HttpStatusCode status_code { get; set; }


        /// <summary>
        /// return response success with resource response
        /// </summary>
        /// <param name="data"></param>
        public ResponseService(T data)
        {
            this.status = true;
            this.message = string.Empty;
            this.data = data;
            this.status_code = HttpStatusCode.OK;
        }

    }
}
 