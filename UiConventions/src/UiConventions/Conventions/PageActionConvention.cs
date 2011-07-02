namespace HtmlTags.UI.Conventions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using Attributes;
	using Extensions;
	using Extensions.Mvc;
	using FubuCore.Reflection;
	using Helpers;

	public class PageActionConvention : IPageActionConvention
	{
		public static IPageActionsSecurity Security { get; set; }

		public static string PageActionsButtonClass = "pageAction";
		public static string CloseWindowAttribute = "CloseWindow";
		public static string OpenNewWindow = "OpenNewWindow";

		public HtmlTag ButtonsFor<TController>(HtmlHelper helper,
		                                       params Expression<Action<TController>>[] expressions)
			where TController : IController
		{
			var actions = expressions.Select(e => new FindMethodVisitor(e).Method);
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			var buttons = Tags.Div
				.Nest(
					actions
						.Where(action => Security.UserCanAccess(action))
						.SelectMany(action => CreateButtonAndJavascript(action, urlHelper))
						.ToArray()
				);

			return buttons;
		}

		private static IEnumerable<HtmlTag> CreateButtonAndJavascript(MethodInfo action, UrlHelper urlHelper)
		{
			var name = action.Name;
			var controller = action.DeclaringType.Name.Replace("Controller", string.Empty);
			var actionUrl = urlHelper.Action(name, controller);

			var button = ViewConventionExtensions.Button(name).AddClass(PageActionsButtonClass);
			ApplyOpenWindowSettings(action, button);
			ApplyCloseWindowSettings(action, button);

			var javascript = GetJavascriptForButton(action, name, actionUrl);

			return new[] {button, javascript};
		}

		public static HtmlTag GetJavascriptForButton(MethodInfo action, string name, string actionUrl)
		{
			if (IsAction(action))
			{
				return PageActions.ButtonAction(name, actionUrl);
			}
			if (IsCommand(action))
			{
				return PageActions.ButtonCommandForSelected(name, actionUrl);
			}
			return PageActions.ButtonActionForSelected(name, actionUrl);
		}

		public HtmlTag MenuItemFor<TController>(UrlHelper helper, Expression<Action<TController>> action,
		                                        MethodInfo currentAction, object routeValues = null)
		{
			var actionMethod = helper.GetActionMethod(action);
			if (!Security.UserCanAccess(actionMethod))
			{
				return Tags.Empty;
			}
			return BuildMenuItemFor(helper, action, actionMethod == currentAction, routeValues);
		}

		public HtmlTag MenuItemFor(string url, string text, bool selected)
		{
			var menuItem = Tags.ListItem;
			if (selected)
			{
				menuItem.Selected();
			}
			return menuItem.Nest(
				Tags.Link
					.Href(url)
					.Text(text)
				);
		}

		private HtmlTag BuildMenuItemFor<TController>(UrlHelper helper, Expression<Action<TController>> action,
		                                              bool selected, object routeValues)
		{
			var text = helper.GetControllerName<TController>();
			return MenuItemFor(helper.GetUrl(action, routeValues), text, selected);
		}

		private static void ApplyOpenWindowSettings(MethodInfo action, HtmlTag button)
		{
			if(ShouldOpenNewWindow(action))
			{
				button.Attr(OpenNewWindow, "true");
			}
		}

		private static bool ShouldOpenNewWindow(MethodInfo action)
		{
			return action.HasAttribute<NewWindowAttribute>();
		}

		private static void ApplyCloseWindowSettings(MethodInfo action, HtmlTag button)
		{
			CloseWindowAttribute attribute;
			if(ShouldCloseWindow(action, out attribute))
			{
				button.Attr(CloseWindowAttribute, attribute.MillisecondsToClose);
			}
		}

		private static bool ShouldCloseWindow(MethodInfo action, out CloseWindowAttribute attribute)
		{
			attribute = action.GetAttribute<CloseWindowAttribute>();
			return attribute != null;
		}

		private static bool IsCommand(MethodInfo action)
		{
			return action.ReturnType == typeof (CommandResult);
		}

		private static bool IsAction(MethodInfo action)
		{
			return action.GetParameters().Length == 0 || action.HasAttribute<ButtonActionAttribute>();
		}
	}
}