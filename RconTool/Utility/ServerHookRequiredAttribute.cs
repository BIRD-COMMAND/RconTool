using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	[System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	sealed class ServerHookRequiredAttribute : Attribute
	{

		/// <summary>
		/// The message to display when the server hook is not available.
		/// </summary>
		readonly string unavailableMessage;
		/// <summary>
		/// Gets the message to display when the server hook is not available.
		/// </summary>
		public string UnavailableMessage {
			get { return unavailableMessage; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerHookRequiredAttribute"/> class.
		/// </summary>
		/// <param name="unavailableMessage">The message to display when the server hook is not available.</param>
		public ServerHookRequiredAttribute(string unavailableMessage) {
			this.unavailableMessage = string.IsNullOrWhiteSpace(unavailableMessage)  
				?  $"This function is not available without the server hook."
				:  unavailableMessage
			;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerHookRequiredAttribute"/> class with a default message.<br/>
		/// The default message is: "This function is not available without the server hook."
		/// </summary>
		public ServerHookRequiredAttribute() : this(null) {}

	}
}
