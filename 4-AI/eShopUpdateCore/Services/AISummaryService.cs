using eShopUpdate.Entities;
using Microsoft.Extensions.AI;

namespace eShopUpdateCore.Services
{
    public class AISummaryService
    {

        private readonly IChatClient _client;

        public AISummaryService(IChatClient client)
        {
            _client = client;
        }


        public async Task<ChatResponse> SummarizeReviews(Product product)
        {
            List<ChatMessage> messages = 
                [ new ChatMessage(
                    ChatRole.System, 
                    "You are an AI assistant that helps users succinctly summarize product reviews")];

            messages.AddRange(
                product
                    .Reviews
                    .Select(r => new ChatMessage(ChatRole.User, r.Text)));

            messages.Add(new ChatMessage(ChatRole.User, "Write a brief summary of the product reviews"));

            var response = await _client.GetResponseAsync(messages);

            return response;
        }
    }
}
