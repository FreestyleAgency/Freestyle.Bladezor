using Freestyle.Bladezor.Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Bladezor.Client.Components
{
	public abstract class ModalBase : ComponentBase, IDisposable
	{
		private ModalContext _modalContext;
		private string _title;
		private string _cssClass;
		private object _returnValue;

		protected virtual object ReturnValue
		{
			get { return _returnValue; }
			set
			{
				if (ModalContext == null)
					throw new InvalidOperationException("ModalContext is null. ReturnValue can only be set once ModalContext has been set. Set Subtitle in OnParametersSet() or later.");

				_returnValue = value;

				ModalContext.ReturnValue = value;
			}
		}

		[CascadingParameter(Name = "ModalContext")]
		public ModalContext ModalContext
		{
			get { return _modalContext; }
			set
			{
				_modalContext = value;

				_title = _modalContext?.Title;

				StateHasChanged();
			}
		}

		[CascadingParameter(Name = "ModalStack")]
		public IModalStack ModalStack { get; set; }

		[CascadingParameter(Name = "StackLayout")]
		public IStackLayout StackLayout { get; set; }

		[Parameter]
		public virtual string Title
		{
			get { return _title; } 
			set
			{
				if (ModalContext == null)
					throw new InvalidOperationException("ModalContext is null. Title can only be set once ModalContext has been set. Set Title in OnParametersSet() or later.");

				_title = value;

				ModalContext.Title = Title;

				if (TitleChanged.HasDelegate)
					TitleChanged.InvokeAsync(value);
			}
		}

		[Parameter]
		public EventCallback<string> TitleChanged { get; set; }

		[Parameter]
		public virtual string CssClass
		{
			get { return _cssClass; }
			set
			{
				if (ModalContext == null)
					throw new InvalidOperationException("ModalContext is null. CssClass can only be set once ModalContext has been set. Set Title in OnParametersSet() or later.");

				_cssClass = value;

				ModalContext.CssClass = CssClass;

				if (CssClassChanged.HasDelegate)
					CssClassChanged.InvokeAsync(value);
			}
		}

		[Parameter]
		public EventCallback<string> CssClassChanged { get; set; }

		public virtual async Task Close()
		{
			await ModalStack.PopPast(ModalContext);
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
			if (ModalContext != null)
				ModalContext.ModalComponent = this;

			base.OnParametersSet();
		}

		public virtual void Dispose()
		{
			if (ModalContext != null)
			{
				ModalContext.ModalComponent = null;

				this.ModalContext = null;
			}
		}
	}
}
