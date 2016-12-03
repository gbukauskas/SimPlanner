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

namespace SP.Web.UserEmails.Inky
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class LayoutTemplate : SP.Web.UserEmails.EmailBase
    {
#line hidden
        #line 4 "..\..\UserEmails\Inky\LayoutTemplate.cshtml"
            
    public string Title{ get; set; }
    public string Description {get; set;}

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"\r\n          \"http" +
"://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n\r\n<html");

WriteLiteral(" xmlns=\"http://www.w3.org/1999/xhtml\"");

WriteLiteral(" lang=\"en\"");

WriteLiteral(" xml:lang=\"en\"");

WriteLiteral(">\r\n<head>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" href=\"emailStyles.css\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" http-equiv=\"Content-Type\"");

WriteLiteral(" content=\"text/html; charset=utf-8\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width\"");

WriteLiteral(" />\r\n    <title>");

            
            #line 17 "..\..\UserEmails\Inky\LayoutTemplate.cshtml"
      Write(Title);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <!-- <style> -->\r\n</head>\r\n<body>\r\n    <span");

WriteLiteral(" class=\"preheader\"");

WriteLiteral(">");

            
            #line 21 "..\..\UserEmails\Inky\LayoutTemplate.cshtml"
                       Write(Description);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n    <table");

WriteLiteral(" class=\"body\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" class=\"center\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(" valign=\"top\"");

WriteLiteral(">\r\n                <center>\r\n                    <wrapper");

WriteLiteral(" class=\"header\"");

WriteLiteral(">\r\n                        <row");

WriteLiteral(" class=\"collapse\"");

WriteLiteral(">\r\n                            <columns");

WriteLiteral(" small=\"2\"");

WriteLiteral(">\r\n                                <img");

WriteAttribute("src", Tuple.Create(" src=\"", 1072), Tuple.Create("\"", 1125)
            
            #line 29 "..\..\UserEmails\Inky\LayoutTemplate.cshtml"
, Tuple.Create(Tuple.Create("", 1078), Tuple.Create<System.Object, System.Int32>(BaseImageUrl
            
            #line default
            #line hidden
, 1078), false)
, Tuple.Create(Tuple.Create("", 1091), Tuple.Create("/images/sim-planner-icon-50x75.png", 1091), true)
);

WriteLiteral(" alt=\"sim-manager logo\"");

WriteLiteral(" height=\"50\"");

WriteLiteral(" width=\"75\"");

WriteLiteral(" />\r\n                            </columns>\r\n                            <columns" +
"");

WriteLiteral(" small=\"8\"");

WriteLiteral(">\r\n                                <h1");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">Sim-planner</h1>\r\n                            </columns>\r\n                      " +
"      <columns");

WriteLiteral(" small=\"2\"");

WriteLiteral(">\r\n                                <img");

WriteLiteral(" class=\"float-right\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1485), Tuple.Create("\"", 1537)
            
            #line 35 "..\..\UserEmails\Inky\LayoutTemplate.cshtml"
, Tuple.Create(Tuple.Create("", 1491), Tuple.Create<System.Object, System.Int32>(BaseImageUrl
            
            #line default
            #line hidden
, 1491), false)
, Tuple.Create(Tuple.Create("", 1504), Tuple.Create("/Content/images/logo-starship.png", 1504), true)
);

WriteLiteral(" alt=\"Starship logo\"");

WriteLiteral(" height=\"50\"");

WriteLiteral(" width=\"93\"");

WriteLiteral(" />\r\n                            </columns>\r\n                        </row>\r\n    " +
"                </wrapper>\r\n\r\n                    <container>\r\n                 " +
"       <spacer");

WriteLiteral(" size=\"16\"");

WriteLiteral("></spacer>\r\n\r\n");

WriteLiteral("                        ");

            
            #line 43 "..\..\UserEmails\Inky\LayoutTemplate.cshtml"
                   Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                        <wrapper");

WriteLiteral(" class=\"footer\"");

WriteLiteral(">\r\n                            <spacer");

WriteLiteral(" size=\"16\"");

WriteLiteral("></spacer>\r\n                            <row");

WriteLiteral(" class=\"collapse\"");

WriteLiteral(">\r\n                                <columns");

WriteLiteral(" large=\"6\"");

WriteLiteral(">\r\n                                    <h5>Sim-planner contact info:</h5>\r\n      " +
"                              <p>Phone: +64 21 715 541</p>\r\n                    " +
"                <p>Email: <a");

WriteLiteral(" href=\"mailto:info@sim-planner.com\"");

WriteLiteral(">info@sim-planner.com</a></p>\r\n                                </columns>\r\n      " +
"                          <columns");

WriteLiteral(" large=\"6\"");

WriteLiteral(">\r\n                                    <h5>\r\n                                    " +
"    Connect With Us:\r\n                                    </h5>\r\n               " +
"                     <row>\r\n                                        <columns");

WriteLiteral(" small=\"5\"");

WriteLiteral(">\r\n                                            <button");

WriteLiteral(" class=\"facebook tiny expand\"");

WriteLiteral(" href=\"http://sim-planner.com\"");

WriteLiteral(">Facebook</button>\r\n                                        </columns>\r\n         " +
"                               <columns");

WriteLiteral(" small=\"5\"");

WriteLiteral(">\r\n                                            <button");

WriteLiteral(" class=\"twitter tiny expand\"");

WriteLiteral(" href=\"http://sim-planner.com\"");

WriteLiteral(">Twitter</button>\r\n                                        </columns>\r\n          " +
"                          </row>\r\n                                    <row>\r\n   " +
"                                     <columns");

WriteLiteral(" small=\"5\"");

WriteLiteral(">\r\n                                            <button");

WriteLiteral(" class=\"google tiny expand\"");

WriteLiteral(" href=\"http://sim-planner.com\"");

WriteLiteral(">Google+</button>\r\n                                        </columns>\r\n          " +
"                              <columns");

WriteLiteral(" small=\"5\"");

WriteLiteral(">\r\n                                            <button");

WriteLiteral(" class=\"linkedIn tiny expand\"");

WriteLiteral(" href=\"http://sim-planner.com\"");

WriteLiteral(@">LinkedIn</button>
                                        </columns>
                                    </row>
                                </columns>
                            </row>
                        </wrapper>
                    </container>
                </center>
            </td>
        </tr>
    </table>
    <!-- prevent Gmail on iOS font size manipulation -->
    <div");

WriteLiteral(" style=\"display:none; white-space:nowrap; font:15px courier; line-height:0;\"");

WriteLiteral("> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &n" +
"bsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp" +
"; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </div>\r\n</body>\r\n</html>");

        }
    }
}
#pragma warning restore 1591
