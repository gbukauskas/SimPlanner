﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP.Web.UserEmails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class ForgotPasswordTemplate : SP.Web.UserEmails.EmailBase
    {
#line hidden
        #line 3 "..\..\UserEmails\ForgotPasswordTemplate.cshtml"
            
    public string Token { get; set; }
    public Guid UserId { get; set; }

    public string AsQueryString()
    {
        return BaseUrl + "index.html#/resetPassword?token=" + System.Web.HttpUtility.UrlEncode(Token) + "&userId=" + UserId.ToString();
    }

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n");

            
            #line 13 "..\..\UserEmails\ForgotPasswordTemplate.cshtml"
   Layout = new SP.Web.UserEmails.LayoutTemplate { Title = "Forgot Password" }; 
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<table");

WriteLiteral(" class=\"row\"");

WriteLiteral(" style=\"border-collapse: collapse; border-spacing: 0; padding: 0; position: relat" +
"ive; text-align: left; vertical-align: top; width: 100%;\"");

WriteLiteral("><tbody><tr");

WriteLiteral(" style=\"padding: 0; text-align: left; vertical-align: top;\"");

WriteLiteral(">\r\n    <th");

WriteLiteral(" class=\"small-12 large-12 columns first last\"");

WriteLiteral(@" style=""Margin: 0 auto; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; line-height: 1.3; margin: 0 auto; padding: 0; padding-bottom: 16px; padding-left: 16px; padding-right: 16px; text-align: left; width: 564px;""");

WriteLiteral("><table");

WriteLiteral(" style=\"border-collapse: collapse; border-spacing: 0; padding: 0; text-align: lef" +
"t; vertical-align: top; width: 100%;\"");

WriteLiteral("><tr");

WriteLiteral(" style=\"padding: 0; text-align: left; vertical-align: top;\"");

WriteLiteral("><th");

WriteLiteral(" style=\"Margin: 0; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; fon" +
"t-size: 16px; font-weight: normal; line-height: 1.3; margin: 0; padding: 0; text" +
"-align: left;\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" class=\"spacer\"");

WriteLiteral(" style=\"border-collapse: collapse; border-spacing: 0; padding: 0; text-align: lef" +
"t; vertical-align: top; width: 100%;\"");

WriteLiteral("><tbody><tr");

WriteLiteral(" style=\"padding: 0; text-align: left; vertical-align: top;\"");

WriteLiteral("><td");

WriteLiteral(" height=\"16px\"");

WriteLiteral(@" style=""-moz-hyphens: auto; -webkit-hyphens: auto; Margin: 0; border-collapse: collapse !important; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; hyphens: auto; line-height: 16px; margin: 0; mso-line-height-rule: exactly; padding: 0; text-align: left; vertical-align: top; word-wrap: break-word;""");

WriteLiteral(">&#xA0;</td></tr></tbody></table> \r\n\r\n        <h1");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"Margin: 0; Margin-bottom: 10px; color: inherit; font-family: Helvetica, A" +
"rial, sans-serif; font-size: 34px; font-weight: normal; line-height: 1.3; margin" +
": 0; margin-bottom: 10px; padding: 0; text-align: center; word-wrap: normal;\"");

WriteLiteral(">Forgot Your Password?</h1>\r\n\r\n        <table");

WriteLiteral(" class=\"spacer\"");

WriteLiteral(" style=\"border-collapse: collapse; border-spacing: 0; padding: 0; text-align: lef" +
"t; vertical-align: top; width: 100%;\"");

WriteLiteral("><tbody><tr");

WriteLiteral(" style=\"padding: 0; text-align: left; vertical-align: top;\"");

WriteLiteral("><td");

WriteLiteral(" height=\"16px\"");

WriteLiteral(@" style=""-moz-hyphens: auto; -webkit-hyphens: auto; Margin: 0; border-collapse: collapse !important; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; hyphens: auto; line-height: 16px; margin: 0; mso-line-height-rule: exactly; padding: 0; text-align: left; vertical-align: top; word-wrap: break-word;""");

