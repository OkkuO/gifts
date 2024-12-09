using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using snow_bot;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7434767910:AAETd319FNn6ALp7h6dQUj39gVZawoJ0_Tg", cancellationToken: cts.Token); 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContextPool<GiftDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=snowbot_db;Username=postgres;Password=1234;Port=5433"));

builder.Services.AddScoped<GiftRepository>();
builder.Services.AddScoped<GiftService>();

var app = builder.Build();

GiftService commands = app.Services.GetService<GiftService>();



var me = await bot.GetMe();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot


int random (int a, int b) {
    Random rnd = new();
    return rnd.Next(a, b);
}


async Task OnMessage(Message msg, UpdateType type)
{
    string userName = msg.From.FirstName;
    GiftModel UserData = commands!.GetId(msg.From!.Id).Result;

    async void userNotWrited()
    {
        await bot.SendMessage(msg.Chat, 
            $"<a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a> не писал письмо Санте, чтобы получать подарки.", 
            parseMode: ParseMode.Html);
        await bot.DeleteMessage(msg.Chat.Id, msg.MessageId);
    }
    async void userWritingHimself()
    {
        await bot.SendMessage(msg.Chat, 
            $"<a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a> вы не можете подарить себе подарок.", 
            parseMode: ParseMode.Html);
        await bot.DeleteMessage(msg.Chat.Id, msg.MessageId);  
    }
    async void userDontHaveGift()
    {
        await bot.SendMessage(msg.Chat, 
        $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, у вас нет такого подарка.", 
        parseMode: ParseMode.Html);
    }


    if(msg.Text != null) 
    {
        


        userName.Replace("<", "&lt;");
        userName.Replace(">", "&lt;");

        if (random(0, 40) == 0)
        {
            await bot.SetMessageReaction(msg.Chat.Id,
            msg.MessageId,
            reaction: ["🎄"]
            );            
         
        }
        else if (random(0, 40) == 1)
        {
            await bot.SetMessageReaction(msg.Chat.Id,
            msg.MessageId,
            reaction: ["☃️"]
            );   
        }
           
        if (UserData != null)
        {
            int randomCoal = random(0, 50);
            int randomGifts = random(51, 150);

            if (randomCoal == 1)
            {
                await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, Получает в подарок уголь! 🪨",
                    parseMode : ParseMode.Html);
                    commands?.UpdateGiftGet(UserData.Id, 1, 0, 0, 0, 0, 0, 0);
            }

            switch(randomGifts)
            {
                case 52: 
                    await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, получает в подарок доллар! 💵",
                        parseMode : ParseMode.Html);
                        commands?.UpdateGiftGet(UserData.Id, 0, 0, 0, 0, 1, 0, 0);
                break;
                case 53: 
                    await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, получает в подарок колечко! 💍",
                        parseMode : ParseMode.Html);
                        commands?.UpdateGiftGet(UserData.Id, 0, 1, 0, 0, 0, 0, 0);
                break;
                case 54: 
                    await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, получает в подарок носочки! 🧦",
                        parseMode : ParseMode.Html);
                        commands?.UpdateGiftGet(UserData.Id, 0, 0, 1, 0, 0, 0, 0);
                break;
                case 55: 
                    await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, получает в подарок плюшевого мишку 🧸!",
                        parseMode : ParseMode.Html);
                        commands?.UpdateGiftGet(UserData.Id, 0, 0, 0, 1, 0, 0, 0);
                break;
                case 56: 
                    await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, получает в подарок матрешку! 🪆",
                        parseMode : ParseMode.Html);
                        commands?.UpdateGiftGet(UserData.Id, 0, 0, 0, 0, 0, 1, 0);
                break;
                case 57: 
                    await bot.SendMessage(msg.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, получает в подарок шампанское! 🍾",
                        parseMode : ParseMode.Html);
                        commands?.UpdateGiftGet(UserData.Id, 0, 0, 0, 0, 0, 0, 1);
                break; 
            }

            if (randomCoal!=1 & randomGifts!=52 & randomGifts!=53 & randomGifts!=54 & randomGifts!=55 & randomGifts!=56 & randomGifts!=57)
            {
                if(msg.Text == "/me@snowbooo_bot" ^ msg.Text == "/me")
                {
                    List<GiftModel> Gifts = commands!.GetGiftsByUserId(msg.From.Id).Result;
                    string myGifts = "";

                    foreach (var item in Gifts)
                    {
                        myGifts+=$"🪨 Угли: {item.Coal}\n\n";
                        myGifts+=$"💍 Колечко: {item.Ring}\n";
                        myGifts+=$"🧦 Носки: {item.Socks}\n";
                        myGifts+=$"🧸 Плюшевые мишки: {item.Bear}\n";
                        myGifts+=$"💵 Баксы: {item.Dollar}\n";
                        myGifts+=$"🍰 Тортики: {item.Matryoshka}\n";
                        myGifts+=$"🍾 Бутылки шампанского: {item.Bottle}\n";
                    }

                    await bot.SendMessage(msg.Chat, 
                        $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, ваши подарки 🎁\n\n{myGifts}", 
                        parseMode: ParseMode.Html
                    );
                    
                    await bot.DeleteMessage(msg.Chat.Id, msg.MessageId);
                } 

                if (msg.Text == "/coal")
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Coal >= 0)
                        {
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 1, 0, 0, 0, 0, 0, 0);

                            
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 🪨 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                 
                    }
                    else
                    {
                        userNotWrited();
                    }
                }

                if(msg.Text == "/ring") 
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Ring > 0)
                        {
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 💍 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 0, 1, 0, 0, 0, 0, 0);
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                        
                    }
                    else
                    {
                        userNotWrited();        
                    }                                
                }                   

                if(msg.Text == "/socks") 
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Ring > 0)
                        {
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 🧦 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 0, 0, 1, 0, 0, 0, 0);
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                        
                    }
                    else
                    {
                        userNotWrited();        
                    }                                
                }  

                if(msg.Text == "/bear") 
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Ring > 0)
                        {
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 🧸 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 0, 0, 0, 1, 0, 0, 0);
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                        
                    }
                    else
                    {
                        userNotWrited();        
                    }                                
                }  

                if(msg.Text == "/dollar") 
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Ring > 0)
                        {
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 💵 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 0, 0, 0, 0, 1, 0, 0);
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                        
                    }
                    else
                    {
                        userNotWrited();        
                    }                                
                }                   

                if(msg.Text == "/cake") 
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Ring > 0)
                        {
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 🍰 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 0, 0, 0, 0, 0, 1, 0);
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                        
                    }
                    else
                    {
                        userNotWrited();        
                    }                                
                } 

                if(msg.Text == "/bottle") 
                {
                    GiftModel replyUserData = commands!.GetId(msg.ReplyToMessage.From.Id).Result;

                    if (msg.ReplyToMessage!=null & replyUserData!=null & msg.From.Id != msg.ReplyToMessage.From.Id)
                    {
                        if (UserData.Ring > 0)
                        {
                            await bot.SendMessage(msg.Chat, 
                                $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, подарила 🍾 для <a href=\"tg://user?id={msg.ReplyToMessage.From.Id}\">{msg.ReplyToMessage.From.FirstName}</a>", 
                                parseMode: ParseMode.Html
                            );
                            commands!.UpdateGifts(replyUserData!.Id, UserData.Id, 0, 0, 0, 0, 0, 0, 1);
                        }
                        else
                        {   
                            userDontHaveGift();
                        } 
                    }
                    else if(msg.From.Id == msg.ReplyToMessage.From.Id)
                    {
                        userWritingHimself();                        
                    }
                    else
                    {
                        userNotWrited();        
                    }                                
                }                   
            }
                  
        }
        else if(UserData==null)
        {
           if (msg.Text == "/reg@snowbooo_bot" ^ msg.Text == "/reg")
            {
                if (msg.From.Username != null & UserData==null)
                {
                    await bot.SendMessage(msg.Chat, $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, Вы написали Санте и он будет выдавать вам подарки!",
                        parseMode : ParseMode.Html);
                        commands?.AddGift(0, msg.From.Id, msg.From.Username, 0, 0, 0, 0, 0, 0, 0);
                }
                else if (msg?.From?.Username == null)
                {
                    await bot.SendMessage(msg!.Chat, "Санта не принимает письма от ноунэмов :(");
                }
                else if (UserData!=null)
                {
                    await bot.SendMessage(msg!.Chat, $"<a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, Вы уже получаете подарки от Санты!",
                        parseMode : ParseMode.Html
                    );
                }

                await bot.DeleteMessage(msg.Chat.Id, msg.MessageId);
            } 
            else if (msg.Text == "/me@snowbooo_bot" ^ msg.Text == "/me" ^ msg.Text == "/coal" ^ msg.Text == "/ring") 
            {
                await bot.SendMessage(msg.Chat, $"🎁 <a href=\"tg://user?id={msg.From.Id}\">{userName}</a>, напишите письмо Санте и не забывайте вести себя хорошо 🤫.",
                    parseMode : ParseMode.Html);  
                    await bot.DeleteMessage(msg.Chat.Id, msg.MessageId);         
            }
            else
            {
                System.Console.WriteLine("User not playing");
            }
        }
    }
}

