package com.blitzagency.util
{
	import com.blitzagency.xray.ui.OutputTools;
	
	import flash.net.SharedObject;
	
	import mx.core.Application;
	//import flash.events.Event;
	
	public class LSOUserPreferences
	{
		//import com.blitzagency.events.GDispatcher;
		
	// Public Properties
		public static var app:Object = mx.core.Application.application;
		public static var loaded:Boolean = false;
		public static var persistent:Boolean = true;
	
	// Private Properties
		private static var preferences:Object = {};
		private static var storedObject:SharedObject;
	
		// EventDispatcher
		//public static var removeEventListener:Function;
		//private static var dispatchEvent:Function;
	
	
	// Initialization
		//private function LSOUserPreferences() {	}
	
	
	// Public Methods
		/*
		public static function addEventListener(p_type:String,p_listener:String):void{
			GDispatcher.initialize(LSOUserPreferences);
			LSOUserPreferences.addEventListener(p_type,p_listener);
		}
		*/
		
		// Retrieve Preference
		public static function getPreference(p_key:String):Object
		{
			/*
			if (preferences[p_key] == undefined) 
			{
				// Try and get LSO property?
				return;
			}
			*/
			return preferences[p_key];
		}
		
		public static function getAllPreferences():Object 
		{
			return preferences;
		}
	
		// Set Local/LSO Preference
		public static function setPreference(p_key:String, p_value:Object, p_persistent:Boolean):void 
		{
			//OutputTools.tt([p_key, p_value, p_persistent]);
			//app.output.text += "setPreference :: " + p_key + " :: " + p_value + " :: " + p_persistent + "\n";
			preferences[p_key] = p_value;
	
			// Optionally save to LSO
			//if (p_persistent == undefined) { p_persistent = persistent; } // Use Default Setting
			if (p_persistent) 
			{
				
				storedObject.data[p_key] = p_value;
				var r:String = storedObject.flush();
				var m:String;
				//app.output.text += "writing SO :: " + r +  "\n";
				switch (r) 
				{
					case "pending": 	
						//app.output.text += "case pending \n";
						m = "Flush is pending, waiting on user interaction"; 			
						break;
					case true: 		
						//app.output.text += "case true \n";
						m = "Flush was successful.  Requested Storage Space Approved"; 	
						break;
					case false: 	
						//app.output.text += "case false \n";
						m = "Flush failed.  User denied request for additional space."; 	
						break;
				}
				//var evtObj:Event = new Event("save");
				//evtObj.success = r;
				//evtObj.msg = m;
				//dispatchEvent(evtObj);
			}
		}
	
		// Load from LSO for now
		public static function load(p_path:String):void 
		{
			//storedObject = SharedObject.getLocal("userPreferences" + _root.projectID, "/");
			storedObject = SharedObject.getLocal("userPreferences" + p_path, "/");
			for (var i:String in storedObject.data)
			{
				preferences[i] = storedObject.data[i];
			}
			loaded = true;
			//dispatchEvent({type:"load", target:LSOUserPreferences, success:true});
		}
	
		// Clear LSO and reset preferences
		public static function clear():void 
		{
			storedObject.clear();
			storedObject.flush();
			storedObject = null;
			preferences = {};
		}
	
	// Semi-Public Methods
	// Private Methods
		

	}
}