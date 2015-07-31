package com.blitzagency.xray.ui
{
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import com.blitzagency.xray.ui.OutputTraceEvent;
	
	public class OutputDispatcher extends EventDispatcher
	{
		public static var TRACE:String = "trace";

		public function sendEvent(eventName:String, obj:Object):void 
		{
	        dispatchEvent(new OutputTraceEvent(OutputDispatcher.TRACE, false, false, obj));
	    }
	}
}