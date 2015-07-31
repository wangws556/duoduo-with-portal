package org.red5.as3.utils
{
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	//import flash.display.Sprite;
	import mx.core.UIComponent;
	import flash.events.Event;
	import flash.media.Sound;
	import flash.media.SoundChannel;
	import flash.media.SoundMixer;
	import flash.net.URLRequest;
	import flash.utils.ByteArray;
	import flash.utils.setInterval;

	  
	public class SoundAnalyzer extends UIComponent 
	{
		private var mySound:Sound;
		private var channel:SoundChannel;
		private var spectrumGraph:BitmapData;
		private var si:Number;
			  
		public function SoundAnalyzer() 
		{
			// Create bitmap for spectrum display
			spectrumGraph = new BitmapData(256, 60,
										   true, 
										   0x00000000);
			var bitmap:Bitmap = new Bitmap(spectrumGraph);
			addChild(bitmap);
			bitmap.x = 10;
			bitmap.y = 10; 
			//addEventListener(Event.ENTER_FRAME, onEnterFrame);
			setStyle("horizontalCenter", 0);
			setStyle("verticalCenter", 0);
		}
		
		public function setSound(p_sound:String):void
		{
			si = setInterval(onEnterFrame, 25);
			mySound = new Sound(new URLRequest(p_sound));
			channel = mySound.play();
		}

		public function onEnterFrame():void
		{
			// Create the byte array and fill it with data
			var spectrum:ByteArray = new ByteArray();
			SoundMixer.computeSpectrum(spectrum);

			// Clear the bitmap
			spectrumGraph.fillRect(spectrumGraph.rect,
								   0x00000000);

			// Create the left channel visualization
			for(var i:int=0;i<256;i++) 
			{
			  spectrumGraph.setPixel32(i, 
							20 + spectrum.readFloat() * 20, 
							0xff00ff00);
			}
		  
			// Create the right channel visualization
			for(var j:int=0;j<256;j++) 
			{
				spectrumGraph.setPixel32(j,
						40 + spectrum.readFloat() * 20,
						0xff00ff00);
			}
		}       
	}
}
