using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YoYoStudio.Controls.Winform
{
    public enum FlexCommand
    {
        None = 0,
        GetCameras,
        Resize,
        StartCamera,
        TakePicture,
        CloseCamera,
        ConnectRTMP,
        Connect,
        Disconnect,
        PublishVideo,
        PauseVideo,
        ResumeVideo,
        PublishAudio,
        PauseAudio,
        ResumeAudio,
        LoadMusics,
        GetPlayStatus,
        PlayMusic,
        SetPlayPosition,
        TogglePauseMusic,
        SetVolume,
        StopMusic,
        GetMusicName,
        GetPlayPosition
    }

    

    public class FlexCommandNames
    {
        public const char FlexReturnDelimiter = '|';
        public const string None = "none";
        public const string GetCameras = "getCameras";
        public const string Resize = "resize";
        public const string StartCamera = "startCamera";
        public const string CloseCamera = "closeCamera";
        public const string TakePicture = "takePicture";
        public const string ConnectRTMP = "connectRtmp";
        public const string Connect = "connect";
        public const string Disconnect = "disconnect";
        public const string PublishVideo = "publishVideo";
        public const string PauseVideo = "pauseVideo";
        public const string ResumeVideo = "resumeVideo";
        public const string PublishAudio = "publishAudio";
        public const string PauseAudio = "pauseAudio";
        public const string ResumeAudio = "resumeAudio";
        public const string LoadMusics = "loadMusics";
        public const string GetPlayStatus = "getPlayStatus";
        public const string PlayMusic = "playMusic";
        public const string SetPlayPosition = "setPlayPosition";
        public const string TogglePauseMusic = "togglePauseMusic";
        public const string SetVolume = "setVolume";
        public const string StopMusic = "stopPlay";
        public const string GetMusicName = "getMusicName";
        public const string GetPlayPosition = "getPlayPosition";
		
        public static FlexCommand GetCommand(string cmdName)
        {
            switch (cmdName)
            {
                case GetCameras:
                    return FlexCommand.GetCameras;
                case Resize:
                    return FlexCommand.Resize;
                case StartCamera:
                    return FlexCommand.StartCamera;
                case CloseCamera:
                    return FlexCommand.CloseCamera;
                case TakePicture:
                    return FlexCommand.TakePicture;
                case ConnectRTMP:
                    return FlexCommand.ConnectRTMP;
                case Connect:
                    return FlexCommand.Connect;
                case Disconnect:
                    return FlexCommand.Disconnect;
                case PublishVideo:
                    return FlexCommand.PublishVideo;
                case PauseVideo:
                    return FlexCommand.PauseVideo;
                case ResumeVideo:
                    return FlexCommand.ResumeVideo;
                case PublishAudio:
                    return FlexCommand.PublishAudio;
                case PauseAudio:
                    return FlexCommand.PauseAudio;
                case ResumeAudio:
                    return FlexCommand.ResumeAudio;
                case LoadMusics:
                    return FlexCommand.LoadMusics;
                case GetPlayStatus:
                    return FlexCommand.GetPlayStatus;
                case PlayMusic:
                    return FlexCommand.PlayMusic;
                case SetPlayPosition:
                    return FlexCommand.SetPlayPosition;
                case TogglePauseMusic:
                    return FlexCommand.TogglePauseMusic;
                case SetVolume:
                    return FlexCommand.SetVolume;
                case StopMusic:
                    return FlexCommand.StopMusic;
                case GetMusicName:
                    return FlexCommand.GetMusicName;
                case GetPlayPosition:
                    return FlexCommand.GetPlayPosition;
                default:
                    return FlexCommand.None;
            }
        }

        public static string GetCommandName(FlexCommand cmd)
        {
            switch (cmd)
            {
                case FlexCommand.GetCameras:
                    return GetCameras;
                case FlexCommand.Resize:
                    return Resize;
                case FlexCommand.StartCamera:
                    return StartCamera;
                case FlexCommand.CloseCamera:
                    return CloseCamera;
                case FlexCommand.TakePicture:
                    return TakePicture;
                case FlexCommand.ConnectRTMP:
                    return ConnectRTMP;
                case FlexCommand.Connect:
                    return Connect;
                case FlexCommand.Disconnect:
                    return Disconnect;
                case FlexCommand.PublishVideo:
                    return PublishVideo;
                case FlexCommand.PauseVideo:
                    return PauseVideo;
                case FlexCommand.ResumeVideo:
                    return ResumeVideo;
                case FlexCommand.PublishAudio:
                    return PublishAudio;
                case FlexCommand.PauseAudio:
                    return PauseAudio;
                case FlexCommand.ResumeAudio:
                    return ResumeAudio;
                case FlexCommand.LoadMusics:
                    return LoadMusics;
                case FlexCommand.GetPlayStatus:
                    return GetPlayStatus;
                case FlexCommand.PlayMusic:
                    return PlayMusic;
                case FlexCommand.SetPlayPosition:
                    return SetPlayPosition;
                case FlexCommand.TogglePauseMusic:
                    return TogglePauseMusic;
                case FlexCommand.SetVolume:
                    return SetVolume;
                case FlexCommand.StopMusic:
                    return StopMusic;
                case FlexCommand.GetMusicName:
                    return GetMusicName;
                case FlexCommand.GetPlayPosition:
                    return GetPlayPosition;
                default:
                    return string.Empty;
            }
        }
    }

    public enum FlexCallbackCommand
    {
        None = 0,
        ReportStatus,
        LoadComplete,
        ScaleXDefault,
        ScaleXMirror,
        TakePicture,
        ExtendVideo,
        VideoStateChanged,
        AudioStateChanged,
        ZoomIn,
        ZoomOut,
        SetPlayPosition,
        SetVolume,
        PlayMusic,
        TogglePauseMusic,
        StopMusic
    }

    public class FlexStatusStrings
    {
        public const string ConnectSucceed = "NetConnection.Connect.Success";
        public const string NotConnected = "NotConnected";
        public const string MusicPlaying = "Playing";
        public const string MusicPaused = "Paused";
        public const string MusicUnPaused = "UnPaused";
        public const string MusicStopped = "Stoped";
    }

    public class FlexCallbackCommandNames
    {
        public const int AV_State_None = 0;
        public const int AV_State_Normal = 1;
        public const int AV_State_Paused = 2;
        public const int AV_State_Resumed = 4;

        public const string None = "none";
        public const string ReportStatus = "reportStatus";
        public const string LoadComplete = "loadComplete";
        public const string ScaleXDefault = "scaleXDefault";
        public const string ScaleXMirror = "scaleXMirror";
        public const string TakePicture = "takePicture";
        public const string ExtendVideo = "extendVideo";
        public const string VideoStateChanged = "videoStateChanged";
        public const string AudioStateChanged = "audioStateChanged";
        public const string ZoomIn = "zoomIn";
        public const string ZoomOut = "zoomOut";
        public const string  SetPlayPosition = "setPlayPosition";
        public const string SetVolume = "setVolume";
        public const string PlayMusic = "playMusic";
        public const string TogglePauseMusic = "togglePauseMusic";
        public const string StopMusic = "stopPlay";

        public static FlexCallbackCommand GetCommand(string commandName)
        {
            switch (commandName)
            {
                case ReportStatus:
                    return FlexCallbackCommand.ReportStatus;
                case LoadComplete:
                    return FlexCallbackCommand.LoadComplete;
                case ScaleXDefault:
                    return FlexCallbackCommand.ScaleXDefault;
                case ScaleXMirror:
                    return FlexCallbackCommand.ScaleXMirror;
                case TakePicture:
                    return FlexCallbackCommand.TakePicture;
                case ExtendVideo:
                    return FlexCallbackCommand.ExtendVideo;
                case VideoStateChanged:
                    return FlexCallbackCommand.VideoStateChanged;
                case AudioStateChanged:
                    return FlexCallbackCommand.AudioStateChanged;
                case ZoomIn:
                    return FlexCallbackCommand.ZoomIn;
                case ZoomOut:
                    return FlexCallbackCommand.ZoomOut;
                case SetPlayPosition:
                    return FlexCallbackCommand.SetPlayPosition;
                case SetVolume:
                    return FlexCallbackCommand.SetVolume;
                case PlayMusic:
                    return FlexCallbackCommand.PlayMusic;
                case TogglePauseMusic:
                    return FlexCallbackCommand.TogglePauseMusic;
                case StopMusic:
                    return FlexCallbackCommand.StopMusic;

            }
            return FlexCallbackCommand.None;
        }

        public static string GetCommandName(FlexCallbackCommand command)
        {
            switch (command)
            {
                case FlexCallbackCommand.LoadComplete:
                    return LoadComplete;
                case FlexCallbackCommand.ReportStatus:
                    return ReportStatus;
                case FlexCallbackCommand.ScaleXDefault:
                    return ScaleXDefault;
                case FlexCallbackCommand.ScaleXMirror:
                    return ScaleXMirror;
                case FlexCallbackCommand.TakePicture:
                    return TakePicture;
                case FlexCallbackCommand.ExtendVideo:
                    return ExtendVideo;
                case FlexCallbackCommand.AudioStateChanged:
                    return AudioStateChanged;
                case FlexCallbackCommand.VideoStateChanged:
                    return VideoStateChanged;
                case FlexCallbackCommand.ZoomIn:
                    return ZoomIn;
                case FlexCallbackCommand.ZoomOut:
                    return ZoomOut;
                case FlexCallbackCommand.SetPlayPosition:
                    return SetPlayPosition;
                case FlexCallbackCommand.SetVolume:
                    return SetVolume;
                case FlexCallbackCommand.PlayMusic:
                    return PlayMusic;
                case FlexCallbackCommand.TogglePauseMusic:
                    return TogglePauseMusic;
                case FlexCallbackCommand.StopMusic:
                    return StopMusic;
            }
            return None;
        }
    }
}