WriteLiteral(">&#xA0;</td></tr></tbody></table> \r\n\r\n        <p");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"Margin: 0; Margin-bottom: 10px; color: #0a0a0a; font-family: Helvetica, A" +
"rial, sans-serif; font-size: 16px; font-weight: normal; line-height: 1.3; margin" +
": 0; margin-bottom: 10px; padding: 0; text-align: center;\"");

WriteLiteral(">Click the link below to reset your password.</p>\r\n        <table");

WriteLiteral(" class=\"button large expand\"");

WriteLiteral(" style=\"Margin: 0 0 16px 0; border-collapse: collapse; border-spacing: 0; margin:" +
" 0 0 16px 0; padding: 0; text-align: left; vertical-align: top; width: 100% !imp" +
"ortant;\"");

WriteLiteral("><tr");

WriteLiteral(" style=\"padding: 0; text-align: left; vertical-align: top;\"");

WriteLiteral("><td");

WriteLiteral(@" style=""-moz-hyphens: auto; -webkit-hyphens: auto; Margin: 0; border-collapse: collapse !important; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; hyphens: auto; line-height: 1.3; margin: 0; padding: 0; text-align: left; vertical-align: top; word-wrap: break-word;""");

WriteLiteral("><table");

WriteLiteral(" style=\"border-collapse: collapse; border-spacing: 0; padding: 0; text-align: lef" +
"t; vertical-align: top; width: 100%;\"");

WriteLiteral("><tr");

WriteLiteral(" style=\"padding: 0; text-align: left; vertical-align: top;\"");

WriteLiteral("><td");

WriteLiteral(@" style=""-moz-hyphens: auto; -webkit-hyphens: auto; Margin: 0; background: #2199e8; border: 2px solid #2199e8; border-collapse: collapse !important; color: #fefefe; font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; hyphens: auto; line-height: 1.3; margin: 0; padding: 0; text-align: left; vertical-align: top; word-wrap: break-word;""");

WriteLiteral("><center");

WriteLiteral(" data-parsed=\"\"");

WriteLiteral(" style=\"min-width: 0; width: 100%;\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 4463), Tuple.Create("\"", 4488)
            
            #line 24 "..\..\UserEmails\ForgotPasswordTemplate.cshtml"
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 , Tuple.Create(Tuple.Create("", 4470), Tuple.Create<System.Object, System.Int32>(AsQueryString()
            
            #line default
            #line hidden
, 4470), false)
);

WriteLiteral(" align=\"center\"");

WriteLiteral(" class=\"float-center\"");

WriteLiteral(@" style=""Margin: 0; border: 0 solid #2199e8; border-radius: 3px; color: #fefefe; display: inline-block; font-family: Helvetica, Arial, sans-serif; font-size: 20px; font-weight: bold; line-height: 1.3; margin: 0; padding: 10px 20px 10px 20px; padding-left: 0; padding-right: 0; text-align: center; text-decoration: none; width: 100%;""");

WriteLiteral(">Reset Password</a></center></td></tr></table></td>\n<td");

WriteLiteral(" class=\"expander\"");

WriteLiteral(@" style=""-moz-hyphens: auto; -webkit-hyphens: auto; Margin: 0; border-collapse: collapse !important; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: normal; hyphens: auto; line-height: 1.3; margin: 0; padding: 0 !important; text-align: left; vertical-align: top; visibility: hidden; width: 0; word-wrap: break-word;""");

WriteLiteral("></td></tr></table>\r\n\r\n    </th>\n<th");

WriteLiteral(" class=\"expander\"");

WriteLiteral(" style=\"Margin: 0; color: #0a0a0a; font-family: Helvetica, Arial, sans-serif; fon" +
"t-size: 16px; font-weight: normal; line-height: 1.3; margin: 0; padding: 0 !impo" +
"rtant; text-align: left; visibility: hidden; width: 0;\"");

WriteLiteral("></th></tr></table></th>\r\n</tr></tbody></table>\r\n");

        }
    }
}
#pragma warning restore 1591