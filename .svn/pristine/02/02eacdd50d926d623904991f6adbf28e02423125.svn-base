package
{
	
	import flash.media.Camera;
	import flash.media.H264Level;
	import flash.media.H264Profile;
	import flash.media.H264VideoStreamSettings;
	import flash.media.Microphone;
	import flash.media.SoundTransform;
	import flash.net.NetConnection;
	import flash.net.NetStream;

	
	
	public class Session
	{
		public var videoState:SessionState;
		public var audioState:SessionState;
		protected var netConnection:NetConnection;
		public var netStream:NetStream;
		public var sessionId:String;
		protected var responder:StreamResponder;
		protected var initialized:Boolean;
		
		protected var cameraIndex:int = 0;
		protected var microphoneIndex:int = 0;
		protected var camera:Camera;
		protected var microphone:Microphone;
		protected var width:uint;
		protected var height:uint;
		protected var fps:uint;
		protected var quality:uint;
		
		public function Session()
		{
			
		}
		
		public function Initialize(connection:NetConnection, session:String, camIndex:int, micIndex:int)
		{
			videoState = new SessionState();
			audioState = new SessionState();
			responder = new StreamResponder();
			netConnection = connection;
			sessionId = session;		
			cameraIndex = camIndex;
			microphoneIndex = micIndex;
			if(netConnection.connected)
			{
				netStream = new NetStream(netConnection);
				netStream.audioReliable = false;
				netStream.videoReliable = false;
				netStream.receiveVideo(false);
				netStream.receiveAudio(false);
				netStream.client = responder;
				initialized = true;
			}
		}		
		
		public function GetVideoState():SessionState
		{
			return videoState;
		}
		
		public function GetAudioState():SessionState
		{
			return audioState;
		}
		public function StartCamera(idx:int,w:int, h:int, f:uint, q:uint)
		{
		}
		
		public function CloseCamera()
		{
		}
		
		public function PublishVideo(idx:int,w:int, h:int, f:uint, q:uint)
		{
		}
		
		public function PauseVideo()
		{
		}
		
		public function ResumeVideo()
		{
		}
		public function ToggleVideo()
		{
			if(videoState.State == SessionState.None)
			{
				PublishVideo(cameraIndex,width,height,fps,quality);
				return camera;
			}			
			else if(videoState.State == SessionState.Paused)
			{
				ResumeVideo();
				return camera;
			}
			else
			{
				PauseVideo();
				return null;
			}
		}
		
		public function ToggleAudio()
		{
			if(audioState.State == SessionState.None )
			{
				PublishAudio(microphoneIndex);
			}
			else if (audioState.State == SessionState.Paused)
			{				
				ResumeAudio();				
			}
			else
			{
				PauseAudio();
			}
		}
		public function StartMicrophone(idx:int,silence:uint = 0, gain:uint = 50, rate:uint = 8, volume:Number = 1)
		{
		}
		
		public function CloseMicrophone()
		{
		}
		
		public function AjustVolume(volume:Number)
		{
		}
		
		public function PublishAudio(idx:int, silence:uint = 0, gain:uint = 50, rate:uint = 8, volume:Number = 1)
		{
		}
		
		public function PauseAudio()
		{
		}
		public function ResumeAudio()
		{
		}
		
		public function Destroy()
		{
		}		
	}
	
	
}