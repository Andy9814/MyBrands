﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyBrands.Utils;
using MyBrands.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace MyBrands.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = BrandIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private const string BrandIdAttribute = "brand";
        [HtmlAttributeName(BrandIdAttribute)]
        public string BrandId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(_session.Get<ProductViewModel[]>("productSession") != null && Convert.ToInt32(BrandId) > 0)
            {
                var innerHtml = new StringBuilder();
                ProductViewModel[] prod = _session.Get<ProductViewModel[]>("productSession");
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:x-large;\"><span>Catalogue</span></div>");

                foreach (ProductViewModel item in prod)
                {
                    if (item.BrandId == Convert.ToInt32(BrandId))
                    {
                        item.Description = item.Description.Replace("'", "&#39;");
                        item.JsonData = JsonConvert.SerializeObject(item);
                        innerHtml.Append("<div class=\"col-sm-3 col-xs-12 text-center\" style=\"border:solid;\">");
                        innerHtml.Append("<span class=\"col-xs-12\"><img width=\"120\" height=\"150\"  padding=\"10%\" src=\"");
                        innerHtml.Append(item.GraphicName);
                        innerHtml.Append("\" /></span>");
                        innerHtml.Append("<p><span style=\"font-size:large;\">" + item.ProductName + "...</span></p><div>");
                        innerHtml.Append("<span>Click for MoreInfo.<br />Details</span></div>");
                        innerHtml.Append("<div style=\"padding-bottom: 10px;\"><a href=\"#details_popup\" data-toggle=\"modal\" class=\"btn btn-default\"");
                        innerHtml.Append(" id=\"modalbtn" + item.Id + "\" data-id=\"" + item.Id + "\" data-details='" + item.JsonData +
                        "'>Details</a></div></div>");

                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }




        }
    }
}
