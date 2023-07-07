using BSOpenAiApi.OpenAiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BSOpenAiApi.OpenAiDataBuilders
{
    public class BasicDalleAPImodel
    {
        public DalleResponse DeSerialize(string response_string)
        {
            return JsonSerializer.Deserialize<DalleResponse>(response_string);
        }

        public object GetData(string tprompt, int tn = 1, string tsize = "1024x1024", string tresponse_format = "url")
        {
            return new
            {
                prompt = tprompt,
                n = tn,
                size = tsize,
            };

        }

        public async Task<string> JsonSerialize(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<DalleResponse>(responseContent);
            return responseObject.data[0].url;
        }
    }
}
