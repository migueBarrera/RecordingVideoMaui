using Camera.MAUI;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RecordingVideoMaui
{
    public partial class CamViewModel : ObservableObject
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
                AutoStartPreview = true;
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

        [ObservableProperty]
        public bool autoStartPreview = false;

        [ObservableProperty]
        public bool autoStartRecording = false;
     
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
        public Command TakeSnapshotCmd { get; set; }
        public string RecordingFile { get; set; }
        public CamViewModel()
        {
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
            OnPropertyChanged(nameof(TakeSnapshotCmd));
            OnPropertyChanged(nameof(StartRecording));
            OnPropertyChanged(nameof(StopRecording));

            
        }

        [RelayCommand]
        public void StartRecording()
        {
           AutoStartRecording = true;
        }

        [RelayCommand]
        public void StopRecording()
        {
           AutoStartRecording = false;
           VideoSource = MediaSource.FromFile(RecordingFile);
           OnPropertyChanged(nameof(VideoSource));
        }

        [RelayCommand]
        public void Rec()
        {
           AutoStartPreview = true;
        }
    }
}

