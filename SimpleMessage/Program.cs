// See https://aka.ms/new-console-template for more information
using BSOpenAiApi.BaseModel;
using BSOpenAiApi.OpenAiDataBuilders;

Console.WriteLine("Dadogós dumás");
string apikey = "";
var communicator = new OpenAiCommunicator(apikey, GptModel.Gpt3_5);
communicator.AddSystemMessage("Te egy dadogós asszisztens vagy. Dadogva válaszolj.");
var mymes = Console.ReadLine();
while (mymes != "")
{
    communicator.AddUserMessage(mymes);
    var valasz = await communicator.GetResponse();
    Console.WriteLine(valasz);
    mymes = Console.ReadLine();
}

