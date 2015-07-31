package com.blitzagency.xray.ui
{
	import flash.events.Event;
	
	public class OutputTraceEvent extends Event
	{
		public var obj:Object = new Object();
		public function OutputTraceEvent(type:String, bubbles:Boolean, cancelable:Boolean, p_obj:Object):void
		{
			super(type, bubbles, cancelable);
			obj = obj;
		}	
	}
}