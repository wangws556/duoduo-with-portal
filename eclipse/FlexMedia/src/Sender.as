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
	import flash.media.SoundCodec;
	
	public class Sender extends Session
	{
		
		override public function StartCamera(idx:int,w:int, h:int, f:uint, q:uint)
		{
			cameraIndex = idx;
			var cameraCount = Camera.names.length;
			if( cameraCount > cameraIndex)
			{
				width = w;
				height = h;
				fps = f;
				quality = q;
				camera = Camera.getCamera(String(cameraIndex));				
				if(camera != null)
				{
					camera.setMode(width,height,fps);
					camera.setQuality(0,quality);
				}
				return camera;
			}
			return null;
		}
		
		override public function CloseCamera()
		{
			if(initialized)
			{
				camera = Camera.getCamera(null);
				camera = null;
			}
		}
		
		override public function PublishVideo(idx:int,w:int, h:int, f:uint, q:uint)
		{
			if(initialized)
			{
				if(videoState.State == SessionState.None)
				{
					StartCamera(idx,w,h,f,q);
				}
				var h264Settings:H264VideoStreamSettings = new H264VideoStreamSettings();
				h264Settings.setProfileLevel(H264Profile.MAIN ,H264Level.LEVEL_5_1);
				h264Settings.setMode(width,height,fps);
				h264Settings.setQuality(0,quality); 	
				netStream.videoStreamSettings = h264Settings;
				netStream.publish(sessionId);
				netStream.attachCamera(camera);
				videoState.State = SessionState.Normal;
				return camera;
			}
			return null;
		}
		
		override public function PauseVideo()
		{
			if(initialized)
			{
				if(videoState.State == SessionState.Normal||
					videoState.State == SessionState.Resumed)
				{
					netStream.attachCamera(null);
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
					netStream.attachCamera(camera);
					videoState.State = SessionState.Resumed;
				}
				else if(videoState.State == SessionState.None)
				{
					PublishVideo(cameraIndex,width,height,fps,quality);
				}
				return camera;
			}
		}
		
		override public function StartMicrophone(idx:int, silence:uint = 0, gain:uint = 50, rate:uint = 8, volume:Number = 1)
		{
			microphoneIndex = idx;
			var microphoneCount:int = Microphone.names.length;
			if(microphoneCount > microphoneIndex)
			{
				microphone = Microphone.getEnhancedMicrophone(microphoneIndex);
				if(microphone != null)
				{
					microphone.gain = gain;
					microphone.rate = rate;
					microphone.setLoopBack(false);
					microphone.setUseEchoSuppression(true);
					microphone.setSilenceLevel(silence);
					microphone.codec = SoundCodec.SPEEX;
					microphone.encodeQuality = 4;
					microphone.framesPerPacket = 4;
					
					var microphoneSoundTransform:SoundTransform = microphone.soundTransform;
					microphoneSoundTransform.volume = volume;
					microphone.soundTransform = microphoneSoundTransform;
				}
				return microphone;
			}
		}
		
		override public function CloseMicrophone()
		{
			if(initialized)
			{
				netStream.attachAudio(null);
				microphone = null;
			}
		}
		
		override public function AjustVolume(volume:Number)
		{
			if(initialized)
			{
				if(microphone != null)
				{
					var microphoneSoundTransform:SoundTransform = microphone.soundTransform;
					microphoneSoundTransform.volume = volume;
					microphone.soundTransform = microphoneSoundTransform;
				}
			}
		}
		
		override public function PublishAudio(idx:int, silence:uint = 0, gain:uint = 50, rate:uint = 8, volume:Number = 1)
		{
			if(initialized)
			{
				if(audioState.State == SessionState.None)
				{
					StartMicrophone(idx,silence,gain,rate,volume);
				}
				netStream.attachAudio(microphone);
				audioState.State = SessionState.Normal;
			}				
		}
		
		override public function PauseAudio()
		{
			if(initialized)
			{
				if(audioState.State == SessionState.Normal ||
					audioState.State == SessionState.Resumed)
				{
					netStream.attachAudio(null);
					audioState.State = SessionState.Paused;
				}
			}
		}
		override public function ResumeAudio()
		{
			if(initialized)
			{
				if(audioState.State = SessionState.Paused)
				{
					netStream.attachAudio(microphone);
					audioState.State = SessionState.Resumed;
				}
				else if(audioState.State = SessionState.None)
				{
					PublishAudio(microphoneIndex);
				}
			}
		}
		
		override public function Destroy()
		{
			if(initialized)
			{
				CloseMicrophone();
				CloseCamera();
				try
				{
					netStream.attachAudio(null);
					netStream.receiveAudio(false);
					netStream.attachCamera(null);
					netStream.receiveVideo(false);
					netStream.close();
				}
				catch(e:Error){}
				netStream = null;	
				initialized = false;
			}
		}
	}
}