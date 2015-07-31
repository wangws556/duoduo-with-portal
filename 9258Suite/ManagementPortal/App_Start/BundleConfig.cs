using System.Web;
using System.Web.Optimization;

namespace YoYoStudio.ManagementPortal
{
	public class BundleConfig
	{
		public const string JQueryBundle = "~/bundles/jquery";
		public const string JQueryUIBundle = "~/bundles/jqueryui";
		public const string JQueryValidationBundle = "~/bundles/jqueryval";
		public const string ModernizrBundle = "~/bundles/modernizr";
		public const string SiteCssBundle = "~/Content/css";
		

		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle(JQueryBundle).Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle(JQueryUIBundle).Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle(JQueryValidationBundle).Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle(ModernizrBundle).Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle(SiteCssBundle).Include("~/Content/site.css"));			

			
		}
	}
}