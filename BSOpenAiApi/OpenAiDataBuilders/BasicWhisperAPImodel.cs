using BSOpenAiApi.OpenAiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BSOpenAiApi.OpenAiDataBuilders
{
    internal class BasicWhisperAPImodel
    {
        public WhisperResponse DeSerialize(string response_string)
        {
            return JsonSerializer.Deserialize<WhisperResponse>(response_string);
        }

        public object GetData(string Ffile, string prompt, string response_format = "json", float temperature = 0)
        {
            return new
            {
                file = File.ReadAllBytes(Ffile),
                model = "whisper-1",
                prompt = prompt,
                response_format = response_format,
                temperature = temperature
            };

        }

        public async Task<string> JsonSerialize(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<WhisperResponse>(responseContent);
            return responseObject.text;
        }
    }
}
