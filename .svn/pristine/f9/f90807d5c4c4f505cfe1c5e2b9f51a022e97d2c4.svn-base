
package org.audiofx.mp3
{
	import flash.display.Loader;
	import flash.display.LoaderInfo;
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.media.Sound;
	import flash.net.FileReference;
	import flash.utils.ByteArray;
	import flash.utils.Endian;
	

	[Event(name="complete", type="org.audiofx.mp3.MP3SoundEvent")]
	
	public class MP3FileReferenceLoader extends EventDispatcher
	{
		private var mp3Parser:MP3Parser;
		
		
		public function MP3FileReferenceLoader()
		{
			mp3Parser=new MP3Parser();
			mp3Parser.addEventListener(Event.COMPLETE,parserCompleteHandler);
			
		}
	
		public function getSound(fr:FileReference):void
		{
			
			mp3Parser.loadFileRef(fr);
		}
		private function parserCompleteHandler(ev:Event):void
		{
			var parser:MP3Parser=ev.currentTarget as MP3Parser;
			generateSound(parser);
		}
		private function generateSound(mp3Source:MP3Parser):Boolean
		{
			var swfBytes:ByteArray=new ByteArray();
			swfBytes.endian=Endian.LITTLE_ENDIAN;
			for(var i:uint=0;i<SoundClassSwfByteCode.soundClassSwfBytes1.length;++i)
			{
				swfBytes.writeByte(SoundClassSwfByteCode.soundClassSwfBytes1[i]);
			}
			var swfSizePosition:uint=swfBytes.position;
			swfBytes.writeInt(0); //swf size will go here
			for(i=0;i<SoundClassSwfByteCode.soundClassSwfBytes2.length;++i)
			{
				swfBytes.writeByte(SoundClassSwfByteCode.soundClassSwfBytes2[i]);
			}
			var audioSizePosition:uint=swfBytes.position;
			swfBytes.writeInt(0); //audiodatasize+7 to go here
			swfBytes.writeByte(1);
			swfBytes.writeByte(0);
			mp3Source.writeSwfFormatByte(swfBytes);
			
			var sampleSizePosition:uint=swfBytes.position;
			swfBytes.writeInt(0); //number of samples goes here
			
			swfBytes.writeByte(0); //seeksamples
			swfBytes.writeByte(0);
						
			var frameCount:uint=0;
			
			var byteCount:uint=0; //this includes the seeksamples written earlier
						
			for(;;)
			{
			
				var seg:ByteArraySegment=mp3Source.getNextFrame();
				if(seg==null)break;
				swfBytes.writeBytes(seg.byteArray,seg.start,seg.length);
				byteCount+=seg.length;
				frameCount++;
			}
			if(byteCount==0)
			{
				return false;
			}
			byteCount+=2;

			var currentPos:uint=swfBytes.position;
			swfBytes.position=audioSizePosition;
			swfBytes.writeInt(byteCount+7);
			swfBytes.position=sampleSizePosition;
			swfBytes.writeInt(frameCount*1152);
			swfBytes.position=currentPos;
			for(i=0;i<SoundClassSwfByteCode.soundClassSwfBytes3.length;++i)
			{
				swfBytes.writeByte(SoundClassSwfByteCode.soundClassSwfBytes3[i]);
			}
			swfBytes.position=swfSizePosition;
			swfBytes.writeInt(swfBytes.length);
			swfBytes.position=0;
			var swfBytesLoader:Loader=new Loader();
			swfBytesLoader.contentLoaderInfo.addEventListener(Event.COMPLETE,swfCreated);
			swfBytesLoader.loadBytes(swfBytes);
			return true;
		}
		private function swfCreated(ev:Event):void
		{

			var loaderInfo:LoaderInfo=ev.currentTarget as LoaderInfo;
			var soundClass:Class=loaderInfo.applicationDomain.getDefinition("SoundClass") as Class;
			var sound:Sound=new soundClass();
			dispatchEvent(new MP3SoundEvent(MP3SoundEvent.COMPLETE,sound,mp3Parser.mp3Data,mp3Parser.constantFrameSize,mp3Parser.frameTime));
			
		}

	}
}