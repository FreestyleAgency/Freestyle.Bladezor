using Freestyle.Bladezor.Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Components
{
	public abstract class BladeBase : ComponentBase, IDisposable
	{
		private BladeContext _bladeContext;
		private string _title;
		private string _subtitle;
		private object _returnValue;

		protected virtual object ReturnValue
		{
			get { return _returnValue; }
			set
			{
				if (BladeContext == null)
					throw new InvalidOperationException("BladeContext is null. ReturnValue can only be set once BladeContext has been set. Set Subtitle in OnParametersSet() or later.");

				_returnValue = value;

				BladeContext.ReturnValue = value;
			}
		}

		[CascadingParameter(Name = "BladeContext")]
		public BladeContext BladeContext
		{
			get { return _bladeContext; }
			set
			{
				_bladeContext = value;

				_title = _bladeContext?.Title;
				_subtitle = _bladeContext?.Subtitle;

				StateHasChanged();
			}
		}

		[CascadingParameter(Name = "BladeStack")]
		public IBladeStack BladeStack { get; set; }

		[CascadingParameter(Name = "StackLayout")]
		public IStackLayout StackLayout { get; set; }

		[CascadingParameter(Name = "Router")]
		public Microsoft.AspNetCore.Components.Routing.Router Router { get; set; }

		[Parameter]
		public virtual string Title
		{
			get { return _title; } 
			set
			{
				if (BladeContext == null)
					throw new InvalidOperationException("BladeContext is null. Title can only be set once BladeContext has been set. Set Title in OnParametersSet() or later.");

				_title = value;
			
				BladeContext.Title = Title;

				if (TitleChanged.HasDelegate)
					TitleChanged.InvokeAsync(value);
			}
		}

		[Parameter]
		public EventCallback<string> TitleChanged { get; set; }

		[Parameter]
		public virtual string Subtitle
		{
			get { return _subtitle; }
			set
			{
				if (BladeContext == null)
					throw new InvalidOperationException("BladeContext is null. Subtitle can only be set once BladeContext has been set. Set Subtitle in OnParametersSet() or later.");

				_subtitle = value;

				BladeContext.Subtitle = Subtitle;

				if (SubtitleChanged.HasDelegate)
					SubtitleChanged.InvokeAsync(value);
			}
		}

		[Parameter]
		public EventCallback<string> SubtitleChanged { get; set; }

		public virtual async Task Close()
		{
			await BladeStack.PopPast(BladeContext);
		}

		public virtual bool ShouldConfirmClose
		{
			get
			{
				return false;
			}
		}

		protected override void OnParametersSet()
		{
			if (BladeContext != null)
				BladeContext.BladeComponent = this;

			base.OnParametersSet();
		}

		public virtual void Dispose()
		{
			if (BladeContext != null)
			{
				BladeContext.BladeComponent = null;

				this.BladeContext = null;
			}
		}
	}
}
