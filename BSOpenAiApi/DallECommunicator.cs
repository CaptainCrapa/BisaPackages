using BSOpenAiApi.BaseModel;
using BSOpenAiApi.OpenAiDataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSOpenAiApi
{
    public class DallECommunicator
    {
        string ApiKey = "";
        string ApiUrl = "";
        string Size = "";
        BasicDalleAPImodel api = new BasicDalleAPImodel();
        public DallECommunicator(string apiKey, string apiUrl, string size)
        {
            ApiKey = apiKey;
            ApiUrl = apiUrl;
            Size = size;
        }
        public async Task<string> GetResponse(string prompt, int timeout = 1000)
        {
            var data = api.GetData( tprompt:prompt, tsize: Size);
            var oac = new OpenAiApiConnection(ApiKey, ApiUrl, timeout);
            var res = await oac.GetResponse(data);
            var res_string = await res.Content.ReadAsStringAsync();
            var response = api.DeSerialize(res_string);
            return response.data[0].url;
        }
    }
}
