using BSOpenAiApi.OpenAIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BSOpenAiApi.OpenAiDataBuilders
{
    public class ChatGpt3_5 : IChatGptAPIModel
    {
        /// <summary>
        /// A paraméterek a default értékek, de szabadon felülírhatóak
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="top_p"></param>
        /// <param name="frequency_penalty"></param>
        /// <param name="presence_penalty"></param>
        /// <param name="n"></param>
        /// <param name="stream"></param>
        public ChatGpt3_5(float temperature = 1, float top_p = 1, int frequency_penalty = 0, int presence_penalty = 0, int n = 1, bool stream = false)
        {
            this.temperature = temperature;
            this.top_p = top_p;
            this.frequency_penalty = frequency_penalty;
            this.presence_penalty = presence_penalty;
            this.n = n;
            this.stream= stream;
        }
        public OpenAiChatResponse DeSerialize(string response_string)
        {
            return JsonSerializer.Deserialize<OpenAiChatResponse>(response_string);
        }

        float temperature;
        float top_p;
        int frequency_penalty;
        int presence_penalty;
        int n;
        bool stream;
        /// <summary>
        /// Összeállítja a POSThoz szükséges adatokat.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public object GetData(List<OpenAiMessage> messages)
        {
            return new
            {
                model = "gpt-3.5-turbo",
                messages = messages.ToArray(),
                temperature = temperature,
                top_p = top_p,
                frequency_penalty = frequency_penalty,
                presence_penalty = presence_penalty,
                n = n,
                stream = stream
            };
        }

        public async Task<OpenAiChatResponse> JsonSerialize(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<OpenAiChatResponse>(responseContent);
            return responseObject;
        }
    }
}
