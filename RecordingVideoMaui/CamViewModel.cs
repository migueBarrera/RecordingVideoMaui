using Camera.MAUI.ZXingHelper;
using Camera.MAUI;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZXing;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RecordingVideoMaui
{
    public class CamViewModel : ObservableObject
    {
        private CameraInfo camera = null;
        public CameraInfo Camera
        {
            get => camera;
            set
            {
                camera = value;
                OnPropertyChanged(nameof(Camera));
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
            }
        }
        private ObservableCollection<CameraInfo> cameras = new();
        public ObservableCollection<CameraInfo> Cameras
        {
            get => cameras;
            set
            {
                cameras = value;
                OnPropertyChanged(nameof(Cameras));
            }
        }
        public int NumCameras
        {
            set
            {
                if (value > 0)
                    Camera = Cameras.First();
            }
        }
        private MicrophoneInfo micro = null;
        public MicrophoneInfo Microphone
        {
            get => micro;
            set
            {
                micro = value;
                OnPropertyChanged(nameof(Microphone));
            }
        }
        private ObservableCollection<MicrophoneInfo> micros = new();
        public ObservableCollection<MicrophoneInfo> Microphones
        {
            get => micros;
            set
            {
                micros = value;
                OnPropertyChanged(nameof(Microphones));
            }
        }
        public int NumMicrophones
        {
            set
            {
                if (value > 0)
                    Microphone = Microphones.First();
            }
        }
        public MediaSource VideoSource { get; set; }
        public bool AutoStartPreview { get; set; } = false;
        public bool AutoStartRecording { get; set; } = false;
     
        private bool takeSnapshot = false;
        public bool TakeSnapshot
        {
            get => takeSnapshot;
            set
            {
                takeSnapshot = value;
                OnPropertyChanged(nameof(TakeSnapshot));
            }
        }
        public float SnapshotSeconds { get; set; } = 0f;
        public string Seconds
        {
            get => SnapshotSeconds.ToString();
            set
            {
                if (float.TryParse(value, out float seconds))
                {
                    SnapshotSeconds = seconds;
                    OnPropertyChanged(nameof(SnapshotSeconds));
                }
            }
        }
        public Command StartCamera { get; set; }
        public Command StopCamera { get; set; }
        public Command TakeSnapshotCmd { get; set; }
        public string RecordingFile { get; set; }
        public Command StartRecording { get; set; }
        public Command StopRecording { get; set; }

        public CamViewModel()
        {
            StartCamera = new Command(() =>
            {
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
            });
            StopCamera = new Command(() =>
            {
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
            });
            TakeSnapshotCmd = new Command(() =>
            {
                TakeSnapshot = false;
                TakeSnapshot = true;
            });
#if IOS
        RecordingFile = Path.Combine(FileSystem.Current.CacheDirectory, "Video.mov");
#else
            RecordingFile = Path.Combine(FileSystem.Current.AppDataDirectory, "Video.mp4");
#endif
            OnPropertyChanged(nameof(RecordingFile));
            StartRecording = new Command(() =>
            {
                AutoStartRecording = true;
                OnPropertyChanged(nameof(AutoStartRecording));
            });
            StopRecording = new Command(() =>
            {
                AutoStartRecording = false;
                OnPropertyChanged(nameof(AutoStartRecording));
                VideoSource = MediaSource.FromFile(RecordingFile);
                OnPropertyChanged(nameof(VideoSource));
            });
            OnPropertyChanged(nameof(StartCamera));
            OnPropertyChanged(nameof(StopCamera));
            OnPropertyChanged(nameof(TakeSnapshotCmd));
            OnPropertyChanged(nameof(StartRecording));
            OnPropertyChanged(nameof(StopRecording));
        }
    }
}

