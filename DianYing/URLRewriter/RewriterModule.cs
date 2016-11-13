using System;
using System.Text.RegularExpressions;
using System.Configuration;
using URLRewriter.Config;
using URLRewriter;

namespace URLRewriter
{
	/// <summary>
	/// Provides a rewriting HttpModule.
	/// </summary>
	public class RewriterModule : BaseRewriterModule
	{
		/// <summary>
		/// This method is called during the module's BeginRequest event.
		/// </summary>
        /// <param name="requestedPath">The RawUrl being requested (includes path and querystring).</param>
		/// <param name="app">The HttpApplication instance.</param>
		protected override void Rewrite(string requestedPath, System.Web.HttpApplication app)
		{
			// log information to the Trace object.
			app.Context.Trace.Write("RewriterModule", "Entering RewriterModule");

			// get the configuration rules
			RewriterRuleCollection rules = RewriterConfiguration.GetConfig().Rules;

			// iterate through each rule...
			for(int i = 0; i < rules.Count; i++)
			{
				// get the pattern to look for, and Resolve the Url (convert ~ into the appropriate directory)
                //带域名 2011.5.18
                //string lookFor = "^" + rules[i].LookFor + "$";
                //无域名
				string lookFor = "^" + RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, rules[i].LookFor) + "$";
				// Create a regex (note that IgnoreCase is set...)
				Regex re = new Regex(lookFor, RegexOptions.IgnoreCase);
                //app.Context.Response.Write(lookFor);
                //app.Context.Response.Write("<br />");
				// See if a match is found
				if (re.IsMatch(requestedPath))
				{
					// match found - do any replacement needed
					string sendToUrl = RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, re.Replace(requestedPath, rules[i].SendTo));

					// log rewriting information to the Trace object
					app.Context.Trace.Write("RewriterModule", "Rewriting URL to " + sendToUrl);

					// Rewrite the URL
					RewriterUtils.RewriteUrl(app.Context, sendToUrl);
                    //app.Context.Response.Write(sendToUrl);
                    //app.Context.Response.Write("<br />");
					break;		// exit the for loop
				}
			}
            //app.Context.Response.Write(requestedPath);
            //app.Context.Response.End();
			// Log information to the Trace object
			app.Context.Trace.Write("RewriterModule", "Exiting RewriterModule");
		}
	}
}
