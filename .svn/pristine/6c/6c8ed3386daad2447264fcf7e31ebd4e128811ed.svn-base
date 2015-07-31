package
{
	import flash.net.NetConnection;
	import flash.net.NetStream;
	
	public class Receiver extends Session
	{
		override public function PublishVideo(idx:int,w:int, h:int, f:uint, q:uint)
		{
			if(initialized)
			{
				netStream.receiveVideo(true);
				netStream.play(sessionId);
				videoState.State = SessionState.Normal;
				return netStream;
			}
			return null;
		}
		
		
		override public function PublishAudio(idx:int, silence:uint = 0, gain:uint = 50, rate:uint = 8, volume:Number = 1)
		{
			if(initialized)
			{
				netStream.receiveAudio(true);
				netStream.play(sessionId);
				audioState.State = SessionState.Normal;
			}
		}
		
		override public function PauseAudio()
		{
			if(initialized)
			{
				if(audioState.State  == SessionState.Normal ||
					audioState.State == SessionState.Resumed)
				{
					netStream.receiveAudio(false);
					netStream.play(sessionId);
					audioState.State = SessionState.Paused;
				}
			}
		}
		
		override public function ResumeAudio()
		{
			if(initialized)
			{
				if(audioState.State == SessionState.Paused)
				{
					netStream.receiveAudio(true);
					netStream.play(sessionId);
					audioState.State = SessionState.Resumed;
				}
				else if(audioState.State == SessionState.None)
				{
					PublishAudio(microphoneIndex);
				}
			}
		}
		
		override public function Destroy()
		{
			if(initialized)
			{
				try
				{
					netStream.receiveAudio(false);
					netStream.receiveVideo(false);
					netStream.close();
				}
				catch(e:Error){}
				netStream = null;	
				initialized = false;
			}
		}
		override public function PauseVideo()
		{
			if(initialized)
			{
				if(videoState.State == SessionState.Normal ||
					videoState.State == SessionState.Resumed)
				{
					netStream.receiveVideo(false);
					netStream.play(sessionId);
					videoState.State = SessionState.Paused;
				}
			}
		}
		override public function ResumeVideo()
		{
			if(initialized)
			{
				if(videoState.State == SessionState.Paused)
				{
					netStream.receiveVideo(true);
					netStream.play(sessionId);
					videoState.State = SessionState.Resumed;
				}
				else if(videoState.State == SessionState.None)
				{
					PublishVideo(cameraIndex,width,height,fps,quality);
				}
				return netStream;
			}
		}
	}
}