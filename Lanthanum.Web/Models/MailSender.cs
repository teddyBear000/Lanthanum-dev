using System;
using System.IO;
using Lanthanum.Web.Domain;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Lanthanum.Web.Models
{
    public enum SendForm {AuthorSubsription, TeamSubscription, SportSubscription}

    public class MailSender
    {
        private readonly string WelcomeHtml;
        private readonly string AuthorSubHtml;
        private readonly string TeamSubHtml;
        private readonly string SportSubHtml;
        private readonly string ReplySubHtml;
        private readonly string BanHtml;
        private readonly string apiKey;
        private readonly EmailAddress emailSender;

        public MailSender()
        {
            using (StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\WelcomeHtml.txt"))
            {
                WelcomeHtml = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\AuthorSubHtml.txt"))
            {
                AuthorSubHtml = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\TeamSubHtml.txt"))
            {
                TeamSubHtml = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\SportSubHtml.txt"))
            {
                SportSubHtml = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\ReplySubHtml.txt"))
            {
                ReplySubHtml = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\BanHtml.txt"))
            {
                BanHtml = sr.ReadToEnd();
            }

            apiKey = WebApiOptions.ApiKey;
            emailSender = new EmailAddress("noreplysport.1@gmail.com", "Sport Site");
        }

        public async void SendWelcome(string clientEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = emailSender,
                Subject = "Welcome to the Sports Hub",
                HtmlContent = WelcomeHtml.Replace("INPUT-DATE", GetDate())
            };

            msg.AddTo(new EmailAddress(clientEmail, "Dear User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async void SendBan(string clientEmail, string adminName, string banReason)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = emailSender,
                Subject = "Sports Hub Notification",
                HtmlContent = BanHtml.Replace("INPUT-ADMIN", adminName).Replace("INPUT-REASON", banReason)
            };

            msg.AddTo(new EmailAddress(clientEmail, "Dear User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async void SendComment(string clientEmail, Comment comment)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = emailSender,
                Subject = "Sports Hub Notification",
                HtmlContent = ReplySubHtml.Replace("INPUT-COMMENT", GetCommentHtml(comment))
            };

            msg.AddTo(new EmailAddress(clientEmail, "Dear User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async void SendSubscription(string clientEmail, SendForm format, string objectName, Article article)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            if (format == SendForm.AuthorSubsription)
            {
                msg = new SendGridMessage()
                {
                    From = emailSender,
                    Subject = "Sports Hub Notification",
                    HtmlContent = AuthorSubHtml.Replace("INPUT-AUTHOR", objectName).Replace("INPUT-ARTICLE", GetArticleHtml(article))
                };
            }
            else if (format == SendForm.TeamSubscription)
            {
                msg = new SendGridMessage()
                {
                    From = emailSender,
                    Subject = "Sports Hub Notification",
                    HtmlContent = TeamSubHtml.Replace("INPUT-TEAM", objectName).Replace("INPUT-ARTICLE", GetArticleHtml(article))
                };
            }
            else if (format == SendForm.SportSubscription)
            {
                msg = new SendGridMessage()
                {
                    From = emailSender,
                    Subject = "Sports Hub Notification",
                    HtmlContent = SportSubHtml.Replace("INPUT-SPORT", objectName).Replace("INPUT-ARTICLE", GetArticleHtml(article))
                };
            }

            msg.AddTo(new EmailAddress(clientEmail, "Dear User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        private string GetArticleHtml(Article article)
        {
            StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\ArticleHtml.txt");
            var ArticleHtml = sr.ReadToEnd().Replace("INSERT-DATE", article.DateTimeOfCreation.ToString("dd.MM.yyyy"));
            return ArticleHtml.Replace("INSERT-HEAD", article.Headline).Replace("INSERT-HEADER", article.Header);
        }

        private string GetCommentHtml(Comment comment)
        {
            StreamReader sr = new StreamReader("..\\Lanthanum.Web\\wwwroot\\txt\\CommentHtml.txt");
            var CommentHtml = sr.ReadToEnd(); // TODO: Add html form
            return CommentHtml;
        }

        private string GetDate()
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            var date = DateTime.Now;

            return months[date.Month] + " " + date.Day + ", " + date.Year;
        }
    }
}
