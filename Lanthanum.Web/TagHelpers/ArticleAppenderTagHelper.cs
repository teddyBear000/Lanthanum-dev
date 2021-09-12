using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lanthanum.Web.TagHelpers
{
    public class ArticleAppenderTagHelper : TagHelper
    {
        public IEnumerable<ArticleViewModel> ArticleViewModels { get; set; }
        private readonly string _defaultArticlePreviewClass = "article-preview my-2";
        private readonly string _defaultTextContainerClass = "text-container";
        private readonly string _defaultDropdownClass = "dropdown";
        private readonly string _defaultHeadlineClass = "headline";
        private readonly string _defaultArticleBriefStartClass = "article-brief-start my-2";
        private readonly string _defaultLocationClass = "location-status-container d-flex justify-content-between";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
             foreach (var model in ArticleViewModels)
            {
                var generalDiv = await ArticlePreviewAsync();

                var generalAnchor = await RedirectToArticleAsync();
                var articleLogoImg = await ArticleLogoAsync(model);
                
                generalDiv.InnerHtml.AppendHtml(generalAnchor);
                generalDiv.InnerHtml.AppendHtml(articleLogoImg);

                var textContainerDiv = await TextContainerAsync();

                var headlineDiv = await HeadlineAsync(model);
                textContainerDiv.InnerHtml.AppendHtml(headlineDiv);

                var articleBriefStartDiv = await ArticleBriefStartAsync(model);
                textContainerDiv.InnerHtml.AppendHtml(articleBriefStartDiv);

                var locationStateDiv = await LocationStateAsync();

                var locationSpan = await LocationAsync(model);
                var statusDiv = await StatusAsync(model);
                locationStateDiv.InnerHtml.AppendHtml(locationSpan);
                locationStateDiv.InnerHtml.AppendHtml(statusDiv);
                textContainerDiv.InnerHtml.AppendHtml(locationStateDiv);

                generalDiv.InnerHtml.AppendHtml(textContainerDiv);
                output.Content.AppendHtml(generalDiv);
            }
        }

        private async Task<TagBuilder> ArticlePreviewAsync()
        {
            //Article Preview Container
            TagBuilder generalDiv = new TagBuilder("div");
            await Task.Run(() => generalDiv.Attributes.Add("class", _defaultArticlePreviewClass));

            return generalDiv;
        }

        private async Task<TagBuilder> RedirectToArticleAsync()
        {
            //Anchor tag - responsible for redirecting to article page
            TagBuilder redirectToArticleAnchor = new TagBuilder("a");
            await Task.Run(() =>
            {
                redirectToArticleAnchor.Attributes.Add("class", "main-link");
                redirectToArticleAnchor.Attributes.Add("href", "#");
            });
            return redirectToArticleAnchor;
        }

        private async Task<TagBuilder> ArticleLogoAsync(ArticleViewModel model)
        {
            //ArticleLogo
            TagBuilder articleLogoImg = new TagBuilder("img");
            await Task.Run(() =>
            {
                articleLogoImg.Attributes.Add("alt", model.Alt);
                articleLogoImg.Attributes.Add("src", model.LogoPath);
            });

            return articleLogoImg;
        }

        //private async Task<TagBuilder> DropdownAsync(ArticleViewModel model)
        //{
        //    //Dropdown
        //    TagBuilder dropdownDiv = new TagBuilder("div");
        //    dropdownDiv.Attributes.Add("class",_defaultDropdownClass);
        //    TagBuilder dotsAnchor = new TagBuilder("a");
        //    dotsAnchor.Attributes.Add("type","submit");
        //    await Task.Run(() =>
        //    {
        //        articleLogoImg.Attributes.Add("alt", model.Alt);
        //        articleLogoImg.Attributes.Add("src", model.LogoPath);
        //    });

        //    return articleLogoImg;
        //}

        private async Task<TagBuilder> TextContainerAsync()
        {
            //Container of all text inside article-preview
            TagBuilder textContainerDiv = new TagBuilder("div");

            await Task.Run(() =>
            {
                textContainerDiv.Attributes.Add("class", _defaultTextContainerClass);
            });
            
            return textContainerDiv;
        }

        private async Task<TagBuilder> HeadlineAsync(ArticleViewModel model)
        {
            //Headline
            TagBuilder headlineDiv = new TagBuilder("div");

            await Task.Run(() =>
            {
                headlineDiv.Attributes.Add("class", _defaultHeadlineClass);
                headlineDiv.InnerHtml.SetContent(model.Headline);
            });
            
            return headlineDiv;
        }

        private async Task<TagBuilder> ArticleBriefStartAsync(ArticleViewModel model)
        {
            //First sentences of article
            TagBuilder articleBriefStartDiv = new TagBuilder("div");

            await Task.Run(() =>
            {
                articleBriefStartDiv.Attributes.Add("class", _defaultArticleBriefStartClass);
                articleBriefStartDiv.InnerHtml.SetContent(model.MainText);
            });

            return articleBriefStartDiv;
        }

        private async Task<TagBuilder> LocationStateAsync()
        {
            //Container for location of the teams and status of article[published,unpublished]
            TagBuilder locationStateDiv = new TagBuilder("div");

            await Task.Run(() =>
            {
                locationStateDiv.Attributes.Add("class", _defaultLocationClass);
            });

            return locationStateDiv;
        }

        private async Task<TagBuilder> LocationAsync(ArticleViewModel model)
        {
            //Location
            TagBuilder locationSpan = new TagBuilder("span");

            await Task.Run(() =>
            {
                locationSpan.Attributes.Add("class", "location");
                locationSpan.InnerHtml.SetContent($"{model.TeamConference} / {model.TeamName}");
            });
            
            return locationSpan;
        }

        private async Task<TagBuilder> StatusAsync(ArticleViewModel model)
        {
            //Status
            TagBuilder statusDiv = new TagBuilder("div");

            TagBuilder statusImg = new TagBuilder("img");
            TagBuilder statusText = new TagBuilder("p");

            if (model.ArticleStatus == ArticleStatus.Published)
            {
                await Task.Run(() =>
                {
                    statusImg.Attributes.Add("src", "/images/green_circle.png");
                    statusText.InnerHtml.SetContent("Published");
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    statusImg.Attributes.Add("src", "/images/yellow_circle.png");
                    statusText.InnerHtml.SetContent("Unpublished");
                });
            }

            await Task.Run(() =>
            {
                statusDiv.Attributes.Add("class", "status");
                statusImg.Attributes.Add("class", "circle");
                statusText.Attributes.Add("class", "d-block m-0");
                statusDiv.InnerHtml.AppendHtml(statusImg);
                statusDiv.InnerHtml.AppendHtml(statusText);
            });

            return statusDiv;
        }
    }
}
