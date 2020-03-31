
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace ScrewTurn.Wiki.PluginFramework {

	/// <summary>
    /// It is the interface that must be implemented in order to create a custom Formatter Provider for ScrewTurn Wiki.
	/// </summary>
	public interface IFormatterProviderV50 : IProviderV60 {

		/// <summary>
		/// Specifies whether or not to execute Phase 1.
		/// </summary>
		bool PerformPhase1 { get; }

		/// <summary>
		/// Specifies whether or not to execute Phase 2.
		/// </summary>
		bool PerformPhase2 { get; }

		/// <summary>
		/// Specifies whether or not to execute Phase 3.
		/// </summary>
		bool PerformPhase3 { get; }

		/// <summary>
		/// Gets the execution priority of the provider (0 lowest, 100 highest).
		/// </summary>
		int ExecutionPriority { get; }

		/// <summary>
		/// Performs a Formatting phase.
		/// </summary>
		/// <param name="raw">The raw content to Format.</param>
		/// <param name="context">The Context information.</param>
		/// <param name="phase">The Phase.</param>
		/// <returns>The Formatted content.</returns>
		string Format(string raw, ContextInformation context, FormattingPhase phase);

		/// <summary>
		/// Prepares the title of an item for display (always during phase 3).
		/// </summary>
		/// <param name="title">The input title.</param>
		/// <param name="context">The context information.</param>
		/// <returns>The prepared title (no markup allowed).</returns>
		string PrepareTitle(string title, ContextInformation context);

		/// <summary>
		/// Method called when the plugin must handle a HTTP request.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <param name="urlMatch">The URL match.</param>
		/// <returns><c>true</c> if the request was handled, <c>false</c> otherwise.</returns>
		/// <remarks>This method is called only when a request matches the 
		/// parameters configured by calling <see cref="IHostV50.RegisterRequestHandler"/> during <see cref="IProviderV60.SetUp"/>. 
		/// If the plugin <b>did not</b> call <see cref="IHostV50.RegisterRequestHandler"/>, this method is never called.</remarks>
		bool HandleRequest(HttpContext context, Match urlMatch);


        /// <summary>
        /// Enable plugins of editor
        /// </summary>
        bool EnablePluginsEditor { get; }

        /// <summary>
        /// Get file for Editor-editor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="destinationStream"></param>
        /// <returns></returns>
        bool GetEditorsFile(string name, Stream destinationStream);

        /// <summary>
        /// List of the names of the Editor-plugins
        /// </summary>
        List<String> PluginsEditroNames { get; }

        /// <summary>
        /// Performs a Formatting phase (Wysiwyg-editor)
        /// </summary>
        /// <param name="raw">The raw content to Format.</param>
        /// <param name="context">The Context information.</param>
        /// <param name="phase">The Phase (only Phase 1 and Phase 2)</param>
        /// <returns>The Formatted content.</returns>
        string WysiwygFormat(string raw, ContextInformation context, FormattingPhase phase);

        /// <summary>
        /// Reverse Format
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sb"></param>
        /// <returns>true - reverse format was success, false - alien tag</returns>
        bool WysiwygReverseFormat(XmlNode node, StringBuilder sb);
	}

	/// <summary>
	/// Enumerates formatting Phases.
	/// </summary>
	public enum FormattingPhase {
		/// <summary>
		/// Phase 1, performed before the internal formatting step.
		/// </summary>
		Phase1,
		/// <summary>
		/// Phase 2, performed after the internal formatting step.
		/// </summary>
		Phase2,
		/// <summary>
		/// Phase 3, performed before sending the page content to the client.
		/// </summary>
		Phase3
	}

}
