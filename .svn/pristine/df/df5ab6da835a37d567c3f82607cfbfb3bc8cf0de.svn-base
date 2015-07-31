using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace YoYoStudio.Controls.Winform
{
    public delegate void FlashCallbackEventHandler(FlexCallbackCommand cmd, List<string> args);

	[ComVisible(true)]
	public partial class FlexControl : UserControl,IDisposable
	{
        public event FlashCallbackEventHandler FlashCallback;

		public FlexControl()
		{
			InitializeComponent();
            
		}

        void FlexControl_Resize(object sender, EventArgs e)
        {
            CallFlash(FlexCommand.Resize, new string[]{Width.ToString(),Height.ToString()});
        }
        
        #region IDisposable Implementation

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (axFlash != null)
            {
                axFlash.Dispose();
            }
            base.Dispose(disposing);
        }

        ~FlexControl()
        {
            Dispose(false);
        }

        #endregion

		void axFlash_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(e.request);

			string commandName = xmlDocument.FirstChild.Attributes[0].Value;

			List<string> args = new List<string>();
			XmlNodeList nodes = xmlDocument.GetElementsByTagName("arguments");
			if (nodes.Count > 0)
			{
				XmlNodeList parameterNodes = nodes[0].ChildNodes;
				if (parameterNodes.Count > 0)
				{
					foreach (XmlNode node in parameterNodes)
					{
						args.Add(node.InnerText);
					}
				}
			}
			FlexCallbackCommand cmd = FlexCallbackCommandNames.GetCommand(commandName);
			if (cmd == FlexCallbackCommand.LoadComplete)
			{
				CallFlash(FlexCommand.Resize, new string[] { Width.ToString(), Height.ToString() });
				Resize += FlexControl_Resize;
			}
			if (FlashCallback != null)
			{
				FlashCallback(cmd, args);
			}
		}

        public void LoadFlash(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    axFlash.Movie = filePath;
                    axFlash.FlashCall += axFlash_FlashCall;
                }
            }
        }

        public string[] CallFlash(FlexCommand cmd, params string[] args)
        {
            return CallFlash(FlexCommandNames.GetCommandName(cmd), args);
        }
        
        private string[] CallFlash(string cmdName, params string[] args)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmdName))
                {
                    string request = "<invoke name=\"" + cmdName + "\" returntype=\"xml\">";
                    request += "<arguments>";
                    for (int i = 0; i < args.Length; i++)
                    {
                        request += "<string>" + args[i] + "</string>";
                    }
                    request += "</arguments>";
                    request += "</invoke>";
                    string result = axFlash.CallFunction(request);
                    if (!string.IsNullOrEmpty(result))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(result);
                        XmlNodeList nodes = doc.GetElementsByTagName("string");
                        if (nodes.Count > 0)
                        {
                            return nodes[0].InnerText.Split(FlexCommandNames.FlexReturnDelimiter).Where(s=>!string.IsNullOrEmpty(s)).ToArray();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            return null;
        }        
	}
}
