using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace Mp4Player
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool _mediaPlayerIsStarting = false;
		private bool _userIsDraggingSlider = false;

		public MainWindow()
		{
			InitializeComponent();

			DataContext = new ViewModel();

			DispatcherTimer timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(1)
			};
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			if ((Player.Source != null) && (Player.NaturalDuration.HasTimeSpan) && (!_userIsDraggingSlider))
			{
				sliProgress.Minimum = 0;
				sliProgress.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;
				sliProgress.Value = Player.Position.TotalSeconds;
			}
		}

		private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "MP4 files (*.mp4;)|*.mp4"
			};
			if (openFileDialog.ShowDialog() == true)
			{
				Player.Source = new Uri(openFileDialog.FileName);
				fileName.Text = Path.GetFileName(openFileDialog.FileName);
				fileSize.Text = Math.Round(new FileInfo(openFileDialog.FileName).Length / Math.Pow(1024, 2), 2).ToString() + " Mb";
				((ViewModel)DataContext).Delete();
				_mediaPlayerIsStarting = false;
				Player.Stop();
			}
		}

		private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Player != null) && (Player.Source != null);
		}

		private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Player.Play();
			_mediaPlayerIsStarting = true;
		}

		private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = _mediaPlayerIsStarting;
		}

		private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Player.Pause();
		}

		private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = _mediaPlayerIsStarting;
		}

		private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Player.Stop();
			_mediaPlayerIsStarting = false;
		}

		private void SliProgress_DragStarted(object sender, DragStartedEventArgs e)
		{
			_userIsDraggingSlider = true;
		}

		private void SliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			_userIsDraggingSlider = false;
			Player.Position = TimeSpan.FromSeconds(sliProgress.Value);
		}

		private void SliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
		}

		private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			Player.Volume += (e.Delta > 0) ? 0.1 : -0.1;
		}

		private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = _mediaPlayerIsStarting;
		}

		private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			((ViewModel)DataContext).Add(Player.Position);
		}

		private void PlayBookMark_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Player != null) && (Player.Source != null) && (((ViewModel)DataContext).Selected != null);
		}

		private void PlayBookMark_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Player.Position = ((ViewModel)DataContext).Selected.Bookmark;
			Player.Play();
			_mediaPlayerIsStarting = true;
		}
	}
}
