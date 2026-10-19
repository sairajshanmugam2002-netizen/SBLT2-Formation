/***********************************************************************************
* Add Array.indexOf                                                                *
***********************************************************************************/
(function ()
{
	if (typeof Array.prototype.indexOf !== 'function')
	{
		Array.prototype.indexOf = function(searchElement, fromIndex)
		{
			for (var i = (fromIndex || 0), j = this.length; i < j; i += 1)
			{
				if ((searchElement === undefined) || (searchElement === null))
				{
					if (this[i] === searchElement)
					{
						return i;
					}
				}
				else if (this[i] === searchElement)
				{
					return i;
				}
			}
			return -1;
		};
	}
})();
/**********************************************************************************/

(function ($,undefined)
{
	var toasting =
	{
		gettoaster : function ()
		{
			var toaster = $('#' + settings.toaster.id);

			if(toaster.length < 1)
			{
				toaster = $(settings.toaster.template).attr('id', settings.toaster.id).css(settings.toaster.css).addClass(settings.toaster['class']);

				if ((settings.stylesheet) && (!$("link[href=" + settings.stylesheet + "]").length))
				{
					$('head').appendTo('<link rel="stylesheet" href="' + settings.stylesheet + '">');
				}

				$(settings.toaster.container).append(toaster);
			}

			return toaster;
		},

		notify : function (title, message, priority)
		{
			var $toaster = this.gettoaster();

			// Prevent duplicate toast messages
			if (message)
			{
				var isDuplicate = false;
				$toaster.find('.message').each(function ()
				{
					if ($(this).text() === message)
					{
						isDuplicate = true;
						return false;
					}
				});
				if (isDuplicate)
				{
					return;
				}
			}

			var $toast = $(settings.toast.template.replace('%priority%', priority)).hide().css(settings.toast.css).addClass(settings.toast['class']);

			if (title)
			{
				$('.title', $toast).css(settings.toast.csst).html(title);
			}
			else
			{
				$('.title', $toast).remove();
				// Remove colon separator if title is empty
				$toast.contents().filter(function ()
				{
					return this.nodeType === 3 && $.trim(this.nodeValue) === ':';
				}).remove();
			}

			$('.message', $toast).css(settings.toast.cssm).html(message);

			// Add icon based on priority if no icon exists
			if ($('.toast-icon', $toast).length === 0)
			{
				var iconSvg = '';
				if (priority === 'success')
				{
					iconSvg = '<svg width="14" height="14" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">' +
						'<path d="M4 10.5L8.5 15L16 6" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>' +
						'</svg>';
				}
				else if (priority === 'danger' || priority === 'error')
				{
					iconSvg = '<svg width="14" height="14" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">' +
						'<path d="M15 5L5 15M5 5L15 15" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>' +
						'</svg>';
				}
				else if (priority === 'warning')
				{
					iconSvg = '<svg width="14" height="14" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">' +
						'<path d="M10 4V11M10 15H10.01" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>' +
						'</svg>';
				}
				else
				{
					iconSvg = '<svg width="14" height="14" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">' +
						'<circle cx="10" cy="10" r="8" stroke="currentColor" stroke-width="2"/>' +
						'<path d="M10 9V14M10 6H10.01" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>' +
						'</svg>';
				}
				$toast.prepend('<span class="toast-icon toast-icon-' + priority + '">' + iconSvg + '</span>');
			}

			// Handle close button click
			$toast.find('.close, [data-dismiss="alert"]').on('click', function (e)
			{
				e.preventDefault();
				settings.toast.remove($toast, function ()
				{
					$toast.remove();
				});
			});

			if ((settings.debug) && (window.console))
			{
				console.log($toast);
			}

			$toaster.append(settings.toast.display($toast));

			if (settings.donotdismiss.indexOf(priority) === -1)
			{
				var timeout = (typeof settings.timeout === 'number') ? settings.timeout : ((typeof settings.timeout === 'object') && (priority in settings.timeout)) ? settings.timeout[priority] : 3000;
				setTimeout(function()
				{
					settings.toast.remove($toast, function()
					{
						$toast.remove();
					});
				}, timeout);
			}
		}
	};

	var defaults =
	{
		'toaster'         :
		{
			'id'        : 'toaster',
			'container' : 'body',
			'template'  : '<div></div>',
			'class'     : 'toaster',
			'css'       :
			{
				'position' : 'fixed',
				'top'      : '20px',
				'right'    : '20px',
				'width'    : 'auto',
				'zIndex'   : 999999
			}
		},

		'toast'       :
		{
			'template' :
			'<div class="alert alert-%priority% alert-dismissible" role="alert">' +
				'<button type="button" class="close" data-dismiss="alert">' +
					'<span aria-hidden="true">&times;</span>' +
					'<span class="sr-only">Close</span>' +
				'</button>' +
				'<span class="title"></span>: <span class="message"></span>' +
			'</div>',

			'css'      : {},
			'cssm'     : {},
			'csst'     : { 'fontWeight' : 'bold' },

			'fade'     : 'slow',

			'display'    : function ($toast)
			{
				return $toast.fadeIn(settings.toast.fade);
			},

			'remove'     : function ($toast, callback)
			{
				return $toast.animate(
					{
						opacity : '0',
						padding : '0px',
						margin  : '0px',
						height  : '0px'
					},
					{
						duration : settings.toast.fade,
						complete : callback
					}
				);
			}
		},

		'debug'        : false,
		'timeout'      : 3000,
		'stylesheet'   : null,
		'donotdismiss' : []
	};

	var settings = {};
	$.extend(settings, defaults);

	$.toaster = function (options)
	{
		if (typeof options === 'object')
		{
			if ('settings' in options)
			{
				settings = $.extend(true, {}, settings, options.settings);
			}

			var title    = ('title' in options) ? options.title : 'Notice';
			var message  = ('message' in options) ? options.message : null;
			var priority = ('priority' in options) ? options.priority : 'success';

			if (message !== null)
			{
				toasting.notify(title, message, priority);
			}
		}
	};

	$.toaster.reset = function ()
	{
		settings = {};
		$.extend(settings, defaults);
	};

	if (typeof window !== 'undefined')
	{
		window.toaster = $.toaster;
		if (window.jQuery)
		{
			window.jQuery.toaster = $.toaster;
		}
		if (window.$)
		{
			window.$.toaster = $.toaster;
		}
	}
})(jQuery);
