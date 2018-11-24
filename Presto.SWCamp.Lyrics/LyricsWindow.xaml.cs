using Presto.SDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace Presto.SWCamp.Lyrics
{
    public partial class LyricsWindow : Window
    {
        string[][] splitData;  //파싱 데이터 
        TimeSpan[] time;       //파싱 데이터 시간 값
        public LyricsWindow()
        {
            InitializeComponent();
            //스티림이 바뀌었을 때의 이벤트 함수
            PrestoSDK.PrestoService.Player.StreamChanged += Player_StreamChanged;
        }

        private void Player_StreamChanged(object sender, Common.StreamChangedEventArgs e)
        {
            //가사 초기화
            textLyrics.Text = null;
            splitData = new string[300][];
            time = new TimeSpan[300];
            //lrc파일 경로 변경
            var fileName = PrestoSDK.PrestoService.Player.CurrentMusic.Path;
            var lrcName = Path.GetFileNameWithoutExtension(fileName) + ".lrc";
            var path = Path.Combine(Path.GetDirectoryName(fileName), lrcName);
            var lines = File.ReadAllLines(path);
            //가사 데이터 읽어 오기
            for (int i = 3; i < lines.Length; i++)
            {
                splitData[i] = lines[i].Split(']');
                time[i] = TimeSpan.ParseExact(splitData[i][0].Substring(1).Trim(), @"mm\:ss\.ff", CultureInfo.InvariantCulture);
            }
            //노래 재생시간 타이머
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            //가사 변경 범위 조정
            for (int i = 0; i < splitData.Length; i++)
            {
                var cur = TimeSpan.FromMilliseconds(PrestoSDK.PrestoService.Player.Position);
                if (cur >= time[i] && cur < time[i + 1])
                {
                    var data = splitData[i];
                    if (data != null)
                    {
                        textLyrics.Text = splitData[i][1];
                    }
                }
                if (i == 298) break;
            }
        }
    }
}
