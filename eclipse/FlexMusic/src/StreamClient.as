package
{
	public class StreamClient
	{
		public function StreamClient()
		{
		}
		
		public function onMetaData(info:Object):void{
			
				//metaData = infoObject;
				//volumeSlider.value = (netStream.soundTransform.volume) * 100;
		}
		
		public function onPlayStatus(info:Object):void{
			for( var n:* in info){
				trace(n+":"+info[n]);
			}
				
		}
		
		public function onImageData(imageData:Object):void {
			trace("imageData recieved!");
		}
		
		public function onBWCheck():void
		{}
	}
}