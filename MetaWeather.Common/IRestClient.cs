using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetaWeather.Common
{
   public interface IRestClient
    {

        string BaseUrl { get; set; }
        string urlParameter { get; set; }

        Task<string> RestclientCall();
    }
}
