using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBotAttachedToMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new TelegramBotClient("5249143724:AAH5V4DrOQELo82vO4fL2WR4TsInE9nO4Pk");
            client.StartReceiving(Update, Error);

            #region BaseCode
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
            #endregion
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine($"An error occurred: {arg2.Message}");
            return Task.CompletedTask;
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message?.Text == null || message.Text.Trim() == "")
                return;

            await botClient.SendTextMessageAsync(message.Chat.Id, $"You wrote: {message.Text}");
        }
    }
}