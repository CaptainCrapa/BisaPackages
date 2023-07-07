using BSOpenAiApi.OpenAIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSOpenAiApi.OpenAiDataBuilders
{
    public interface IChatGptAPIModel
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
        public object GetData(List<OpenAiMessage> messages);
        public Task<OpenAiChatResponse> JsonSerialize(HttpResponseMessage response);
        public OpenAiChatResponse DeSerialize(string response_string);

    }
    public enum GptModel
    {
        Gpt3_5,
        Gpt4_0
    }
}
