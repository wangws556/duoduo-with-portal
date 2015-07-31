package com.blitzagency.xray.ui
{
	import com.blitzagency.util.LSOUserPreferences;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	import flash.utils.*;
	
	import mx.controls.TextArea;
	import mx.core.Application;
	
	public class OutputTools
	{
		public static var app:Object = mx.core.Application.application;
		public static var acceptOutput:Boolean = true;
		public static var outputInterface:OutputPanel;
		public static var initialized:Boolean = initialize();
		private static var xt:XrayTrace = new XrayTrace();
		private static var history:Array = new Array();
		private static var lastSearch:String;
		private static var lastSearchIndex:Number;
		private static var scrolling:Boolean = false;
		private static var searchList:Array;
		private static var od:OutputDispatcher;
		
		public static function initialize():Boolean
		{
			//initOD();
			LSOUserPreferences.load("XrayOutputPrefs");
			return true;
		}
		
		/*
		public static function initOD():void
		{
			if(!od)	var od:OutputDispatcher = new OutputDispatcher();
		}
		
		public static function addEventListener(type:String, listener:Function):void
		{
			initOD();
			od.addEventListener(type, listener);
		}
		*/
		
		public static function tt(p_messageList:Array):void
		{
			if(!acceptOutput) return;
			
			var ary:Array = [];
			//ary.push("(" + getTimer() + ") ");
			
			for(var i:Number=0;i<p_messageList.length;i++)
			{
				var value:String = xt.trace(p_messageList[i]);
				ary.push(value);
				//ary.push(p_messageList[i].toString());
			}
			
			history.push(ary.join("") + "\n");
			//app.output.data = history.join("\n");
			
			//od.sendEvent(OutputDispatcher.TRACE, {history:history});
			outputInterface.output.data = history.join("\n");
			updateScroll();
		}
		
		public static function updateScroll():void
		{
			outputInterface.output.verticalScrollPosition = outputInterface.output.maxVerticalScrollPosition;
		}
		
		public static function clear():void
		{
			history = new Array();
			outputInterface.output.text = "";
		}
		
		public static function setAcceptOutput():void
		{
			acceptOutput = outputInterface.acceptOutput.selected;
			if(!acceptOutput) outputInterface.output.text += "*** You've turned off output.  You're trace statements will not appear. ***";
			LSOUserPreferences.setPreference("acceptOutput", outputInterface.acceptOutput.selected, true);
		}
		
		public static function resetLastSearch():void
		{
			lastSearch = "";
			outputInterface.output.setSelection(0,0);
		}
		
		public static function handleSearchKey(key:KeyboardEvent):void
		{
			if(key.keyCode != Keyboard.ENTER) return;
			search();
		}
		
		public static function search():Boolean
		{
			var search:String = outputInterface.searchPhrase.text;
			var caseSensitive:Boolean = outputInterface.caseSensitive.selected;
			// clear searchList
			// put all occurances into the array of the specified search string
			if(search.length > 0)
			{
				if(lastSearch != search)
				{
					// reset the search index
					lastSearchIndex = 0;
					
					// reset the last search variable
					lastSearch = search
					
					// set bScrolling to true so that it doesn't move during the search
					scrolling = true;
					
					// initialize search array
					searchList = new Array();
					
					// init toSearch based on whether this is a case sensitive search or not
					var toSearch:String = caseSensitive ? outputInterface.output.text : outputInterface.output.text.toLowerCase();
					
					// if there's nothing to search through, return false
					if(toSearch.length <= 0) return false;
					
					// reset search based on case search
					search = caseSensitive ? search : search.toLowerCase();
					
					// set while loop flag
					var continueSearch:Boolean = true;
					
					
					
					// iIndex is reset in the loop everytime a match is found
					var index:Number = 0;
					while(continueSearch)
					{
						var i:Number = toSearch.indexOf(search, index);
						if(i >= 0)
						{
							// if you find a match, push into searchList
							searchList.push(
							{
								sBlock: toSearch.substring(0, i+search.length), 
								iBIndex: i, 
								iEIndex: i+search.length
							});
							index = i+(search.length+1);
						}else
						{
							continueSearch = false;
						}
					}
					gotoNextFind();
				}else
				{
					// if the search hasn't changed, just call next highlight
					gotoNextFind();
				}
			}
			
			return true;
		}
		
		private static function gotoNextFind():void
		{
			// init object from the searchList
			var obj:Object = searchList[lastSearchIndex];
			
			// reset lastSearchIndex
			lastSearchIndex = lastSearchIndex + 1 > searchList.length-1 ? 0 : lastSearchIndex+1;
			
			// set selection focus to the output window
			outputInterface.output.setFocus();
			
			// set the selection - this HAS to happen before you set the scroll
			outputInterface.output.setSelection(obj.iBIndex, obj.iEIndex);
			
			// get the row to set the scroll to
			//
			var i:Number = getScrollLocation(obj.sBlock);
			
			// set scroll position
			//outputInterface.output.setPosition(i-10);
		}
		
		private static function getScrollLocation(searchBlock:String):Number
		{
			outputInterface.searchLineCheck.text = searchBlock;
			return outputInterface.searchLineCheck.maxVerticalScrollPosition;
		}
	}
}