using BSOpenAiApi.BaseModel;
using BSOpenAiApi.OpenAiDataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BSOpenAiApi
{
    public class WhisperCommunicator
    {
        string Api_Key = "";
        string Api_Url = "";
        BasicWhisperAPImodel api = new BasicWhisperAPImodel();
        public WhisperCommunicator(string api_Key, string api_Url)
        {
            Api_Key = api_Key;
            Api_Url = api_Url;
        }
        public async Task<string> GetResponse(string prompt, string filePath, int timeout = 2000)
        {
            var data = api.GetData(prompt, filePath);
            var oac = new OpenAiApiConnection(Api_Key, Api_Url, timeout);
            var res = await oac.GetResponse(data);
            var res_string = await res.Content.ReadAsStringAsync();
            var response = api.DeSerialize(res_string);
            return response.text;
        }
    }
}
