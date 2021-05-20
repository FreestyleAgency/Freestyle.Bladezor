using Freestyle.Bladezor.Client.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Freestyle.Bladezor.Client.Models
{
	public class BladeContext
	{
		private string _title;
		private string _subtitle;
		private ElementReference _ref;
		private string _cssClass;
		private object _returnValue;

		public event EventHandler<ValueEventArgs<object>> ReturnValueChanged;
		public event EventHandler<ValueEventArgs<string>> TitleChanged;
		public event EventHandler<ValueEventArgs<string>> SubtitleChanged;
		public event EventHandler<ValueEventArgs<ElementReference>> RefChanged;
		public event EventHandler<ValueEventArgs<string>> CssClassChanged;

		public event EventHandler Closed;

		public string Url { get; set; }
		public string Title { 
			get { return _title; }
			set
			{
				if (_title == value) return;

				_title = value;

				TitleChanged?.Invoke(this, new ValueEventArgs<string>(value));
			}
		}

		public string Subtitle
		{
			get { return _subtitle; }
			set
			{
				if (_subtitle == value) return;

				_subtitle = value;

				SubtitleChanged?.Invoke(this, new ValueEventArgs<string>(value));
			}
		}

		public RenderFragment RenderFragment { get; set; }

		public ElementReference Ref {
			get { return _ref; }
			set
			{
				_ref = value;

				RefChanged?.Invoke(this, new ValueEventArgs<ElementReference>(_ref));
			}
		}

		public BladeBase BladeComponent { get; set; }
		public Type BladeType { get; set; }

		public string CssClass { 
			get { return _cssClass; }
			set
			{
				if (_cssClass == value) return;

				_cssClass = value;

				CssClassChanged?.Invoke(this, new ValueEventArgs<string>(value));
			}
		}

		public void OnClosed()
		{
			this.Closed?.Invoke(this, EventArgs.Empty);
		}

		public object ReturnValue
		{
			get { return _returnValue; }
			internal set
			{
				_returnValue = value;

				ReturnValueChanged?.Invoke(this, new ValueEventArgs<object>(value));
			}
		}
	}
}
