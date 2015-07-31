

package org.audiofx.mp3
{
	import flash.events.Event;
	import flash.media.Sound;
	import flash.utils.ByteArray;

	
	public class MP3SoundEvent extends Event
	{
		
		public var sound:Sound;
		public var mp3Data:ByteArray;
		public var framesize:uint;
		public var frametime:Number;
		
		
		public static const COMPLETE:String="complete";
		public function MP3SoundEvent(type:String, sound:Sound,mp3data:ByteArray, framesize:uint,frametime:Number,bubbles:Boolean=false, cancelable:Boolean=false)
		{
			super(type, bubbles, cancelable);
			this.sound=sound;
			this.mp3Data=mp3data;
			this.framesize=framesize;
			this.frametime=frametime;
		}
		
	}
}